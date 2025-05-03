using Business.Contracts;
using Data.contracts;
using Data.Contracts;
using Domain;
using FirebaseAdmin.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Implementations
{
    public class EspecialistaService : ISpecialistService
    {
        private readonly ISpecialistRepository _service;
        private readonly IDateRepository _citaRepository;

        private readonly IGoalRepository _logroRepository;

        private readonly IItemsRepository _itemsRepository;

        private readonly INotificationRepository _notificationRepository;

        public EspecialistaService(ISpecialistRepository service, IDateRepository citaRepository, IGoalRepository logroRepository, IItemsRepository itemsRepository, INotificationRepository notificationRepository)
        {
            _service = service;
            _citaRepository = citaRepository;
            _logroRepository = logroRepository;
            _itemsRepository = itemsRepository;
            _notificationRepository = notificationRepository;
        }

        public int Add(AddSpecialist especialista)
        {
            if (especialista == null) { return 0; }
            return _service.Add(especialista);
        }

        public bool Delete(int id)
        {
            if (id < 1) { return false; }
            return _service.Delete(id);
        }

        public Specialist? Get(int id)
        {
            if (id < 1) return null;
            return _service.Get(id);
        }

        public bool Update(Specialist especialista)
        {
            if (especialista == null) { return false; }
            return _service.Update(especialista);
        }

        public Specialist? GetByToken(string relatedToken)
        {
            if (string.IsNullOrEmpty(relatedToken)) { return null; }
            return _service.GetByToken(relatedToken);
        }

        public int login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password)) { return 0; }
            return _service.login(email, password);
        }

        public bool aceptarSolicitud(int idSpecialist, int pacientId)
        {
            if (idSpecialist < 1 || pacientId < 1) { return false; }

            var result = _service.aceptarSolicitud(idSpecialist, pacientId);

            if (result)
            {
                var idLogro = _logroRepository.AddGoalPatient(pacientId, 6);

                if (idLogro > 0)
                {
                    _itemsRepository.addStickerToPatient(4, pacientId);

                    var notiId = _notificationRepository.AddNotification(new NotificationModel
                    {
                       notificationType = NotificationType.goal,
                        Titulo = "¡Nuevo logro!",
                        Descripcion = "Has logrado vincularte con un especialista, ¡sigue así!, te has ganado un nuevo sticker",
                        url = _itemsRepository.GetSticker(4).url,
                        stickerId = 4,
                        reference = "patientSync"                    
                    });

                    _notificationRepository.vincularNotificationConPaciente(notiId, pacientId);
                }
            }

            return result;
        }


        public bool rechazarSolicitud(int idSpecialist, int pacientId)
        {
            if (idSpecialist < 1 || pacientId < 1) { return false; }

            return _service.rechazarSolicitud(idSpecialist, pacientId);
        }
        
        public bool aceptarCita(int idEspecialista, int idCita)
        {
            if (idCita < 1 || idEspecialista < 1) { return false; }

            DateRequest solicitudCita = _citaRepository.GetSolicitudCita(idCita)!;

            if (solicitudCita == null) { return false; }

            bool vinculacion = _citaRepository.vincularCitaConEspecialista(idEspecialista, idCita);

            if (vinculacion)
            {
                _citaRepository.confirmarCitaPorEspecialista(idCita, idEspecialista);

                bool result = _citaRepository.eliminarSolicitudCita(solicitudCita.dateRequestId);

                return result;
            }
            else
            {
                return false;
            }
        }

        public bool rechazarCita(int id, int idCita)
        {
            if(id < 1 || idCita < 1) { return false; }

             DateRequest? solicitudCita = _citaRepository.GetSolicitudCita(idCita);

            if (solicitudCita == null) { 
                return false; }

            bool res = _citaRepository.eliminarSolicitudCita(solicitudCita.dateRequestId);

            if(res)
            {
                bool result = _citaRepository.DeleteCita(idCita);

                return result;
            }
            else
            {
                return false;
            }

        }

        public List<Patient>? GetPacientes(int id)
        {
            if (id < 1) { return null; }
            return _service.GetPacientes(id);
        }

        public List<DateRequest>? GetSolicitudesCitas(int id)
        {
            if(id < 1) { return null; }
            return _citaRepository.GetSolicitudesCitas(id);
        }

        public List<Specialist> ListarEspecialists(int offset, int limit)
        {
            if (offset < 0 || limit < 1) { return new List<Specialist>(); }
            return _service.ListarEspecialistas(offset, limit);
        }

        public bool refreshToken(int id, string token)
        {
            if (id < 1 || string.IsNullOrEmpty(token)) { return false; }
            return _service.refreshToken(id, token);
        }

        public List<PatientRequest>? GetSolicitudesPacientes(int id)
        {
            if (id < 1) { return null; }
            return _service.GetSolicitudesPacientes(id);
        }
    }
}