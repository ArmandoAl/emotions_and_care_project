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
        private readonly IRecomendacionService _recomendacionService;

        public RecomendacionController(IRecomendacionService recomendacionService)
        {
            _recomendacionService = recomendacionService;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Recomendacion recomendacion)
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
        public IActionResult Put([FromBody] Recomendacion recomendacion)
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
    }

}