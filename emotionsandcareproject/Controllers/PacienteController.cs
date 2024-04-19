using Business.Contracts;
using Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PacienteController : Controller
    {
        private readonly IPacienteService _service;
        public PacienteController(IPacienteService service)
        {
            _service = service;
        }

        [HttpPost]
        public Task<ActionResult> Add(AgregarPaciente paciente)
        {
            if (paciente == null) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.Add(paciente);
            if (result == 0) return Task.FromResult<ActionResult>(BadRequest());
            return Task.FromResult<ActionResult>(Ok(result));
        }

        [HttpGet("{id}")]
        public Task<ActionResult> Get(int id)
        {
            if (id < 1) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.Get(id);
            if (result == null) return Task.FromResult<ActionResult>(NotFound());
            return Task.FromResult<ActionResult>(Ok(result));
        }

        [HttpDelete("{id}")]
        public Task<ActionResult> Delete(int id)
        {
            if (id < 1) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.Delete(id);
            if (!result) return Task.FromResult<ActionResult>(BadRequest());
            return Task.FromResult<ActionResult>(Ok(result));
        }

        [HttpPut]
        public Task<ActionResult> Update(Paciente paciente)
        {
            if (paciente == null) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.Update(paciente);
            if (!result) return Task.FromResult<ActionResult>(BadRequest());
            return Task.FromResult<ActionResult>(Ok(result));
        }

        [HttpPost("{id}/vincularEspecialista/{tokenEspecialista}")]
        public Task<ActionResult> VincularEspecialista(int id, string tokenEspecialista)
        {
            if (id < 1 || tokenEspecialista == null) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.VincularEspecialista(id, tokenEspecialista);
            if (!result) return Task.FromResult<ActionResult>(BadRequest());
            return Task.FromResult<ActionResult>(Ok(result));
        }   
    }       
}

