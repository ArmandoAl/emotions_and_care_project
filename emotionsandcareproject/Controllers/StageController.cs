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

    public class StageController : Controller
    {

        private readonly IStageService _service;

        public StageController(IStageService service)
        {
            _service = service;
        }

        [HttpPost]
        public ActionResult Add(Stage stage)
        {
            if (stage == null) return BadRequest();
            var result = _service.Add(stage);
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

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (id < 1) return BadRequest();
            var result = _service.Delete(id);
            if (!result) return BadRequest();
            return Ok(result);
        }

        [HttpPut]
        public ActionResult Update(Stage stage)
        {
            if (stage == null) return BadRequest();
            var result = _service.Update(stage);
            if (!result) return BadRequest();
            return Ok(result);
        }

        [HttpPost]
        public ActionResult AddRequest(StageRequest stageRequest)
        {
            if (stageRequest == null) return BadRequest();
            var result = _service.AddRequest(stageRequest);
            if (result == 0) return BadRequest();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult GetRequest(int id)
        {
            if (id < 1) return BadRequest();
            var result = _service.GetRequest(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteRequest(int id)
        {
            if (id < 1) return BadRequest();
            var result = _service.DeleteRequest(id);
            if (!result) return BadRequest();
            return Ok(result);
        }

        [HttpPut]
        public ActionResult UpdateRequest(StageRequest stageRequest)
        {
            if (stageRequest == null) return BadRequest();
            var result = _service.UpdateRequest(stageRequest);
            if (!result) return BadRequest();
            return Ok(result);
        }



    }


}


