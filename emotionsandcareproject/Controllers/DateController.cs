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

        [HttpPost("{idPaciente}/AgregarCita/{idEspecialista}")]
        public async Task<ActionResult> Add([FromRoute] int idPaciente, [FromBody] Date cita, [FromRoute] int idEspecialista)

        {
            if (cita == null) return BadRequest();
            var result = _service.AddDate(cita, idPaciente, idEspecialista);

            try {
                var message = new Message()
            {
                Notification = new Notification
                {
                    Title = "Agenda",
                    Body = "El paciente " + _pacienteService.Get(idPaciente)!.name + " ha agendado una cita"
                },
                Data = new Dictionary<string, string>()
                {
                    { "module", "schedule"},
                    { "event", "newRequest"},
                },
                Token = _especialistaService.Get(idEspecialista)!.token
            };

            await FirebaseMessaging.DefaultInstance.SendAsync(message);
            }  
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            if (result == null) return BadRequest();
            return Ok(result);
        }

        [HttpPost("{idEspecialista}/AgregarCitaEspecialista/{idPaciente}")]
       public async Task<ActionResult> AddDateSpecialist([FromBody] Date cita, [FromRoute] int idEspecialista, [FromRoute] int idPaciente)

        {
            if (cita == null) return BadRequest();
            var result = _service.AddDateSpecialist(cita, idEspecialista, idPaciente);


            try {
                var message = new Message()
            {
                Notification = new Notification
                {
                    Title = "Agenda",
                    Body = "Tu especialista " + _especialistaService.Get(idEspecialista)!.name + " ha agendado una cita, puedes consultarla en tu agenda"
                },
                Data = new Dictionary<string, string>()
                {  { "module", "schedule"},
                    { "event", "newDateBySpecialist"},
                },
                Token = _especialistaService.Get(idEspecialista)!.token
            };

            var paciente = _pacienteService.Get(idPaciente);

             if(paciente!.settings!.notificationsActive) {
                await FirebaseMessaging.DefaultInstance.SendAsync(message);
             }
            }  
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


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

            if (paciente!.specialist == null) return BadRequest();
           
            try {
                var message = new Message()
            {
                Notification = new Notification
                {
                    Title = "Agenda",
                    Body = "Tu cita ha sido confirmada por el paciente"
                },
                Data = new Dictionary<string, string>()
                {  { "module", "schedule"},
                    { "event", "dateConfirmedByPatient"},
                },
                Token = _especialistaService.Get(paciente!
                    .specialist!.userId
                )!.token
            };

            await FirebaseMessaging.DefaultInstance.SendAsync(message);
            }  
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return Ok(result);
        }

        [HttpPut("{idCita}/CancelarCitaPorPaciente/{idPaciente}")]
        public async Task<ActionResult> CancelarCitaPorPaciente(int idCita, int idPaciente)
        {
            if (idCita < 1 || idPaciente < 1) return BadRequest();
            var result = _service.cancelarDatePorPaciente(idCita, idPaciente);
            if (!result) return BadRequest();
            var paciente = _pacienteService.Get(idPaciente);

            try {
                var message = new Message()
            {
                Notification = new Notification
                {
                    Title = "Agenda",
                    Body = "Tu cita ha sido cancelada por el paciente"
                },
                Data = new Dictionary<string, string>()
                {  { "module", "schedule"},
                    { "event", "dateCanceledByPatient"},
                },
                Token = _especialistaService.Get(paciente!
                    .specialist!.userId
                )!.token
            };

            await FirebaseMessaging.DefaultInstance.SendAsync(message);
            }  
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

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

            _notificacionService.AddNotificacion(notificacion, idPaciente);

            var paciente = _pacienteService.Get(idPaciente);

            if (paciente == null) return BadRequest();
        try {
                var message = new Message()
            {
                Notification = new Notification
                {
                    Title = "Agenda",
                    Body = "Tu cita ha sido confirmada por el especialista"
                },
                Data = new Dictionary<string, string>()
                {  { "module", "schedule"},
                    { "event", "dateConfirmedBySpecialist"},
                
                },
                Token = _pacienteService.Get(paciente!.userId
                )!.token
            };

              if(paciente!.settings!.notificationsActive) {
                await FirebaseMessaging.DefaultInstance.SendAsync(message);
              }
            }  
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return Ok(result);
        }

        [HttpPut("{idCita}/CancelarCitaPorEspecialista/{idEspecialista}/{idPaciente}")]
        public async Task<ActionResult> CancelarCitaPorEspecialista(int idCita, int idEspecialista, int idPaciente)
        {
            if (idCita < 1 || idEspecialista < 1) return BadRequest();
            var result = _service.cancelarDatePorEspecialista(idCita, idEspecialista);
            if (!result) return BadRequest();


            var paciente = _pacienteService.Get(idPaciente);

            if (paciente == null) return BadRequest();

               try {
                var message = new Message()
            {
                Notification = new Notification
                {
                    Title = "Agenda",
                    Body = "Tu cita ha sido cancelada por el especialista"
                },
                Data = new Dictionary<string, string>()
                {  { "module", "schedule"},
                    { "event", "dateCanceledBySpecialist"},
                    
                },
                Token = _pacienteService.Get(paciente!.userId
                )!.token
            };


              if(paciente!.settings!.notificationsActive) {
                await FirebaseMessaging.DefaultInstance.SendAsync(message);
              }
            }  
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return Ok(result);
        }

        [HttpPut("{patientId}/actualizarCita/{isFromSpecialist}/{specialistId}")]
        public async Task<ActionResult> Update([FromBody] Date cita, 
        [FromRoute] int patientId,
        [FromRoute] bool isFromSpecialist, [FromRoute] int specialistId)
        {
            if (cita == null) return BadRequest();
            var result = _service.UpdateDate(cita);

            var specialist = _especialistaService.Get(specialistId);

              try {
                var message = new Message()
            {
                Notification = new Notification
                {
                    Title = "Agenda",
                    Body = isFromSpecialist ? "El paciente " + _pacienteService.Get(patientId)!.name + " ha actualizado una cita" : "Tu especialista ha actualizado tu cita cita"
                },
                Data = new Dictionary<string, string>()
                {  { "module", "schedule"},
                    { "event", "dateUpdated"},
                },
                Token = isFromSpecialist == false ? _pacienteService.Get(patientId)!.token : _especialistaService.Get(specialist!.userId)!.token
            };

            var paciente = _pacienteService.Get(patientId);
                if(paciente!.settings!.notificationsActive && isFromSpecialist == false) {
                    await FirebaseMessaging.DefaultInstance.SendAsync(message);
                }  
              }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            if (!result) return BadRequest();
            return Ok(result);
        }


        //put, marcar cita como completada
        [HttpPut("{idCita}/marcarCitaComoCompletada")]
        public ActionResult UpdateStatusCita([FromBody] Date cita, [FromRoute] int idCita)
        {
            if (cita == null) return BadRequest();
            var result = _service.UpdateStatusCita(cita);
            if (!result) return BadRequest();
            return Ok(result);
        }
    }
}

