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

        public PatientService(IPatientRepository service, IItemsRepository itemsService, INotificationRepository notificationRepository)
        {
            _service = service;
            _itemsService = itemsService;
            _notificationRepository = notificationRepository;
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

               //Mandar notificacion
               _notificationRepository.AddNotification(new NotificationModel
               {
                    Titulo = "¡Tu flor ha crecido!",
                     Descripcion = "Felicidades, te haz esforzado mucho y tu flor esta creciendo. Esperamos que tu salud emocional este mejorando junto con ella.", 
                     notificationType = NotificationType.growNotifications,
                     FechaCreacion = DateTime.Now,
                     emitDate = DateTime.Now,                     
               });
            }

            return can;
        }

        public bool growStage(int idPatient) {
            if (idPatient < 1) { return false; }
            return _service.growStage(idPatient);

        }

        public bool refreshToken(int id, string token)
        {
            if (id < 1 || string.IsNullOrEmpty(token)) { return false; }
            return _service.refreshToken(id, token);
        }

        public bool VincularDirecto(int id, string tokenEspecialista)
        {
            if (id < 1 || string.IsNullOrEmpty(tokenEspecialista)) { return false; }
            return _service.VincularDirecto(id, tokenEspecialista);
        }

        //growFlowerStage

        public bool actualizarThemeId(int idPatient, int themeId)
        {
            if (idPatient < 1 || themeId < 1) { return false; }
            return _service.actualizarThemeId(idPatient, themeId);
        }
    }
}
