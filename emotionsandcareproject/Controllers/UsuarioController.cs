using Business.Contracts;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UsuarioController : Controller
    {
        private readonly IPacienteService _pacienteService; 
        private readonly IEspecialistaService _especialistaService;

        public UsuarioController(IPacienteService pacienteService, IEspecialistaService especialistaService)
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
    }
}
