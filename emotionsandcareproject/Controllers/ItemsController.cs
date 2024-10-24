using Business;
using Business.Contracts;
using Business.Implementations;
using Domain;
using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ItemsController : Controller
    {

        private readonly IItemsService _itemsService;

        public ItemsController(IItemsService itemsService)
        {
            _itemsService = itemsService;
        }

        [HttpPost("AddFlor")]
        public ActionResult AddFlor([FromBody] Flower flor)
        {
            int idFlor = _itemsService.AddFlor(flor);

            if (idFlor == 0) return BadRequest();
            return Ok(idFlor);
        }

        [HttpPost("AddSticker")]
        public ActionResult AddSticker([FromBody] Sticker sticker)
        {
            int idSticker = _itemsService.AddSticker(sticker);

            if (idSticker == 0) return BadRequest();
            return Ok(idSticker);
        }

        [HttpGet("GetAllFlores")]
        public ActionResult GetAllFlores()
        {
            var result = _itemsService.GetAllFlores();
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("GetAllStickers")]
        public ActionResult GetAllStickers()
        {
            var result = _itemsService.GetAllStickers();
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("GetFlor/{id}")]
        public ActionResult GetFlor([FromRoute] int id)
        {
            if (id < 1) return BadRequest();
            var result = _itemsService.GetFlor(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("GetSticker/{id}")]
        public ActionResult GetSticker([FromRoute] int id)
        {
            if (id < 1) return BadRequest();
            var result = _itemsService.GetSticker(id);
            if (result == null) return NotFound();
            return Ok(result);
        }



        [HttpPost("AddFlowersToPatient/{idPatient}/{indexStart}/{indexEnd}")] 
        public ActionResult AddFlorToPatient([FromRoute] int idPatient, [FromRoute] int indexStart, [FromRoute] int indexEnd)
        {
            if (idPatient <= 0) return BadRequest();
            var result = _itemsService.AddFlowersToPatient(idPatient, indexStart, indexEnd);
            if (result == 0) return BadRequest();
            return Ok(result);
        }



    }
}