using Business.Contracts;
using Data.contracts;
using Data.Contracts;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Implementations
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _service;

        private readonly IItemsRepository _itemsService;

        private readonly INotificationRepository _notificationRepository;

        private readonly IGoalRepository _logroRepository;

        private readonly IItemsRepository _itemsRepository;

        public PatientService(IPatientRepository service, IItemsRepository itemsService, INotificationRepository notificationRepository, IGoalRepository logroRepository, IItemsRepository itemsRepository)
        {
            _service = service;
            _itemsService = itemsService;
            _notificationRepository = notificationRepository;
            _logroRepository = logroRepository;
            _itemsRepository = itemsRepository;
        }

        public int Add(AddPatient paciente)
        {
            if(paciente == null) { return 0; }
            int id = _service.Add(paciente);

             if(id == 0)
            {
            return 0;        
            }


           for (int i = 0; i < 4; i++)
            {
                bool result = _itemsService.putFlowerInInterface(id, i + 1);

                if (!result)
                {
                    return 0;
                }
            }

            var success = _service.AddAllAchievementsToPatient(id);
            if (!success)
            {
                return 0;
            }

            for (int i = 0; i < 2; i++)
            {
                bool result = _itemsService.addStickerToPatient(i + 1, id);

                if (!result)
                {
                    return 0;
                }
            }

            

            return id;
        }

        public bool Delete(int id)
        {
           if(id < 1) { return false; }
           return _service.Delete(id);
        }

        public Patient? Get(int id)
        {
            if (id < 1) return null;
            
            return _service.Get(id);
        }

        public bool Update(Patient paciente)
        {
            if(paciente == null) { return false; };
            return _service.Update(paciente);
        }

        public bool VincularEspecialista(int id, string tokenEspecialista)
        {
            if (id < 1 || string.IsNullOrEmpty(tokenEspecialista)) { return false; }
            return _service.VincularEspecialista(id, tokenEspecialista);
        }

        public string? GetByToken(int idPaciente)
        {
            if (idPaciente < 1) { return null; }
            return _service.GetByToken(idPaciente);
        }

        public int login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password)) { return 0; }
            return _service.login(email, password);
        }

        public bool MoficarConfiguracionNotificaciones(int id, bool notificacionesActivas, bool dirioActivado, bool progresoActivado)
        {
            if (id < 1) { return false; }
            return _service.MoficarConfiguracionNotificaciones(id, notificacionesActivas, dirioActivado, progresoActivado);
        }

        public string? registerSet(
            int id,
            string state)
        {
            if (id < 1) { return  null; }
            if (string.IsNullOrEmpty(state)) { return null; }
            return _service.registerSet(id, state);

        }

        public int putStickeriInInterface(int idPatient, int idUserSticker, int position) {
            
            if (idPatient < 1 || idUserSticker < 1 || position < 1) { return 0; }
            return _service.putStickeriInInterface(idPatient, idUserSticker, position);
            
         }

        public int putFlowerInInterface(int idPatient, int idFlower, int position) {
            if (idPatient < 1 || idFlower < 1 || position < 1) { return 0; }
            return _service.putFlowerInInterface(idPatient, idFlower, position);
        }
        
        public bool canGrowFlower(int idPatient) {
            if (idPatient < 1) { return false; }


            bool canReview = _service.reviewCanCheck(idPatient);

            if (!canReview) { return false; }

            
            bool can = _service.canGrowFlower(idPatient);

            if (can)
            {  
               _service.updateLastProgressDate(idPatient);

               var notiExist = _notificationRepository.checkExistGrowNotification(idPatient);

               if (notiExist)
               {
                     return can;    
                   
               }

               //Mandar notificacion
               var notiId = _notificationRepository.AddNotification(new NotificationModel
               {
                    Titulo = "¡Tu flor ha crecido!",
                     Descripcion = "Felicidades, te haz esforzado mucho y tu flor esta creciendo. Esperamos que tu salud emocional este mejorando junto con ella.", 
                     notificationType = NotificationType.growNotifications,
                     FechaCreacion = DateTime.Now,
                     emitDate = DateTime.Now,                     
               });

               _notificationRepository.vincularNotificationConPaciente(notiId, idPatient);
            }

            return can;
        }

        public bool growStage(int idPatient) {
            if (idPatient < 1) { return false; }
            return _service.growStage(idPatient);

        }

        public List<StageProgressInfo>? GetStageProgress(int idPatient)
        {
            if (idPatient < 1) { return null; }
            return _service.GetStageProgress(idPatient);
        }

        public bool refreshToken(int id, string token)
        {
            if (id < 1 || string.IsNullOrEmpty(token)) { return false; }
            return _service.refreshToken(id, token);
        }

        public bool VincularDirecto(int id, string tokenEspecialista)
        {
            if (id < 1 || string.IsNullOrEmpty(tokenEspecialista)) { return false; }
            var result = _service.VincularDirecto(id, tokenEspecialista);

            if (result)
            {
               var idLogro = _logroRepository.AddGoalPatient(id, 6);

                if (idLogro > 0)
                {
                    _itemsRepository.addStickerToPatient(4, id);

                    var notiId = _notificationRepository.AddNotification(new NotificationModel
                    {
                       notificationType = NotificationType.goal,
                        Titulo = "¡Nuevo sticker!",
                        Descripcion = "Has logrado vincularte con un especialista, ¡sigue así!, te has ganado un nuevo sticker",
                        url = _itemsRepository.GetSticker(4).url,
                        stickerId = 4,
                        reference = "patientSync"                    
                    });

                    _notificationRepository.vincularNotificationConPaciente(notiId, id);
                }

            }

            return result;

        }

        //growFlowerStage

        public bool actualizarThemeId(int idPatient, int themeId)
        {
            if (idPatient < 1 || themeId < 1) { return false; }
            return _service.actualizarThemeId(idPatient, themeId);
        }

        public bool addAchievementToPatient(int idPatient, int idAchievement)
        {
            if (idPatient < 1 || idAchievement < 1) { return false; }
            return _service.addAchievementToPatient(idPatient, idAchievement);
        }
        public AchievementCollection? getAllAchievements(int idPatient)
        {
            if (idPatient < 1) { return null; }
            return _service.getAllAchievements(idPatient);
        }

        public bool createAchievementCollection(int idPatient)
        {
            if (idPatient < 1) { return false; }
            return _service.createAchievementCollection(idPatient);
        }

        public bool AddAllAchievementsToPatient(int idPatient)
        {
            if (idPatient < 1) { return false; }
            return _service.AddAllAchievementsToPatient(idPatient);

        }

        public List<progressBool>? CheckAchievements(int idPatient)
        {
            if (idPatient < 1) { return null; }
            return _service.CheckAchievements(idPatient);
        }


         public bool removeStickerInInterface(int idPatient, int position) {
            if (idPatient < 1 || position < 1) { return false; }
            return _service.removeStickerInInterface(idPatient, position);
        }
      

        public bool actualizarBackgroundId(int idPatient, int backgroundId)
        {
            if (idPatient < 1 || backgroundId < 0) { return false; }
            return _service.actualizarBackgroundId(idPatient, backgroundId);
        }

        public bool SoftDelete(int id)
        {
            if (id < 1) { return false; }
            return _service.SoftDelete(id);
        }

        public bool ConfirmarUsuario(int id)
        {
            if (id < 1) { return false; }
            return _service.ConfirmarUsuario(id);
        }

        public Patient? GetByEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) { return null; }
            return _service.GetByEmail(email);
        }

        public string GetForgotPassword(int idPatient)
        {
            if (idPatient < 1) { return ""; }
            return _service.GetForgotPassword(idPatient);
        }

        public bool ValidarCodigo(int idPatient, string code)
        {
            if (idPatient < 1 || string.IsNullOrEmpty(code)) { return false; }
            return _service.ValidarCodigo(idPatient, code);
        }   

        public bool ModificarContraseña(int idPatient, string password)
        {
            if (idPatient < 1 || string.IsNullOrEmpty(password)) { return false; }
            return _service.ModificarContraseña(idPatient, password);
        }

        public Sticker? AddStickerToPatient(int idPatient, int stickerId)
        {
            if (idPatient < 1 || stickerId < 1) { return null; }
            var sticker = _itemsRepository.GetSticker(stickerId);
            if (sticker == null) { return null; }
            var result = _itemsRepository.addStickerToPatient(stickerId, idPatient);
            if (!result) { return null; }
            return sticker;
        }
    }
}
