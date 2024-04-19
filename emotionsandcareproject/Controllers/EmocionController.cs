using Business.Contracts;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class EmocionController : Controller
    {
        private readonly IEmocionSerrvice _service;

        public EmocionController(IEmocionSerrvice service)
        {
            _service = service;
        }

        [HttpPost]
        public ActionResult Add(Emocion emocion)
        {
            if (emocion == null) return BadRequest();
            var result = _service.AddEmocion(emocion);
            if (result == 0) return BadRequest();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            if (id < 1) return BadRequest();
            var result = _service.GetEmocion(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (id < 1) return BadRequest();
            var result = _service.DeleteEmocion(id);
            if (!result) return BadRequest();
            return Ok(result);
        }

        [HttpPut]
        public ActionResult Update(Emocion emocion)
        {
            if (emocion == null) return BadRequest();
            var result = _service.UpdateEmocion(emocion);
            if (!result) return BadRequest();
            return Ok(result);
        }

        [HttpGet("/Emociones")]
        public ActionResult GetEmociones()
        {
            var result = _service.GetEmociones();
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}