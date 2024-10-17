using Business.Contracts;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class NotaController : Controller
    {
        private readonly INoteService _service;

        public NotaController(INoteService service)
        {
            _service = service;
        }

        [HttpPost("{idPaciente}/AgregarNota/{isFirstTime}")]
        public Task<ActionResult> Add([FromRoute] int idPaciente, [FromRoute] bool isFirstTime,
         [FromBody] Note nota)
        {

            Console.WriteLine("Nota: " + nota);
            if (nota == null) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.AddNota(nota, idPaciente, isFirstTime);
            if (result == null) return Task.FromResult<ActionResult>(BadRequest());
            return Task.FromResult<ActionResult>(Ok(result));
        }

        [HttpGet("{id}/Get/{idPaciente}")]
        public Task<ActionResult> Get([FromRoute] int id, [FromRoute] int idPaciente)
        {
            if (id < 1) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.GetNota(id, idPaciente);
            if (result == null) return Task.FromResult<ActionResult>(NotFound());
            return Task.FromResult<ActionResult>(Ok(result));
        }

        [HttpDelete("{id}/Delete/{idPaciente}")]
        public Task<ActionResult> Delete([FromRoute] int id, [FromRoute] int idPaciente)
        {
            if (id < 1) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.DeleteNota(id, idPaciente);
            if (!result) return Task.FromResult<ActionResult>(BadRequest());
            return Task.FromResult<ActionResult>(Ok(result));
        }

        [HttpPut("{idPaciente}/Update")]    
        public Task<ActionResult> Update(Note nota, [FromRoute] int idPaciente)
        {
            if (nota == null) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.UpdateNota(nota, idPaciente);
            if (!result) return Task.FromResult<ActionResult>(BadRequest());
            return Task.FromResult<ActionResult>(Ok(result));
        }

        [HttpGet("{idPaciente}/Notas")]
        public Task<ActionResult> GetNotasByPaciente([FromRoute] int idPaciente)
        {
            if (idPaciente < 1) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.GetNotas(idPaciente);
            if (result == null) return Task.FromResult<ActionResult>(NotFound());
            return Task.FromResult<ActionResult>(Ok(result));
        }
    }   

 }

