using Business.Contracts;
using Data.Contracts;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Implementations
{
    public class NotificacionService : INotificationService
    {
        private readonly INotificationRepository _notificacionRepository;
        private readonly INoteRepository _notaRepository;
        private readonly IDateRepository _citaRepository;
        private readonly IRecomendationRepository _recomendacionRepository;
        private readonly ICartRepository _cartaRepository;

        public NotificacionService(INotificationRepository notificacionRepository, 
               INoteRepository notaRepository, IDateRepository citaRepository, IRecomendationRepository recomendacionRepository,
               ICartRepository cartaRepository)
        {
            _notificacionRepository = notificacionRepository;
            _notaRepository = notaRepository;
            _citaRepository = citaRepository;
            _recomendacionRepository = recomendacionRepository;
            _cartaRepository = cartaRepository;
        }

        public List<NotificationModel> init(int idPaciente)
        {

            var notiList = _notificacionRepository.GetNotificationesByPaciente(idPaciente);
            if (notiList != null)
            {
                foreach (var noti in notiList)
                {
                    if (noti.Titulo == "Agenda" && noti.Descripcion.StartsWith("Tu cita es"))
                    {
                        return notiList;
                    } else
                    {
                        if(noti.Titulo == "Diario" && noti.Descripcion.StartsWith("No has escrito en tu diario"))
                        {
                            return notiList;
                        } else {
                            if(noti.Titulo == "Buzon" && noti.Descripcion.StartsWith("Parece que tienes cartas sin abrir"))
                            {
                                return notiList;
                            } else {
                                if(noti.Titulo == "Recomendación")
                                {
                                    return notiList;
                                }
                            }
                        }
                    }
                }
            }



            List<Recomendation>? recomandaciones = _recomendacionRepository.GetRecomentaciones();

            if (recomandaciones != null)
            {
               //agrega una recomendacion ramdom como notificacion
               Random rnd = new Random();
                int index = rnd.Next(recomandaciones.Count);
                var notificacion = new NotificationModel
                {
                    notificationType = NotificationType.RecommendationNotification,
                    Titulo = "Recomendación",
                    Descripcion = recomandaciones[index].content,
                    recomendationId = recomandaciones[index].recomendationId,
                    recomendationType = recomandaciones[index].type,
                    reference = recomandaciones[index].reference,
                   // Url = recomandaciones[index].Url
                };

                var idNotificacion = _notificacionRepository.AddNotification(notificacion);

                if (idNotificacion > 0)
                {
                    var vinculacion = _notificacionRepository.vincularNotificationConPaciente(idNotificacion, idPaciente);
                    if (!vinculacion)
                    {
                        _notificacionRepository.DeleteNotification(idNotificacion);
                    }
                }   
            }


            bool thereCartsForOpen = _cartaRepository.thereCartsForOpen(idPaciente);
            if (thereCartsForOpen)
            {

                var notificacion = new NotificationModel
                {
                    notificationType = NotificationType.ReminderNotification,
                    Titulo = "Buzon",
                    Descripcion = "Parece que tienes cartas sin abrir, revisalas en tu buzon de el apartado de comunidad"
                };

                var idNotificacion = _notificacionRepository.AddNotification(notificacion);

                if (idNotificacion > 0)
                {
                    var vinculacion = _notificacionRepository.vincularNotificationConPaciente(idNotificacion, idPaciente);
                    if (!vinculacion)
                    {
                        _notificacionRepository.DeleteNotification(idNotificacion);
                    }
                }

            }
           
            
            var notaDays = _notaRepository.getTimeWithoutNotes(idPaciente);
            if (notaDays > 2)
            {
                var notificacion = new NotificationModel
                {
                    notificationType = NotificationType.ReminderNotification,
                    Titulo = "Diario",
                    Descripcion = "No has escrito en tu diario, por que no nos cuentas como te sientes hoy :)"
                };

                var idNotificacion = _notificacionRepository.AddNotification(notificacion);

                if (idNotificacion > 0)
                {
                    var vinculacion = _notificacionRepository.vincularNotificationConPaciente(idNotificacion, idPaciente);
                    if (!vinculacion)
                    {
                        _notificacionRepository.DeleteNotification(idNotificacion);
                    }
                }
            }

            var citas = _citaRepository.GetCitasPorPaciente(idPaciente);
            if (citas != null)
            {
                foreach (var cita in citas)
                {
                    var days = getDaysDiference(cita.date, DateTime.Now);
                    if (days < 3)
                    {
                        var notificacion = new NotificationModel
                        {
                            notificationType = NotificationType.ReminderNotification,
                            Titulo = "Agenda",
                        };

                        if (days == 0)
                        {
                            notificacion.Descripcion = "Tu cita es hoy, no olvides asistir";

                        }
                        else
                        {
                            notificacion.Descripcion = "Tu cita es en " + Math.Abs(days) + " dias, no olvides asistir";
                        }

                        var idNotificacion = _notificacionRepository.AddNotification(notificacion);

                        if (idNotificacion > 0)
                        {
                            var vinculacion = _notificacionRepository.vincularNotificationConPaciente(idNotificacion, idPaciente);
                            if (!vinculacion)
                            {
                                _notificacionRepository.DeleteNotification(idNotificacion);
                            }
                        }
                    }
                }
            }

            //TODO: Agregar notificaciones de buzon de notas
            return _notificacionRepository.GetNotificationesByPaciente(idPaciente)!;

        }


        private int getDaysDiference(DateTime date1, DateTime date2)
        {
            TimeSpan ts = date1 - date2;
            //the value most to be positive
            return ts.Days;
        }

        public int AddNotificacion(NotificationModel notificacion, int idPaciente)
        {
            if (notificacion == null) return 0;
            var id = _notificacionRepository.AddNotification(notificacion);

            if (id > 0)
            {
                var vinculacion = _notificacionRepository.vincularNotificationConPaciente(id, idPaciente);
                if (!vinculacion)
                {
                    _notificacionRepository.DeleteNotification(id);
                    return 0;
                }
                return id;
            }

            return 0;
        }

        public bool DeleteNotificacion(int idNotificacion)
        {
            if (idNotificacion <= 0) return false;
            return _notificacionRepository.DeleteNotification(idNotificacion);
        }

        public NotificationModel? GetNotificacion(int idNotificacion)
        {
            if (idNotificacion <= 0) return null;
            return _notificacionRepository.GetNotification(idNotificacion);

        }

        public List<NotificationModel>? GetNotificacionesByPaciente(int idPaciente)
        {
            if (idPaciente <= 0) return null;
            return _notificacionRepository.GetNotificationesByPaciente(idPaciente);
        }

        public bool updateDateEmision(int idNotificacion)
        {
            return _notificacionRepository.updateDateEmision(idNotificacion);
        }

        public bool UpdateNotificacion(NotificationModel notificacion)
        {
            if (notificacion == null) return false;
            return _notificacionRepository.UpdateNotification(notificacion);
        }

        public bool vincularNotificacionConPaciente(int idNotificacion, int idPaciente)
        {
            return _notificacionRepository.vincularNotificationConPaciente(idNotificacion, idPaciente);
        }
    }
}