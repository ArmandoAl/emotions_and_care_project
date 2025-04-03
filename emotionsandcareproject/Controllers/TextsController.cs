using Business.Contracts;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TextsController : Controller
    {
        private readonly ITextService _service;

        public TextsController(ITextService service)
        {
            _service = service;
        }

        [HttpPost]
        public Task<ActionResult> Add(InAppText text)
        {
            if (text == null) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.Add(text);
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

        [HttpPut("Edit/{id}")]
        public Task<ActionResult> Update(int id, InAppText text)
        {
            if (id < 1) return Task.FromResult<ActionResult>(BadRequest());
            if (text == null) return Task.FromResult<ActionResult>(BadRequest());
            text.textId = id;
            var result = _service.Update(id, text);
            if (!result) return Task.FromResult<ActionResult>(BadRequest());
            return Task.FromResult<ActionResult>(Ok(result));
        }

        [HttpGet("GET-ALL")]
        public Task<ActionResult> GetAll()
        {
            var result = _service.GetAll();
            if (result == null || result.Count == 0) return Task.FromResult<ActionResult>(NotFound());
            return Task.FromResult<ActionResult>(Ok(result));
        }
    }
}