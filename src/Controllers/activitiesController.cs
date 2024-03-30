using ConferenceService.DBContext.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class activitiesController : ControllerBase
    {
        private readonly DBContext.DatabaseContext _context;
        public activitiesController(DBContext.DatabaseContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Activity>>> GetActivities()
        {
            var activities = _context.activities.Select(x => new { activity = x.activity.ToString(), x.description }).ToList();
            return Ok(activities);
        }
    }
}
