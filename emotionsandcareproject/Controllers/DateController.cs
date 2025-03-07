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
        private readonly IDateService _service;
        private readonly IPatientService _pacienteService;
        private readonly ISpecialistService _especialistaService;
        private readonly INotificationService _notificacionService;

       
        public CitaController(IDateService service, 
               IPatientService pacienteService, ISpecialistService especialistaService, INotificationService notificacionService
            )
        {
            _service = service;
            _pacienteService = pacienteService;
            _especialistaService = especialistaService;
            _notificacionService = notificacionService;
        }

        [HttpPost("{idPaciente}/AgregarCita/{idEspecialista}/{isFirtTime}")]
        public ActionResult Add([FromRoute] int idPaciente, [FromBody] Date cita, [FromRoute] int idEspecialista, [FromRoute] bool isFirtTime)

        {
            if (cita == null) return BadRequest();
            Console.WriteLine("Cita: " + cita);
            var result = _service.AddDate(cita, idPaciente, idEspecialista, isFirtTime);

            Console.WriteLine("Resultado: " + result);
            if (result == null) return BadRequest();
            return Ok(result);
        }

        [HttpPost("{idEspecialista}/AgregarCitaEspecialista/{idPaciente}")]
        public ActionResult AddDateSpecialist([FromBody] Date cita, [FromRoute] int idEspecialista, [FromRoute] int idPaciente)

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
            var result = _service.GetDate(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (id < 1) return BadRequest();
            var result = _service.DeleteDate(id);
            if (!result) return BadRequest();
            return Ok(result);
        }

        [HttpPut]
        public ActionResult Update(Date cita)
        {
            if (cita == null) return BadRequest();
            var result = _service.UpdateDate(cita);
            if (!result) return BadRequest();
            return Ok(result);
        }

        [HttpGet("{idPaciente}/Citas")]
        public ActionResult GetCitasByPaciente(int idPaciente)
        {
            if (idPaciente < 1) return BadRequest();
            var result = _service.GetDatesPorPaciente(idPaciente);
            if (result == null) return NotFound();
            return Ok(result);
        }


        [HttpGet("{idSpecialist}/CitasDelEspecialist")]
        public ActionResult GetCitasBySpecialist(int idSpecialist)
        {
            if (idSpecialist < 1) return BadRequest();
            var result = _service.GetDatesPorEspecialista(idSpecialist);
            if (result == null) return NotFound();
            return Ok(result);
        }


        [HttpPut("{idCita}/ConfirmarCitaPorPaciente/{idPaciente}")]
        public async Task<ActionResult> ConfirmarCitaPorPaciente(int idCita, int idPaciente)
        {
            if (idCita < 1 || idPaciente < 1) return BadRequest();
            var result = _service.confirmarDatePorPaciente(idCita, idPaciente);
            if (!result) return BadRequest();

            var paciente = _pacienteService.Get(idPaciente);

            var message = new Message()
            {
                Notification = new Notification
                {
                    Title = "Agenda",
                    Body = "El paciente " + paciente!.name + " ha agendado una cita"

                },
                Token = _especialistaService.GetByToken(paciente.specialist!.relationalToken)!.token
            };

            await FirebaseMessaging.DefaultInstance.SendAsync(message);
            return Ok(result);
        }

        [HttpPut("{idCita}/CancelarCitaPorPaciente/{idPaciente}")]
        public async Task<ActionResult> CancelarCitaPorPaciente(int idCita, int idPaciente)
        {
            if (idCita < 1 || idPaciente < 1) return BadRequest();
            var result = _service.cancelarDatePorPaciente(idCita, idPaciente);
            if (!result) return BadRequest();
            var paciente = _pacienteService.Get(idPaciente);

            var message = new Message()
            {
                Notification = new Notification
                {
                    Title = "Agenda",
                    Body = "El paciente ha cancelado la cita"
                },
                Token = _especialistaService.GetByToken(paciente!.specialist!.relationalToken)!.token
            };

            await FirebaseMessaging.DefaultInstance.SendAsync(message);
            return Ok(result);
        }

        [HttpPut("{idCita}/ConfirmarCitaPorEspecialista/{idEspecialista}/{idPaciente}")]
        public async Task<ActionResult> ConfirmarCitaPorEspecialista(int idCita, int idEspecialista, int idPaciente)
        {
            if (idCita < 1 || idEspecialista < 1) return BadRequest();
            var result = _service.confirmarDatePorEspecialista(idCita, idEspecialista);
            if (!result) return BadRequest();

            var notificacion = new NotificationModel()
            {
                Titulo = "Agenda",
                notificationType = NotificationType.ReminderNotification,
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
            var result = _service.cancelarDatePorEspecialista(idCita, idEspecialista);
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

