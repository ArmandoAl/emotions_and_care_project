using Business.Contracts;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CuestionarioController : Controller
    {
        private readonly IQuestionnaireService _service;

        public CuestionarioController(IQuestionnaireService service)
        {
            _service = service;
        }

        [HttpPost]
        public ActionResult Add(Questionnaire cuestionario)
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

        [HttpDelete("{id}/delete/{idPaciente}")]
        public ActionResult Delete([FromRoute] int id, [FromRoute] int idPaciente)
        {
            if (id < 1) return BadRequest();
            var result = _service.DeleteCuestionario(id, idPaciente);
            if (!result) return BadRequest();
            return Ok(result);
        }

        [HttpPut]
        public ActionResult Update(Questionnaire cuestionario)
        {
            if (cuestionario == null) return BadRequest();
            var result = _service.UpdateCuestionario(cuestionario);
            if (!result) return BadRequest();
            return Ok(result);
        }

        [HttpPost("{idPaciente}/completarCuestionario/{idCuestionario}/{isFirstTime}")]
        public ActionResult completarCuestionario(int idPaciente, int idCuestionario,  bool isFirstTime,
         [FromBody] ListResponseModel respuestas)
        {
            if (idPaciente < 1 || idCuestionario < 1) return BadRequest();

            var result = _service.completarCuestionario(idCuestionario, idPaciente,
               
             respuestas.questions,  isFirstTime);

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

        [HttpPut("{idCuestionario}/changeVisibility/{idTestInfoModel}/{visible}/patient/{patientId}")]
        public ActionResult changeVisibility(int idCuestionario, int idTestInfoModel, bool visible, int patientId)
        {
            if (idCuestionario < 1 || idTestInfoModel < 1) return BadRequest();
            var result = _service.changeVisibility(
                patientId,
                idCuestionario, idTestInfoModel, visible);
            if (!result) return BadRequest();
            return Ok(result);
        }
    }

}