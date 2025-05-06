using Business.Contracts;
using Business.Implementations;
using Domain;
using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class RecomendacionController : Controller
    {
        private readonly IRecomendationService _recomendacionService;

        public RecomendacionController(IRecomendationService recomendacionService)
        {
            _recomendacionService = recomendacionService;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Recomendation recomendacion)
        {
            if (recomendacion == null) return BadRequest();
            return Ok(_recomendacionService.Add(recomendacion));
        }

        [HttpGet("GetAllRecomendacion")]
        public IActionResult Get()
        {
            return Ok(_recomendacionService.GetRecomentaciones());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_recomendacionService.Get(id));
        }

        [HttpPut]
        public IActionResult Put([FromBody] Recomendation recomendacion)
        {
            _recomendacionService.Update(recomendacion);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _recomendacionService.Delete(id);
            return Ok();
        }

        [HttpPost("{recommendationId}/recomendationCompleted/{userId}")]
        public IActionResult recomendationCompleted(int recommendationId, int userId)
        {
            if (recommendationId <= 0 || userId <= 0) return BadRequest();
            return Ok(_recomendacionService.recomendationCompleted(recommendationId, userId));
        }

        [HttpGet("GetCompletedRecomendations/{userId}")]
        public IActionResult GetCompletedRecomendations(int userId)
        {
            if (userId <= 0) return BadRequest();
            var recomendations = _recomendacionService.GetCompletedRecomendations(userId);
            if (recomendations == null) return NotFound();
            return Ok(recomendations);
        }
    }

}