using ConferenceService.DBContext.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ConferenceService.DTO_models;
using ConferenceService.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
namespace ConferenceService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class applicationsController : ControllerBase
    {
        private readonly DBContext.DatabaseContext _context;
        public applicationsController(DBContext.DatabaseContext context)
        {
            _context = context;
        }
        private bool CheckAppDTOonCreation(ApplicationDTO dto)
        {
            return dto.activity != null || dto.name != null || dto.description != null || dto.outline != null;
        }
        private bool CheckApponSubmitting(Application app)
        {
            return app.activity != null && app.Name != null && app.Outline != null;
        }
        private bool CheckAppDTOonEditing(EditApplicationDTO dto)
        {
            return dto.activity != null || dto.name != null || dto.description == null || dto.outline != null;
        }
        [HttpPost]
        public async Task<IActionResult> CreateApplication([FromBody] ApplicationDTO appDTO)
        {
            if (appDTO.author == null)
            {
                return BadRequest("Нельзя создать заявку не указав идентификатор пользователя");
            }
            var user = await _context.users.Include(x => x.currentApplication).FirstOrDefaultAsync(x => x.id == appDTO.author);

            if (user == null)
            {
                user = new User((Guid)appDTO.author);
                await _context.users.AddAsync(user);
                await _context.SaveChangesAsync();
            }
            if (user.currentApplication != null)
            {
                return BadRequest("Пользователь уже имеет черновик заявки!");
            }

            if (!Enum.TryParse(appDTO.activity, true, out EnumTypeActivity enumActivity) && appDTO.activity != null)
            {
                return BadRequest("Неправильный тип активности!.");
            }
            var activity = await _context.activities.FirstOrDefaultAsync(x => x.activity == enumActivity);
            if (!CheckAppDTOonCreation(appDTO))
            {
                return BadRequest("Нельзя создать заявку не указав хотя бы еще одно поле помимо идентификатора пользователя");
            }
            try
            {

                Application newApplication = new Application((Guid)appDTO.author, activity, appDTO.name, appDTO.description, appDTO.outline, DateTime.Now);
                user.currentApplication = newApplication;
                await _context.applications.AddAsync(newApplication);
                return Ok(new { id = newApplication.id, author = appDTO.author, activity = enumActivity.ToString(), appDTO.name, appDTO.description, appDTO.outline });
            }

            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest($"{ex.ParamName} - {ex.Message}, отправленное значение {ex.ActualValue}");
            }

            finally
            {
                await _context.SaveChangesAsync();
            }
        }

        [HttpPut]
        [Route("{guid}")]
        public async Task<IActionResult> UpdateApplication([FromRoute] Guid guid, [FromBody] EditApplicationDTO appDTO)
        {
            if (!CheckAppDTOonEditing(appDTO))
            {
                return BadRequest("Нельзя отредактировать заявку так, чтобы  в ней не остались заполненными идентификатор пользователя + еще одно поле");
            }
            var Application = await _context.applications.Include(x => x.activity).FirstOrDefaultAsync(x => x.id == guid);
            if (Application == null)
            {
                return NotFound("Нельзя удалить или редактировать не существующую заявку");
            }
            var submitted = await _context.submittedApplications.Include(x => x.application).FirstOrDefaultAsync(x => x.application == Application);
            if (submitted != null)
            {
                return BadRequest("Нельзя редактировать отправленные на рассмотрение заявки");
            }
            if (!Enum.TryParse(appDTO.activity, true, out EnumTypeActivity enumActivity) && appDTO.activity != null)
            {
                return BadRequest("Неправильный тип активности!.");
            }
            var activity = appDTO.activity == null ? null : await _context.activities.FirstOrDefaultAsync(x => x.activity == enumActivity);
            Application.activity = appDTO.activity == null ? Application.activity : activity!;
            Application.Name = appDTO.name == null ? Application.Name : appDTO.name;
            Application.Description = appDTO.description == null ? Application.Description : appDTO.description;
            Application.Outline = appDTO.outline == null ? Application.Outline : appDTO.outline;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        [Route("{guid}")]
        public async Task<IActionResult> DeleteApplication([FromRoute] Guid guid)
        {
            var Application = await _context.applications.FirstOrDefaultAsync(x => x.id == guid);
            if (Application == null)
            {
                return NotFound("Нельзя удалить или редактировать не существующую заявку");
            }
            var submitted = await _context.submittedApplications.FirstOrDefaultAsync(x => x.application == Application);
            if (submitted != null)
            {
                return BadRequest("Нельзя удалять отправленные на рассмотрение заявки");
            }
            var User = await _context.users.Include(x => x.currentApplication).FirstOrDefaultAsync(x => x.currentApplication == Application);
            if (User == null)
            {
                return BadRequest("Автор не найден");
            }
            User.currentApplication = null;
            _context.applications.Remove(Application);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        [Route("{guid}/submit")]
        public async Task<IActionResult> SubmittApplication([FromRoute] Guid guid)
        {
            var Application = await _context.applications.Include(x => x.activity).FirstOrDefaultAsync(x => x.id == guid);
            if (Application == null)
            {
                return NotFound("Нельзя отправить на рассмотрение не существующую заявку");
            }
            if (!CheckApponSubmitting(Application))
            {
                return BadRequest("Можно отправить на рассмотрение только заявки у которых заполнены все обязательные поля");
            }
            var submitted = await _context.submittedApplications.FirstOrDefaultAsync(x => x.application == Application);
            if (submitted != null)
            {
                return BadRequest("Нельзя отправлять отправленные на рассмотрение заявки");
            }
            SubmittedApplication subApp = new SubmittedApplication();
            subApp.application = Application;
            var User = await _context.users.Include(x => x.currentApplication).FirstOrDefaultAsync(x => x.currentApplication == Application);
            User.currentApplication = null;
            await _context.submittedApplications.AddAsync(subApp);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("/applications")]
        public async Task<ActionResult<List<Application>>> GetApplicationsWithQuery(DateTime? submittedAfter, DateTime? unsubmittedOlder)
        {
            List<Application> apps;
            if (submittedAfter != null && unsubmittedOlder != null)
            {
                return BadRequest("Запрос на получение поданных и не поданных заявок одновременно не корректный");
            }
            if (submittedAfter != null)
            {
                apps = await _context.submittedApplications.Include(x => x.application).ThenInclude(x => x.activity).Where(x => x.sumbittedAt > submittedAfter).Select(x => x.application).ToListAsync();
                return Ok(apps.Select(x => new { x.id, x.author, x.activity.activityName, x.Name, x.Description, x.Outline }));
            }
            if (unsubmittedOlder != null)
            {
                var submitted = await _context.submittedApplications.Select(x => x.application).ToListAsync();
                apps = await _context.applications.Include(x => x.activity).Where(x => x.createdAt > unsubmittedOlder).ToListAsync();
                var unsubmitted = apps.Except(submitted).Select(x => new {x.id, x.author, x.activity.activityName, x.Name,x.Description, x.Outline});
                return Ok(unsubmitted);
            }
            return BadRequest("Нет данных по времени");
        }
    }
}
