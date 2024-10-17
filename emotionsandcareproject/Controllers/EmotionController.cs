using Business.Contracts;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class EmocionController : Controller
    {
        private readonly IEmotionSerrvice _service;

        public EmocionController(IEmotionSerrvice service)
        {
            _service = service;
        }

        [HttpPost]
        public ActionResult Add(Emotion emocion)
        {
            if (emocion == null) return BadRequest();
            var result = _service.AddEmotion(emocion);
            if (result == 0) return BadRequest();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            if (id < 1) return BadRequest();
            var result = _service.GetEmotion(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (id < 1) return BadRequest();
            var result = _service.DeleteEmotion(id);
            if (!result) return BadRequest();
            return Ok(result);
        }

        [HttpPut]
        public ActionResult Update(Emotion emocion)
        {
            if (emocion == null) return BadRequest();
            var result = _service.UpdateEmotion(emocion);
            if (!result) return BadRequest();
            return Ok(result);
        }

        [HttpGet("/Emociones")]
        public ActionResult GetEmociones()
        {
            var result = _service.GetEmotions();
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}