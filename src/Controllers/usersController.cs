using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ConferenceService.DBContext.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
namespace ConferenceService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usersController : ControllerBase
    {
        private readonly DBContext.DatabaseContext _context;
        public usersController(DBContext.DatabaseContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("{guid}/currentapplication")]
        public async Task<ActionResult<Application>> GetUserApplication([FromRoute] Guid guid)
        {
            var User = await _context.users.Include(x => x.currentApplication).ThenInclude(y => y.activity).FirstOrDefaultAsync(x => x.id.Equals(guid));
            if (User == null)
            {
                return NotFound("Автор не найден");
            }
            var Application = User.currentApplication;
            if (Application == null)
            {
                return NotFound("Заявки не найдено");
            }
            Enum.TryParse(Application.activity.activity.ToString(), true, out EnumTypeActivity enumActivity);
            return Ok(new { id = Application.id, author = Application.author, activity = enumActivity.ToString(), Application.Name, Application.Description, Application.Outline});
        }
    }
}
