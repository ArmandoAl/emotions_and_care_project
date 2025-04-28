using Business.Contracts;
using Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PacienteController : Controller
    {
        private readonly IPatientService _service;
        private readonly ISpecialistService _specialistService;
        private readonly PushNotificationService _pushNotificationService;
        public PacienteController(IPatientService service, PushNotificationService pushNotificationService, ISpecialistService specialistService)
        {
            _service = service;
            _pushNotificationService = pushNotificationService;
            _specialistService = specialistService;
        }

        [HttpPost]
        public Task<ActionResult> Add(AddPatient paciente)
        {
            if (paciente == null) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.Add(paciente);
            if (result == 0) return Task.FromResult<ActionResult>(BadRequest());
            return Task.FromResult<ActionResult>(Ok(result));
        }

        [HttpGet("{id}")]
        public Task<ActionResult> Get(int id)
        {
            if (id < 1) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.Get(id);
            if (result == null) return Task.FromResult<ActionResult>(NotFound());
            return Task.FromResult<ActionResult>(Ok(result));
        }

        [HttpDelete("{id}")]
        public Task<ActionResult> Delete(int id)
        {
            if (id < 1) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.Delete(id);
            if (!result) return Task.FromResult<ActionResult>(BadRequest());
            return Task.FromResult<ActionResult>(Ok(result));
        }

        [HttpPut]
        public Task<ActionResult> Update(Patient paciente)
        {
            if (paciente == null) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.Update(paciente);
            if (!result) return Task.FromResult<ActionResult>(BadRequest());
            return Task.FromResult<ActionResult>(Ok(result));
        }

        [HttpPost("{id}/vincularEspecialista/{tokenEspecialista}")]
        public async Task<ActionResult> VincularEspecialista(int id, string tokenEspecialista)
        {
            if (id < 1 || tokenEspecialista == null) return BadRequest();
            var result = _service.VincularEspecialista(id, tokenEspecialista);

            try {
                 var data = new Dictionary<string, string>
                {
                    { "module", "patientRequest" },
                    { "event", "newRequest" }
                };

                var patient = _service.Get(id);
                var token = _specialistService.GetByToken(tokenEspecialista)?.token ?? "";
                var title = "¡Nueva solicitud de vinculación!";
                var body = patient!.name + " quiere vincularse contigo.";
                await _pushNotificationService.SendPushAsync(title, body, token,
                 data);
               } catch (Exception e) {
                   Console.WriteLine(e);
               }
         
            return Ok(result);
        }   

        //vincularDirecto
        [HttpPost("{id}/vincularDirecto/{tokenEspecialista}")]
         public async Task<ActionResult> VincularDirecto(int id, string tokenEspecialista)
        {
            if (id < 1 || tokenEspecialista == null) return BadRequest();
            var result = _service.VincularDirecto(id, tokenEspecialista);

            try {
                var data = new Dictionary<string, string>
                {
                    { "module", "sync" },
                    { "type", "patientSync" },
                };

            var patient = _service.Get(id);
            var token = _specialistService.GetByToken(tokenEspecialista)?.token ?? "";
            var specialist = _specialistService.GetByToken(tokenEspecialista);

            await _pushNotificationService.SendPushAsync("Nueva notificación", 
            patient!.name + " se ha vinculado contigo.", token, data);
          
            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }


            return Ok(result);
        }

        [HttpPut("{id}/MoficarConfiguracionNotificaciones/{notificacionesActivas}/{dirioActivado}/{progresoActivado}")]
        public Task<ActionResult> ActivarNotificaciones([FromRoute] int id, [FromRoute] bool notificacionesActivas, [FromRoute] bool dirioActivado, [FromRoute] bool progresoActivado)
        {
            if (id < 1) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.MoficarConfiguracionNotificaciones(id, notificacionesActivas, dirioActivado, progresoActivado);
            if (!result) return Task.FromResult<ActionResult>(BadRequest());
            return Task.FromResult<ActionResult>(Ok(result));
        }

        [HttpPut("{id}/registerSet/{state}")]
        public Task<ActionResult> registerSet(int id, string state)
        {
            if (id < 1) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.registerSet(id, state);
            if (result != "success") return Task.FromResult<ActionResult>(BadRequest());
            return Task.FromResult<ActionResult>(Ok(result));
        }

        [HttpPut("{id}/putStickeriInInterface/{idUserSticker}/{position}")]
        public Task<ActionResult> putStickeriInInterface(int id, int idUserSticker, int position)
        {
            if (id < 1) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.putStickeriInInterface(id, idUserSticker, position);

            if (result < 1) return Task.FromResult<ActionResult>(BadRequest());
            return Task.FromResult<ActionResult>(Ok(result));
        }

        //putFlowerInInterface
        [HttpPut("{id}/putFlowerInInterface/{idFlower}/{position}")]
        public Task<ActionResult> putFlowerInInterface(int id, int idFlower, int position)
        {
            if (id < 1) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.putFlowerInInterface(id, idFlower, position);

            if (result < 1) return Task.FromResult<ActionResult>(BadRequest());
            return Task.FromResult<ActionResult>(Ok(result));
        }


        [HttpGet("{id}/canGrowFlower")]
        public async Task<ActionResult> canGrowFlower(int id)
        {
            if (id < 1)
                return BadRequest("Id o token inválido.");
            var result = _service.canGrowFlower(id);

            if (result)
            {
               try {
                 var data = new Dictionary<string, string>
                {
                    { "module", "yard" },
                    { "type", "canGrow" }
                };

                var patient = _service.Get(id);
                var token = patient!.token;
                var title = "¡Tu flor ha crecido!";
                var body = "¡Tu flor ha crecido!";
                await _pushNotificationService.SendPushAsync(title, body, token, data);
               } catch (Exception e) {
                   Console.WriteLine(e);
               }
            }

            return Ok(result);
        }

        [HttpPost("{id}/growStage")]
        public Task<ActionResult> growStage(int id)
        {
            if (id < 1) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.growStage(id);
            if (!result) return Task.FromResult<ActionResult>(BadRequest());
            return Task.FromResult<ActionResult>(Ok(result));
        }



        //bool actualizarThemeId(int idPatient, int themeId)
        [HttpPut("{id}/actualizarThemeId/{themeId}")]
        public Task<ActionResult> actualizarThemeId(int id, int themeId)
        {
            if (id < 1) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.actualizarThemeId(id, themeId);
            if (!result) return Task.FromResult<ActionResult>(BadRequest());
            return Task.FromResult<ActionResult>(Ok(result));
        }

        [HttpPost("{id}/addAchievementToPatient/{idAchievement}")]
        public Task<ActionResult> addAchievementToPatient(int id, int idAchievement)
        {
            Console.WriteLine("id: " + id);
            Console.WriteLine("idAchievement: " + idAchievement);
            if (id < 1 || idAchievement < 1) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.addAchievementToPatient(id, idAchievement);
            if (!result) return Task.FromResult<ActionResult>(BadRequest());
            return Task.FromResult<ActionResult>(Ok(result));
        }
        [HttpGet("{id}/getAllAchievements")]
        public Task<ActionResult> getAllAchievements(int id)
        {
            if (id < 1) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.getAllAchievements(id);
            if (result == null) return Task.FromResult<ActionResult>(NotFound());
            return Task.FromResult<ActionResult>(Ok(result));
        }
        [HttpPost("{id}/createAchievementCollection")]
        public Task<ActionResult> createAchievementCollection(int id)
        {
            if (id < 1) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.createAchievementCollection(id);
            if (!result) return Task.FromResult<ActionResult>(BadRequest());
            return Task.FromResult<ActionResult>(Ok(result));
        }

        [HttpPost("{id}/AddAllAchievementsToPatient")]
        public Task<ActionResult> AddAllAchievementsToPatient(int id)
        {
            if (id < 1) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.AddAllAchievementsToPatient(id);
            if (!result) return Task.FromResult<ActionResult>(BadRequest());
            return Task.FromResult<ActionResult>(Ok(result));
        }
    }       
}



