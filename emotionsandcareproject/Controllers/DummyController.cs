using Business.Contracts;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DummyController : Controller
    {
        private readonly IDummyService _service;

        public DummyController(IDummyService service)
        {
            _service = service;
        }

        [HttpPost]
        public ActionResult Add([FromBody] DummyUser dummy)
        {
            if (dummy == null) return BadRequest();
            var result = _service.Add(dummy);
            if (result == 0) return BadRequest();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            if (id < 1) return BadRequest();
            var result = _service.Get(id);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}