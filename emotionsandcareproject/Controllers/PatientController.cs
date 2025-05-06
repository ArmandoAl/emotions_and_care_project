using Business.Contracts;
using Business.Implementations;
using Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Mail;
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

        private readonly IEmailService _emailService;
        public PacienteController(IPatientService service, IEmailService emailService, PushNotificationService pushNotificationService, ISpecialistService specialistService)
        {
            _service = service;
            _emailService = emailService;
            _pushNotificationService = pushNotificationService;
            _specialistService = specialistService;
        }


        [HttpPost]
        public Task<ActionResult> Add(AddPatient paciente)
        {
            if (paciente == null) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.Add(paciente);
            if (result == 0) return Task.FromResult<ActionResult>(BadRequest());

            var patient = _service.Get(result);

            if (patient == null) return Task.FromResult<ActionResult>(BadRequest());


            string confirmationLink = $"https://emotionsandcare-erffhse3f7aecnb0.eastus-01.azurewebsites.net/Api/Paciente/confirmarUsuario/{patient.userId}";
            string subject = "Confirma tu registro";
            string body = $"<p>Hola {patient!.name},</p><p>Por favor <a href='{confirmationLink}'>confirma tu cuenta aquí</a>.</p>";
            
            try {
                var res = _emailService.sendMail(result, new EmailClass
                {
                    To = paciente.mail,
                    Subject = subject,
                    Body = body
                });

                if (!res)
                {
                    Console.WriteLine("Error al enviar el correo electrónico.");
                }
            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }
            

            return Task.FromResult<ActionResult>(Ok(result));
        }

        //confirmar usuario put
        [HttpPut("confirmarUsuario/{id}")]
        public Task<ActionResult> ConfirmarUsuario(int id)
        {
            if (id < 1) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.ConfirmarUsuario(id);
            if (!result) return Task.FromResult<ActionResult>(BadRequest());
            return Task.FromResult<ActionResult>(Ok(result));
        }

        //olvidar contraseña
        [HttpPut("olvidarContraseña/{email}")]
        public Task<ActionResult> OlvidarContraseña(string email)
        {
            if (email == null) return Task.FromResult<ActionResult>(BadRequest());
            var patient = _service.GetByEmail(email);

            if (patient == null) return Task.FromResult<ActionResult>(BadRequest());

            var code = _service.GetForgotPassword(patient.userId);
            Console.WriteLine(code);

            if (code == "") return Task.FromResult<ActionResult>(BadRequest());

            string codeText = code.ToString();

            string subject = "Recupera tu contraseña";
            //muestra el código en el correo, el usuario lo pondra en la misma app
            string body = $"<p>Hola,</p><p>Tu código de recuperación es: {codeText}</p>";
            try {
                var res = _emailService.sendMail(patient.userId, new EmailClass
                {
                    To = email,
                    Subject = subject,
                    Body = body
                });

                if (!res)
                {
                    Console.WriteLine("Error al enviar el correo electrónico.");
                }
            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }

            return Task.FromResult<ActionResult>(Ok());
        }

        //validar codigo
        [HttpPut("validarCodigo/{gmail}/{codigo}")]
        public Task<ActionResult> ValidarCodigo(string gmail, string codigo)
        {
            if (gmail == "" || codigo == null) return Task.FromResult<ActionResult>(BadRequest());

            var patient = _service.GetByEmail(gmail);

            if(patient == null) return Task.FromResult<ActionResult>(BadRequest());

            var result = _service.ValidarCodigo(patient.userId, codigo);
            if (!result) return Task.FromResult<ActionResult>(BadRequest());
            return Task.FromResult<ActionResult>(Ok(result));
        }

        //modificar contraseña
        [HttpPut("modificarContraseña/{gmail}/{nuevaContraseña}")]
        public Task<ActionResult> ModificarContraseña(string gmail, string nuevaContraseña)
        {
            if (gmail == "" || nuevaContraseña == null) return Task.FromResult<ActionResult>(BadRequest());
            var patient = _service.GetByEmail(gmail);
            if (patient == null) return Task.FromResult<ActionResult>(BadRequest());

            
            var result = _service.ModificarContraseña(patient.userId, nuevaContraseña);
            if (!result) return Task.FromResult<ActionResult>(BadRequest());
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

        //removeStickerInInterface
        [HttpPut("{id}/removeStickerInInterface/{position}")]
        public Task<ActionResult> removeStickerInInterface(int id, int position)
        {
            if (id < 1) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.removeStickerInInterface(id, position);

            if (result == false) return Task.FromResult<ActionResult>(BadRequest());
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

                 if(patient!.settings!.notificationsActive) {
                    await _pushNotificationService.SendPushAsync(title, body, token, data);
                 }
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

        [HttpGet("{id}/getStageProgress")]
        public Task<ActionResult> GetStageProgress(int id)
        {
            if (id < 1) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.GetStageProgress(id);
            if (result == null) return Task.FromResult<ActionResult>(NotFound());
            return Task.FromResult<ActionResult>(Ok(result));
        }

        [HttpGet("{id}/getAllStagesProgress")]
        public Task<ActionResult> GetAllStagesProgress(int id)
        {
            if (id < 1) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.GetAllStagesProgress(id);
            if (result == null) return Task.FromResult<ActionResult>(NotFound());
            return Task.FromResult<ActionResult>(Ok(result));
        }



        //bool actualizarThemeId(intactualizarThemeId idPatient, int themeId)
        [HttpPut("{id}/actualizarThemeId/{themeId}")]
        public Task<ActionResult> actualizarThemeId(int id, int themeId)
        {
            if (id < 1) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.actualizarThemeId(id, themeId);
            Console.WriteLine(result);

            if (!result) return Task.FromResult<ActionResult>(BadRequest());
            return Task.FromResult<ActionResult>(Ok(result));
        }

        //actualizar backgroundUrl
        [HttpPut("{id}/actualizarBackgroundUrl/{backgroundId}")]
        public Task<ActionResult> actualizarBackgroundUrl(int id, int backgroundId)
        {
            if (id < 1) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.actualizarBackgroundId(id, backgroundId);
            if (!result) return Task.FromResult<ActionResult>(BadRequest());
            return Task.FromResult<ActionResult>(Ok(result));
        }

        //newDeletePatient
        [HttpDelete("{id}/delete")]
        public Task<ActionResult> DeletePatient(int id)
        {
            if (id < 1) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.SoftDelete(id);
            if (!result) return Task.FromResult<ActionResult>(BadRequest());
            return Task.FromResult<ActionResult>(Ok(result));
        }


        //mandar correo 
        [HttpPost("{id}/sendEmail/{email}/{subject}/{body}")]
        public Task<ActionResult> SendEmail(int id, string email, string subject, string body)
        {
            if (id < 1 || email == null || subject == null || body == null) return Task.FromResult<ActionResult>(BadRequest());
            var result = _emailService.sendMail(id, new EmailClass
            {
                To = email,
                Subject = subject,
                Body = body
            });

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

        [HttpGet("{id}/CheckAchievements")]
        public Task<ActionResult> CheckAchievements(int id)
        {
            if (id < 1) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.CheckAchievements(id);
            if (result == null) return Task.FromResult<ActionResult>(NotFound());
            return Task.FromResult<ActionResult>(Ok(result));
        }

        [HttpPost("{userId}/addStickerToPatient/{idSticker}")]
        public Task<ActionResult> addStickerToPatient(int userId, int idSticker)
        {
            if (userId < 1 || idSticker < 1) return Task.FromResult<ActionResult>(BadRequest());
            var result = _service.AddStickerToPatient(userId, idSticker);
            if (result == null) return Task.FromResult<ActionResult>(NotFound());
            return Task.FromResult<ActionResult>(Ok(result));
        }
    }       
}



