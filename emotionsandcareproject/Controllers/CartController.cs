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
        private readonly ICartService _cartaService;

        public CartaController(ICartService cartaService)
        {
            _cartaService = cartaService;
        }

        [HttpPost("{idUsuario}/AgregarCarta/{isPatient}/{isFirtTime}")]
        public ActionResult Add([FromBody] Cart carta, [FromRoute] int idUsuario, [FromRoute] bool isPatient, [FromRoute] bool isFirtTime)
        {
            var idCarta = _cartaService.Add(carta, idUsuario, isPatient, isFirtTime);

            if (idCarta == null) return BadRequest();
            return Ok(idCarta);
        }

        [HttpPost("{idCarta}/AgregarRespuesta/{isFirtTime}")]
        public ActionResult AddRespuesta([FromBody] CartAnswer respuesta, [FromRoute] int idCarta, [FromRoute] bool isFirtTime)
        {
            if (respuesta == null) return BadRequest();
            var result = _cartaService.AddRespuesta(respuesta, idCarta, isFirtTime);
            if (result == null) return BadRequest();
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
        public ActionResult Update([FromBody] Cart carta)
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