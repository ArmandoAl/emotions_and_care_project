using Business.Contracts;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspecialistaController : Controller
    {
        private readonly IEspecialistaService _service;

        public EspecialistaController(IEspecialistaService service)
        {
            _service = service;
        }

        [HttpPost]
        public Task<ActionResult> Add(AgregarEspecialista especialista)
        {
            if (especialista == null) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.Add(especialista);
            if (result == 0) return Task.FromResult<ActionResult>(BadRequest());
            return Task.FromResult<ActionResult>(Ok(result));
        }

        [HttpGet("{id}")]
        public Task<ActionResult> Get([FromRoute] int id)
        {
            if (id < 1) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.Get(id);
            if (result == null) return Task.FromResult<ActionResult>(NotFound());
            return Task.FromResult<ActionResult>(Ok(result));
        }

        [HttpDelete("{id}")]
        public Task<ActionResult> Delete([FromRoute] int id)
        {
            if (id < 1) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.Delete(id);
            if (!result) return Task.FromResult<ActionResult>(BadRequest());
            return Task.FromResult<ActionResult>(Ok(result));
        }

        [HttpPut]
        public Task<ActionResult> Update(Especialista especialista)
        {
            if (especialista == null) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.Update(especialista);
            if (!result) return Task.FromResult<ActionResult>(BadRequest());
            return Task.FromResult<ActionResult>(Ok(result));
        }

        [HttpPost("{id}/vincularPaciente/{tokenPaciente}")]
        public Task<ActionResult> VincularPaciente([FromRoute] int id, [FromRoute] string tokenPaciente)
        {
            if (id < 1 || tokenPaciente == null) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.vincularPaciente(id, tokenPaciente);
            if (!result) return Task.FromResult<ActionResult>(BadRequest());
            return Task.FromResult<ActionResult>(Ok(result));
        }


        [HttpPost("{id}/aceptarCita/{idCita}")]
        public Task<ActionResult> AceptarCita([FromRoute] int id, [FromRoute] int idCita)
        {
            if (id < 1 || idCita < 1) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.aceptarCita(id, idCita);
            return Task.FromResult<ActionResult>(Ok(result));
        }

        [HttpPost("{id}/rechazarCita/{idCita}")]

        public Task<ActionResult> RechazarCita([FromRoute] int id, [FromRoute] int idCita)
        {
            if (id < 1 || idCita < 1) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.rechazarCita(id, idCita);
            if (!result) return Task.FromResult<ActionResult>(BadRequest());
            return Task.FromResult<ActionResult>(Ok(result));
        }

        [HttpGet("{id}/pacientes")]
        public Task<ActionResult> GetPacientes([FromRoute] int id)
        {
            if (id < 1) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.GetPacientes(id);
            if (result == null) return Task.FromResult<ActionResult>(NotFound());
            return Task.FromResult<ActionResult>(Ok(result));
        }

        [HttpGet("{id}/solicitudesCitas")]
        public Task<ActionResult> GetSolicitudesCitas([FromRoute] int id)
        {
            if (id < 1) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.GetSolicitudesCitas(id);
            if (result == null) return Task.FromResult<ActionResult>(NotFound());
            return Task.FromResult<ActionResult>(Ok(result));
        }
    }
}
