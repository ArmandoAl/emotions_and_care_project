using Business.Contracts;
using Business.Implementations;
using Domain;
using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AchievementController : Controller
    {
        private readonly IAchievementService _achievementService;

        public AchievementController(IAchievementService achievementService)
        {
            _achievementService = achievementService;
        }

        [HttpPost]
        public ActionResult Add([FromBody] Achievement achievement)
        {
            if (achievement == null) return BadRequest();
            var result = _achievementService.AddAchievement(achievement);
            if (result == 0) return BadRequest();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            if (id < 1) return BadRequest();
            var result = _achievementService.GetAchievement(id);
            if (result == null) return NotFound();
            return Ok(result);
        }
        [HttpGet]
        public ActionResult GetAll()
        {
            var result = _achievementService.GetAllAchievements();
            if (result == null || !result.Any()) return NotFound();
            return Ok(result);
        }
    }
}