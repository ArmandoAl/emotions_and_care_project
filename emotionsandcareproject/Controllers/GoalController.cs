using Business.contracts;
using Business.Contracts;
using Business.Implementations;
using Domain;
using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class LogroController : Controller
    {
        private readonly IGoalService _service;

        public LogroController(IGoalService service)
        {
            _service = service;
        }

        [HttpPost]
        public ActionResult Add(Goal logro)
        {
            if (logro == null) return BadRequest();
            var result = _service.AddLogro(logro);
            if (result == 0) return BadRequest();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            if (id < 1) return BadRequest();
            var result = _service.GetLogro(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (id < 1) return BadRequest();
            var result = _service.DeleteLogro(id);
            if (!result) return BadRequest();
            return Ok(result);
        }

        [HttpPut]
        public ActionResult Update(Goal logro)
        {
            if (logro == null) return BadRequest();
            var result = _service.UpdateLogro(logro);
            if (!result) return BadRequest();
            return Ok(result);
        }
    }
}