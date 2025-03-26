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

    public class OppsController : Controller
    {
        private readonly IOppsService _service;

        public OppsController(IOppsService service)
        {
            _service = service;
        }

        [HttpPost]
        public Task<ActionResult> Add(OppsAdd opps)
        {
            if (opps == null) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.Add(opps);
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

        [HttpPost("{userId}/sakaNote")]
        public Task<ActionResult> AddSakaNote(SakaNotes note, int userId)
        {
            if (note == null || userId < 1) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.AddSakaNote(note, userId);
            if (result == 0) return Task.FromResult<ActionResult>(BadRequest());
            return Task.FromResult<ActionResult>(Ok(result));
        }
    }
}