using Business.Contracts;
using Business.Implementations;
using Domain;
using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CartaController : Controller
    {
        private readonly ICartaService _cartaService;

        public CartaController(ICartaService cartaService)
        {
            _cartaService = cartaService;
        }

        [HttpPost("{idUsuario}/AgregarCarta/{isPatient}")]
        public ActionResult Add([FromBody] Carta carta, [FromRoute] int idUsuario, [FromRoute] bool isPatient)
        {
            int idCarta = _cartaService.Add(carta, idUsuario, isPatient);

            if (idCarta == 0) return BadRequest();
            return Ok(idCarta);
        }

        [HttpPost("{idCarta}/AgregarRespuesta")]
        public ActionResult AddRespuesta([FromBody] RespuestaCarta respuesta, [FromRoute] int idCarta)
        {
            if (respuesta == null) return BadRequest();
            bool result = _cartaService.AddRespuesta(respuesta, idCarta);
            if (result == false) return BadRequest();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            if (id < 1) return BadRequest();
            var result = _cartaService.Get(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (id < 1) return BadRequest();
            bool result = _cartaService.Delete(id);
            if (result == false) return BadRequest();
            return Ok(result);
        }

        [HttpGet("{idUsuario}/Cartas/{isPatient}")]
        public ActionResult GetAllByUser([FromRoute] int idUsuario, [FromRoute] bool isPatient)
        {
            if (idUsuario < 1) return BadRequest();
            var result = _cartaService.GetAllByUser(idUsuario, isPatient);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPut]
        public ActionResult Update([FromBody] Carta carta)
        {
            if (carta == null) return BadRequest();
            bool result = _cartaService.Update(carta);
            if (result == false) return BadRequest();
            return Ok(result);
        }

        [HttpGet("{idUsuario}/initCarts")]

        public ActionResult initCarts([FromRoute] int idUsuario)
        {
            if (idUsuario < 1) return BadRequest();
            var result = _cartaService.initCarts(idUsuario);
            if (result == null) return NotFound();
            return Ok(result);
        }

    }  

}