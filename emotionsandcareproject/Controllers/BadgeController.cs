using Business.Contracts;
using Business.Implementations;
using Domain;
using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class BadgeController : Controller
    {
        private readonly IBadgeService _badgeService;

        public BadgeController(IBadgeService badgeService)
        {
            _badgeService = badgeService;
        }

        [HttpPost]
        public ActionResult Add([FromBody] Badge badge)
        {
            if (badge == null) return BadRequest();
            var result = _badgeService.AddBadge(badge);
            if (result == 0) return BadRequest();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            if (id < 1) return BadRequest();
            var result = _badgeService.GetBadge(id);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}