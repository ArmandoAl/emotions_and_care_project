using Business.Contracts;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CuestionarioController : Controller
    {
        private readonly ICuestionarioService _service;

        public CuestionarioController(ICuestionarioService service)
        {
            _service = service;
        }

        [HttpPost]
        public ActionResult Add(Cuestionario cuestionario)
        {
            if (cuestionario == null) return BadRequest();
            var result = _service.AddCuestionario(cuestionario);
            if (result == 0) return BadRequest();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            if (id < 1) return BadRequest();
            var result = _service.GetCuestionario(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (id < 1) return BadRequest();
            var result = _service.DeleteCuestionario(id);
            if (!result) return BadRequest();
            return Ok(result);
        }

        [HttpPut]
        public ActionResult Update(Cuestionario cuestionario)
        {
            if (cuestionario == null) return BadRequest();
            var result = _service.UpdateCuestionario(cuestionario);
            if (!result) return BadRequest();
            return Ok(result);
        }

        [HttpPost("{idPaciente}/completarCuestionario/{idCuestionario}")]
        public ActionResult completarCuestionario(int idPaciente, int idCuestionario, [FromBody] ListResponseModel respuestas)
        {
            if (idPaciente < 1 || idCuestionario < 1) return BadRequest();
            var result = _service.completarCuestionario(idCuestionario, idPaciente, respuestas.preguntas);
            if (result == null) return BadRequest();
            return Ok(result);
        }

        [HttpGet("{idPaciente}/obtenerCuestionarios")]
        public ActionResult obtenerCuestionarios(int idPaciente)
        {
            if (idPaciente < 1) return BadRequest();
            var result = _service.GetCuestionariosInfo(idPaciente);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }

}