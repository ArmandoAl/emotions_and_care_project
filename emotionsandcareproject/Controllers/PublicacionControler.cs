using Business.Contracts;
using Business.Implementations;
using Domain;
using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PublicacionController : Controller
    {
        private readonly IPublicacionService _publicacionService;

        public PublicacionController(IPublicacionService publicacionService)
        {
            _publicacionService = publicacionService;
        }

        [HttpPost]
        public Task<ActionResult> Add([FromBody] Publicacion publicacion)
        {
            int idPublicacion = _publicacionService.Add(publicacion);

            if (idPublicacion == 0) return Task.FromResult<ActionResult>(BadRequest());
            return Task.FromResult<ActionResult>(Ok(idPublicacion));
        }

        [HttpPost("{idPublicacion}/AgregarComentario")]
        public Task<ActionResult> AddComentario([FromBody] Comentario comentario, [FromRoute] int idPublicacion)
        {
            if (comentario == null) return Task.FromResult<ActionResult>(BadRequest());
            bool result = _publicacionService.AddComent(idPublicacion, comentario);
            if (result == false) return Task.FromResult<ActionResult>(BadRequest());
            return Task.FromResult<ActionResult>(Ok(result));
        }

        [HttpPost("{idPublicacion}/ElimarComentario/{idComment}")]
        public Task<ActionResult> DeleteComentario([FromRoute] int idPublicacion, [FromRoute] int idComment)
        {
            if (idPublicacion == 0 || idComment == 0) return Task.FromResult<ActionResult>(BadRequest());
            bool result = _publicacionService.RemoveComent(idPublicacion, idComment);
            if (result == false) return Task.FromResult<ActionResult>(BadRequest());
            return Task.FromResult<ActionResult>(Ok(result));
        }


        [HttpPost("{idPublicacion}/AgregarLike")]
        public Task<ActionResult> AddLike([FromRoute] int idPublicacion)
        {
            if (idPublicacion == 0) return Task.FromResult<ActionResult>(BadRequest());
            bool result = _publicacionService.AddLike(idPublicacion);
            if (result == false) return Task.FromResult<ActionResult>(BadRequest());
            return Task.FromResult<ActionResult>(Ok(result));
        }

        [HttpDelete("{idPublicacion}")]
        public Task<ActionResult> Delete([FromRoute] int idPublicacion)
        {
            if (idPublicacion == 0) return Task.FromResult<ActionResult>(BadRequest());
            bool result = _publicacionService.Delete(idPublicacion);
            if (result == false) return Task.FromResult<ActionResult>(BadRequest());
            return Task.FromResult<ActionResult>(Ok(result));
        }

        [HttpGet("{idPublicacion}")]
        public Task<ActionResult> Get([FromRoute] int idPublicacion)
        {
            if (idPublicacion == 0) return Task.FromResult<ActionResult>(BadRequest());
            var result = _publicacionService.Get(idPublicacion);
            if (result == null) return Task.FromResult<ActionResult>(NotFound());
            return Task.FromResult<ActionResult>(Ok(result));
        }

        [HttpGet("GetPost")]
        public Task<ActionResult> GetAll()
        {
            var result = _publicacionService.GetPublicaciones();
            if (result == null) return Task.FromResult<ActionResult>(NotFound());
            return Task.FromResult<ActionResult>(Ok(result));
        }

        [HttpPost("{idPublicacion}/QuitarLike")]
        public Task<ActionResult> RemoveLike([FromRoute] int idPublicacion)
        {
            if (idPublicacion == 0) return Task.FromResult<ActionResult>(BadRequest());
            bool result = _publicacionService.RemoveLike(idPublicacion);
            if (result == false) return Task.FromResult<ActionResult>(BadRequest());
            return Task.FromResult<ActionResult>(Ok(result));
        }


        [HttpPut]
        public Task<ActionResult> Update([FromBody] Publicacion publicacion)
        {
            if (publicacion == null) return Task.FromResult<ActionResult>(BadRequest());
            bool result = _publicacionService.Update(publicacion);
            if (result == false) return Task.FromResult<ActionResult>(BadRequest());
            return Task.FromResult<ActionResult>(Ok(result));
        }
    }
}
