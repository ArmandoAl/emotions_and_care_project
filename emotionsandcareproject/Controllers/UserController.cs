using Business.Contracts;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UsuarioController : Controller
    {
        private readonly IPatientService _pacienteService; 
        private readonly ISpecialistService _especialistaService;

        public UsuarioController(IPatientService pacienteService, ISpecialistService especialistaService)
        {
            _pacienteService = pacienteService;
            _especialistaService = especialistaService;
        }

        [HttpPost("{email}/multiLogin/{password}")]

        public ActionResult MultiLogin(string email, string password)
        {
            if (email == null || password == null) return BadRequest();
            var isPaciente = _pacienteService.login(email, password);

            if (isPaciente == -1)
            {
                return BadRequest("Contraseña incorrecta.");
            }
            else
            {
                if (isPaciente != 0) return Ok(_pacienteService.Get(isPaciente));

                var isEspecialista = _especialistaService.login(email, password);

                if (isEspecialista == -1)
                {
                    return BadRequest("Contraseña incorrecta.");
                }
                else
                {
                    if (isEspecialista != 0) return Ok(_especialistaService.Get(isEspecialista));
                }

                return BadRequest("Usuario no encontrado.");
            }
        }

        [HttpPut("refreshToken/{id}/{token}")]
        public ActionResult RefreshToken(int id, string token)
        {
            return Ok("Hola");
            if (id == 0 || token == null) return BadRequest();
            var isPaciente = _pacienteService.Get(id);

            if (isPaciente == null)
            {
                var isEspecialista = _especialistaService.Get(id);

                if (isEspecialista == null) return BadRequest("Usuario no encontrado.");

                _especialistaService.refreshToken(id, token);
                return Ok(_especialistaService.Get(id));
            }

            _pacienteService.refreshToken(id, token);
            return Ok(_pacienteService.Get(id));
        }
    }
}
