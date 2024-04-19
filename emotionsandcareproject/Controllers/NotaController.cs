using Business.Contracts;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class NotaController : Controller
    {
        private readonly INotaService _service;

        public NotaController(INotaService service)
        {
            _service = service;
        }

        [HttpPost("{idPaciente}/AgregarNota")]
        public Task<ActionResult> Add([FromRoute] int idPaciente, [FromBody] Nota nota)
        {
            if (nota == null) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.AddNota(nota, idPaciente);
            if (result == 0) return Task.FromResult<ActionResult>(BadRequest());
            return Task.FromResult<ActionResult>(Ok(result));
        }

        [HttpGet("{id}")]
        public Task<ActionResult> Get(int id)
        {
            if (id < 1) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.GetNota(id);
            if (result == null) return Task.FromResult<ActionResult>(NotFound());
            return Task.FromResult<ActionResult>(Ok(result));
        }

        [HttpDelete("{id}")]
        public Task<ActionResult> Delete(int id)
        {
            if (id < 1) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.DeleteNota(id);
            if (!result) return Task.FromResult<ActionResult>(BadRequest());
            return Task.FromResult<ActionResult>(Ok(result));
        }

        [HttpPut]
        public Task<ActionResult> Update(Nota nota)
        {
            if (nota == null) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.UpdateNota(nota);
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

