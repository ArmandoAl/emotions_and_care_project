using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Contracts
{
    public interface INotificationService
    {
        int AddNotificacion(Notification notificacion, int idPaciente);

        bool UpdateNotificacion(Notification notificacion);

        bool DeleteNotificacion(int idNotificacion);

        Notification? GetNotificacion(int idNotificacion);

        List<Notification>? GetNotificacionesByPaciente(int idPaciente);

        bool vincularNotificacionConPaciente(int idNotificacion, int idPaciente);

        bool updateDateEmision(int idNotificacion);

        List<Notification> init(int idPaciente); 
    }
}
