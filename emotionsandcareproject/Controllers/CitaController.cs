using Business.Contracts;
using Business.Implementations;
using Domain;
using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CitaController : Controller
    {
        private readonly ICitaService _service;
        private readonly IPacienteService _pacienteService;
        private readonly IEspecialistaService _especialistaService;
        private readonly INotificacionService _notificacionService;

        public CitaController(ICitaService service, 
               IPacienteService pacienteService, IEspecialistaService especialistaService, INotificacionService notificacionService
            )
        {
            _service = service;
            _pacienteService = pacienteService;
            _especialistaService = especialistaService;
            _notificacionService = notificacionService;
        }

        [HttpPost("{idPaciente}/AgregarCita/{idEspecialista}")]
        public ActionResult Add([FromRoute] int idPaciente, [FromBody] Cita cita, [FromRoute] int idEspecialista)

        {
            if (cita == null) return BadRequest();
            var result = _service.AddCita(cita, idPaciente, idEspecialista);
            if (result == 0) return BadRequest();
            return Ok(result);
        }

        [HttpPost("{idEspecialista}/AgregarCitaEspecialista/{idPaciente}")]
        public ActionResult AddDateSpecialist([FromBody] Cita cita, [FromRoute] int idEspecialista, [FromRoute] int idPaciente)

        {
            if (cita == null) return BadRequest();
            var result = _service.AddDateSpecialist(cita, idEspecialista, idPaciente);
            if (result == 0) return BadRequest();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            if (id < 1) return BadRequest();
            var result = _service.GetCita(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (id < 1) return BadRequest();
            var result = _service.DeleteCita(id);
            if (!result) return BadRequest();
            return Ok(result);
        }

        [HttpPut]
        public ActionResult Update(Cita cita)
        {
            if (cita == null) return BadRequest();
            var result = _service.UpdateCita(cita);
            if (!result) return BadRequest();
            return Ok(result);
        }

        [HttpGet("{idPaciente}/Citas")]
        public ActionResult GetCitasByPaciente(int idPaciente)
        {
            if (idPaciente < 1) return BadRequest();
            var result = _service.GetCitasPorPaciente(idPaciente);
            if (result == null) return NotFound();
            return Ok(result);
        }


        [HttpGet("{idSpecialist}/CitasDelEspecialist")]
        public ActionResult GetCitasBySpecialist(int idSpecialist)
        {
            if (idSpecialist < 1) return BadRequest();
            var result = _service.GetCitasPorEspecialista(idSpecialist);
            if (result == null) return NotFound();
            return Ok(result);
        }


        [HttpPut("{idCita}/ConfirmarCitaPorPaciente/{idPaciente}")]
        public async Task<ActionResult> ConfirmarCitaPorPaciente(int idCita, int idPaciente)
        {
            if (idCita < 1 || idPaciente < 1) return BadRequest();
            var result = _service.confirmarCitaPorPaciente(idCita, idPaciente);
            if (!result) return BadRequest();

            var paciente = _pacienteService.Get(idPaciente);

            var message = new Message()
            {
                Notification = new Notification
                {
                    Title = "Agenda",
                    Body = "El paciente " + paciente!.Nombre + " ha agendado una cita"

                },
                Token = _especialistaService.GetByToken(paciente.Especialista!.IdUsuario)
            };

            await FirebaseMessaging.DefaultInstance.SendAsync(message);
            return Ok(result);
        }

        [HttpPut("{idCita}/CancelarCitaPorPaciente/{idPaciente}")]
        public async Task<ActionResult> CancelarCitaPorPaciente(int idCita, int idPaciente)
        {
            if (idCita < 1 || idPaciente < 1) return BadRequest();
            var result = _service.cancelarCitaPorPaciente(idCita, idPaciente);
            if (!result) return BadRequest();
            var paciente = _pacienteService.Get(idPaciente);

            var message = new Message()
            {
                Notification = new Notification
                {
                    Title = "Agenda",
                    Body = "El paciente ha cancelado la cita"
                },
                Token = _especialistaService.GetByToken(paciente!.Especialista!.IdUsuario)
            };

            await FirebaseMessaging.DefaultInstance.SendAsync(message);
            return Ok(result);
        }

        [HttpPut("{idCita}/ConfirmarCitaPorEspecialista/{idEspecialista}/{idPaciente}")]
        public async Task<ActionResult> ConfirmarCitaPorEspecialista(int idCita, int idEspecialista, int idPaciente)
        {
            if (idCita < 1 || idEspecialista < 1) return BadRequest();
            var result = _service.confirmarCitaPorEspecialista(idCita, idEspecialista);
            if (!result) return BadRequest();

            var notificacion = new Notificacion()
            {
                Titulo = "Agenda",
                TipoNotificacion = TipoNotificacion.NotificacionRecordatorio,
                Descripcion = "Tu cita ha sido confirmada por el especialista"
            };

            var idNotificacion = _notificacionService.AddNotificacion(notificacion, idPaciente);

            var message = new Message()
            {
                Notification = new Notification
                {
                    Title = "Agenda",
                    Body = "Tu cita ha sido confirmada por el especialista"
                },
                Data = new Dictionary<string, string>()
                {
                    { "idNotificacion", idNotificacion.ToString()}
                },
                Token = _pacienteService.GetByToken(idPaciente)
            };

            await FirebaseMessaging.DefaultInstance.SendAsync(message);


            return Ok(result);
        }

        [HttpPut("{idCita}/CancelarCitaPorEspecialista/{idEspecialista}/{idPaciente}")]
        public async Task<ActionResult> CancelarCitaPorEspecialista(int idCita, int idEspecialista, int idPaciente)
        {
            if (idCita < 1 || idEspecialista < 1) return BadRequest();
            var result = _service.cancelarCitaPorEspecialista(idCita, idEspecialista);
            if (!result) return BadRequest();


            var message = new Message()
            {
                Notification = new Notification
                {
                    Title = "Agenda",
                    Body = "Tu cita ha sido confirmada por el especialista"
                },
                Token = _pacienteService.GetByToken(idPaciente)
            };

            await FirebaseMessaging.DefaultInstance.SendAsync(message);



            return Ok(result);
        }
    }
}

