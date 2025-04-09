using Business.Contracts;
using Domain;
using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class NotificacionController : Controller
    {
        private readonly INotificationService _service;
        private readonly IPatientService _pacienteService;
        private readonly ISpecialistService _especialistaService;

        public NotificacionController(INotificationService service, 
              IPatientService pacienteService, ISpecialistService especialistaService
            )
        {
            _service = service;
            _pacienteService = pacienteService;
            _especialistaService = especialistaService;
        }

        [HttpPost("{idPaciente}/sendNotification")]
        public async Task<ActionResult> Add([FromBody] NotificationModel notificacion, [FromRoute] int idPaciente)
        {
            if (notificacion == null) return BadRequest();
            var result = _service.AddNotificacion(notificacion, idPaciente);
            if (result == 0) return BadRequest();

            var message = new Message()
            {
                Notification = new Notification
                {
                    Title = notificacion.Titulo,
                    Body = notificacion.Descripcion
                },
                Data = new Dictionary<string, string>()
                {
                    { "idNotificacion", result.ToString() }
                },
                Token = _pacienteService.GetByToken(idPaciente)
            };

            await FirebaseMessaging.DefaultInstance.SendAsync(message);


            return Ok(result);
        }

        [HttpGet("{id}")]
        public Task<ActionResult> Get(int id)
        {
            if (id < 1) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.GetNotificacion(id);
            if (result == null) return Task.FromResult<ActionResult>(NotFound());
            return Task.FromResult<ActionResult>(Ok(result));
        }

        [HttpDelete("{id}")]
        public Task<ActionResult> Delete(int id)
        {
            if (id < 1) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.DeleteNotificacion(id);
            if (!result) return Task.FromResult<ActionResult>(BadRequest());
            return Task.FromResult<ActionResult>(Ok(result));
        }

        [HttpPost("{idNotificacion}/postpone")]
        public Task<ActionResult> Postpone([FromRoute] int idNotificacion)
        {
            if (idNotificacion < 1) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.postponeNotification(idNotificacion);
            if (!result) return Task.FromResult<ActionResult>(BadRequest());
            return Task.FromResult<ActionResult>(Ok(result));
        }

        [HttpPut]
        public Task<ActionResult> Update(NotificationModel notificacion)
        {
            if (notificacion == null) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.UpdateNotificacion(notificacion);
            if (!result) return Task.FromResult<ActionResult>(BadRequest());
            return Task.FromResult<ActionResult>(Ok(result));
        }

        [HttpGet("{idPaciente}/init")]
        public Task<ActionResult> GetNotificacionesByPaciente([FromRoute] int idPaciente)
        {
            if (idPaciente < 1) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.init(idPaciente);
            if (result == null) return Task.FromResult<ActionResult>(NotFound());
            return Task.FromResult<ActionResult>(Ok(result));
        }


        [HttpPut("{idNotificacion}/UpdateDateEmision")]

        public Task<ActionResult> UpdateDateEmision([FromRoute] int idNotificacion)
        {
            if (idNotificacion < 1) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.updateDateEmision(idNotificacion);
            if (!result) return Task.FromResult<ActionResult>(BadRequest());
            return Task.FromResult<ActionResult>(Ok(result));
        }
    }
}
