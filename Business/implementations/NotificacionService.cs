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
    public class NotificacionService : INotificacionService
    {
        private readonly INotificacionRepository _notificacionRepository;
        private readonly INotaRepository _notaRepository;
        private readonly ICitaRepository _citaRepository;
        private readonly IRecomendacionRepository _recomendacionRepository;
        private readonly ICartaRepository _cartaRepository;

        public NotificacionService(INotificacionRepository notificacionRepository, 
               INotaRepository notaRepository, ICitaRepository citaRepository, IRecomendacionRepository recomendacionRepository,
               ICartaRepository cartaRepository)
        {
            _notificacionRepository = notificacionRepository;
            _notaRepository = notaRepository;
            _citaRepository = citaRepository;
            _recomendacionRepository = recomendacionRepository;
            _cartaRepository = cartaRepository;
        }

        public List<Notificacion> init(int idPaciente)
        {

            var notiList = _notificacionRepository.GetNotificacionesByPaciente(idPaciente);
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



            List<Recomendacion>? recomandaciones = _recomendacionRepository.GetRecomentaciones();

            if (recomandaciones != null)
            {
               //agrega una recomendacion ramdom como notificacion
               Random rnd = new Random();
                int index = rnd.Next(recomandaciones.Count);
                var notificacion = new Notificacion
                {
                    TipoNotificacion = TipoNotificacion.NotificacionRecomendacion,
                    Titulo = "Recomendación",
                    Descripcion = recomandaciones[index].Contenido,
                    idRecomandacion = recomandaciones[index].IdRecomendacion,
                    TipoRecomendacion = recomandaciones[index].Tipo,
                    Referencia = recomandaciones[index].Referencia,
                   // Url = recomandaciones[index].Url
                };

                var idNotificacion = _notificacionRepository.AddNotificacion(notificacion);

                if (idNotificacion > 0)
                {
                    var vinculacion = _notificacionRepository.vincularNotificacionConPaciente(idNotificacion, idPaciente);
                    if (!vinculacion)
                    {
                        _notificacionRepository.DeleteNotificacion(idNotificacion);
                    }
                }   
            }


            bool thereCartsForOpen = _cartaRepository.thereCartsForOpen(idPaciente);
            if (thereCartsForOpen)
            {

                var notificacion = new Notificacion
                {
                    TipoNotificacion = TipoNotificacion.NotificacionRecordatorio,
                    Titulo = "Buzon",
                    Descripcion = "Parece que tienes cartas sin abrir, revisalas en tu buzon de el apartado de comunidad"
                };

                var idNotificacion = _notificacionRepository.AddNotificacion(notificacion);

                if (idNotificacion > 0)
                {
                    var vinculacion = _notificacionRepository.vincularNotificacionConPaciente(idNotificacion, idPaciente);
                    if (!vinculacion)
                    {
                        _notificacionRepository.DeleteNotificacion(idNotificacion);
                    }
                }

            }
           
            
            var notaDays = _notaRepository.getTimeWithoutNotes(idPaciente);
            if (notaDays > 2)
            {
                var notificacion = new Notificacion
                {
                    TipoNotificacion = TipoNotificacion.NotificacionRecordatorio,
                    Titulo = "Diario",
                    Descripcion = "No has escrito en tu diario, por que no nos cuentas como te sientes hoy :)"
                };

                var idNotificacion = _notificacionRepository.AddNotificacion(notificacion);

                if (idNotificacion > 0)
                {
                    var vinculacion = _notificacionRepository.vincularNotificacionConPaciente(idNotificacion, idPaciente);
                    if (!vinculacion)
                    {
                        _notificacionRepository.DeleteNotificacion(idNotificacion);
                    }
                }
            }

            var citas = _citaRepository.GetCitasPorPaciente(idPaciente);
            if (citas != null)
            {
                foreach (var cita in citas)
                {
                    var days = getDaysDiference(cita.Fecha, DateTime.Now);
                    if (days < 3)
                    {
                        var notificacion = new Notificacion
                        {
                            TipoNotificacion = TipoNotificacion.NotificacionRecordatorio,
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

                        var idNotificacion = _notificacionRepository.AddNotificacion(notificacion);

                        if (idNotificacion > 0)
                        {
                            var vinculacion = _notificacionRepository.vincularNotificacionConPaciente(idNotificacion, idPaciente);
                            if (!vinculacion)
                            {
                                _notificacionRepository.DeleteNotificacion(idNotificacion);
                            }
                        }
                    }
                }
            }

            //TODO: Agregar notificaciones de buzon de notas
            return _notificacionRepository.GetNotificacionesByPaciente(idPaciente)!;

        }


        private int getDaysDiference(DateTime date1, DateTime date2)
        {
            TimeSpan ts = date1 - date2;
            //the value most to be positive
            return ts.Days;
        }

        public int AddNotificacion(Notificacion notificacion, int idPaciente)
        {
            if (notificacion == null) return 0;
            var id = _notificacionRepository.AddNotificacion(notificacion);

            if (id > 0)
            {
                var vinculacion = _notificacionRepository.vincularNotificacionConPaciente(id, idPaciente);
                if (!vinculacion)
                {
                    _notificacionRepository.DeleteNotificacion(id);
                    return 0;
                }
                return id;
            }

            return 0;
        }

        public bool DeleteNotificacion(int idNotificacion)
        {
            if (idNotificacion <= 0) return false;
            return _notificacionRepository.DeleteNotificacion(idNotificacion);
        }

        public Notificacion? GetNotificacion(int idNotificacion)
        {
            if (idNotificacion <= 0) return null;
            return _notificacionRepository.GetNotificacion(idNotificacion);

        }

        public List<Notificacion>? GetNotificacionesByPaciente(int idPaciente)
        {
            if (idPaciente <= 0) return null;
            return _notificacionRepository.GetNotificacionesByPaciente(idPaciente);
        }

        public bool updateDateEmision(int idNotificacion)
        {
            return _notificacionRepository.updateDateEmision(idNotificacion);
        }

        public bool UpdateNotificacion(Notificacion notificacion)
        {
            if (notificacion == null) return false;
            return _notificacionRepository.UpdateNotificacion(notificacion);
        }

        public bool vincularNotificacionConPaciente(int idNotificacion, int idPaciente)
        {
            return _notificacionRepository.vincularNotificacionConPaciente(idNotificacion, idPaciente);
        }
    }
}