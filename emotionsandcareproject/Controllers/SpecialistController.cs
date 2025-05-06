using Business.Contracts;
using Domain;
using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspecialistaController : Controller
    {
        private readonly ISpecialistService _service;
        private readonly PushNotificationService _pushNotificationService;

        public EspecialistaController(ISpecialistService service, PushNotificationService pushNotificationService)
        {
            _service = service;
            _pushNotificationService = pushNotificationService;
        }

        [HttpPost]
        public Task<ActionResult> Add(AddSpecialist especialista)
        {
            if (especialista == null) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.Add(especialista);
            if (result == 0) return Task.FromResult<ActionResult>(BadRequest());
            return Task.FromResult<ActionResult>(Ok(result));
        }

        [HttpGet("{id}")]
        public Task<ActionResult> Get([FromRoute] int id)
        {
            if (id < 1) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.Get(id);
            if (result == null) return Task.FromResult<ActionResult>(NotFound());
            return Task.FromResult<ActionResult>(Ok(result));
        }

        [HttpDelete("{id}")]
        public Task<ActionResult> Delete([FromRoute] int id)
        {
            if (id < 1) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.Delete(id);
            if (!result) return Task.FromResult<ActionResult>(BadRequest());
            return Task.FromResult<ActionResult>(Ok(result));
        }

        [HttpPut]
        public Task<ActionResult> Update(Specialist especialista)
        {
            if (especialista == null) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.Update(especialista);
            return Task.FromResult<ActionResult>(Ok(result));
        }

      [HttpPost("{id}/aceptarSolicitud/{pacientId}")]
        public async Task<ActionResult> VincularPaciente([FromRoute] int id, [FromRoute] int pacientId)
        {
            if (id < 1 || pacientId < 1) return BadRequest();        

            // Intentar vincular al paciente
            var result = _service.aceptarSolicitud(id, pacientId);
            if (!result)
                return BadRequest("No se pudo vincular el paciente.");

            // Obtener el especialista
            var specialist = _service.Get(id);
            if (specialist == null)
                return NotFound("Especialista no encontrado.");


            var patients = _service.GetPacientes(id);
            var patient = patients?.FirstOrDefault(p => p.userId == pacientId);
            if (patient == null || string.IsNullOrEmpty(patient.token))
                return NotFound("Paciente no encontrado o token inválido.");
            
           try {
            var data = new Dictionary<string, string>
            {
                { "module", "sync" },
                { "type", "specialistSync" },
            };

             if(patient!.settings!.notificationsActive) {
                await _pushNotificationService.SendPushAsync("Nueva notificación", specialist.name +  " ha aceptado tu solicitud de vinculación. ¡Felicidades!", patient.token, data);
             }
           } catch (Exception e) {
                Console.WriteLine(e.Message);
            }

            return Ok(result);
        }

        //rechazar solicitud
        [HttpPost("{id}/rechazarSolicitud/{pacientId}")]
        public async Task<ActionResult> RechazarPaciente([FromRoute] int id, [FromRoute] int pacientId)
        {
            if (id < 1 || pacientId < 1) return BadRequest();

            // Intentar vincular al paciente
            var result = _service.rechazarSolicitud(id, pacientId);
            if (!result)
                return BadRequest("No se pudo vincular el paciente.");

            // Obtener el especialista
            var specialist = _service.Get(id);
            if (specialist == null)
                return NotFound("Especialista no encontrado.");

            var patients = _service.GetPacientes(id);
            var patient = patients?.FirstOrDefault(p => p.userId == pacientId);
            if (patient == null || string.IsNullOrEmpty(patient.token))
                return NotFound("Paciente no encontrado o token inválido.");

            try {
                var data = new Dictionary<string, string>
            {
                { "module", "sync" },
                { "type", "specialistSyncReject" },
            
            };

             if(patient!.settings!.notificationsActive) {
                await _pushNotificationService.SendPushAsync("Nueva notificación", specialist.name + " ha rechazado tu solicitud de vinculación.", patient.token, data);
             }
            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }

            return Ok(result);
        }


        [HttpPost("{id}/aceptarCita/{idCita}")]
        public Task<ActionResult> AceptarCita([FromRoute] int id, [FromRoute] int idCita)
        {
            if (id < 1 || idCita < 1) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.aceptarCita(id, idCita);
            return Task.FromResult<ActionResult>(Ok(result));
        }

        [HttpPost("{id}/rechazarCita/{idCita}")]

        public Task<ActionResult> RechazarCita([FromRoute] int id, [FromRoute] int idCita)
        {
            if (id < 1 || idCita < 1) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.rechazarCita(id, idCita);
            if (!result) return Task.FromResult<ActionResult>(BadRequest());
            return Task.FromResult<ActionResult>(Ok(result));
        }

        [HttpGet("{id}/pacientes")]
        public Task<ActionResult> GetPacientes([FromRoute] int id)
        {
            if (id < 1) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.GetPacientes(id);
            if (result == null) return Task.FromResult<ActionResult>(NotFound());
            return Task.FromResult<ActionResult>(Ok(result));
        }

        [HttpGet("{id}/solicitudesCitas")]
        public Task<ActionResult> GetSolicitudesCitas([FromRoute] int id)
        {
            if (id < 1) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.GetSolicitudesCitas(id);
            if (result == null) return Task.FromResult<ActionResult>(NotFound());
            return Task.FromResult<ActionResult>(Ok(result));
        }

        [HttpGet("listarEspecialistas/{offset}/{limit}")]
        public Task<ActionResult> ListarEspecialistas([FromRoute] int offset, [FromRoute] int limit)
        {
            if (offset < 0 || limit < 1) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.ListarEspecialists(offset, limit);
            if (result == null) return Task.FromResult<ActionResult>(NotFound());
            return Task.FromResult<ActionResult>(Ok(result));
        }

        //List<PatientRequest>? GetSolicitudesPacientes(int id)
        [HttpGet("{id}/solicitudesPacientes")]
        public Task<ActionResult> GetSolicitudesPacientes([FromRoute] int id)
        {
            if (id < 1) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.GetSolicitudesPacientes(id);
            if (result == null) return Task.FromResult<ActionResult>(NotFound());
            return Task.FromResult<ActionResult>(Ok(result));
        }   
    }
}



