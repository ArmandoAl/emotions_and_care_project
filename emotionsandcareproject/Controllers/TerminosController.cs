using Business.Contracts;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TerminosController : Controller
    {
        private readonly ITerminosYCondicionesService _service;

        public TerminosController(ITerminosYCondicionesService service)
        {
            _service = service;
        }

        [HttpPost]
        public Task<ActionResult> Add(TerminosYCondiciones terminosYCondiciones)
        {
            if (terminosYCondiciones == null) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.AddTerminosYCondiciones(terminosYCondiciones);
            if (result == 0) return Task.FromResult<ActionResult>(BadRequest());
            return Task.FromResult<ActionResult>(Ok(result));
        }

        [HttpGet("{id}")]
        public Task<ActionResult> Get(int id)
        {
            if (id < 1) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.GetTerminosYCondiciones(id);
            if (result == null) return Task.FromResult<ActionResult>(NotFound());
            return Task.FromResult<ActionResult>(Ok(result));
        }

        [HttpDelete("{id}")]
        public Task<ActionResult> Delete(int id)
        {
            if (id < 1) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.DeleteTerminosYCondiciones(id);
            if (!result) return Task.FromResult<ActionResult>(BadRequest());
            return Task.FromResult<ActionResult>(Ok(result));
        }

        [HttpPut]
        public Task<ActionResult> Update(TerminosYCondiciones terminosYCondiciones)
        {
            if (terminosYCondiciones == null) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.UpdateTerminosYCondiciones(terminosYCondiciones);
            if (!result) return Task.FromResult<ActionResult>(BadRequest());
            return Task.FromResult<ActionResult>(Ok(result));
        }
    }
 }
