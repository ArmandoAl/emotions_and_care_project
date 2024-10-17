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
        int AddNotificacion(NotificationModel notificacion, int idPaciente);

        bool UpdateNotificacion(NotificationModel notificacion);

        bool DeleteNotificacion(int idNotificacion);

        NotificationModel? GetNotificacion(int idNotificacion);

        List<NotificationModel>? GetNotificacionesByPaciente(int idPaciente);

        bool vincularNotificacionConPaciente(int idNotificacion, int idPaciente);

        bool updateDateEmision(int idNotificacion);

        List<NotificationModel> init(int idPaciente); 
    }
}
