using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Contracts
{
    public interface INotificationRepository
    {
        int AddNotification(NotificationModel notification);

        bool UpdateNotification(NotificationModel notification);

        bool DeleteNotification(int idNotification);

        NotificationModel? GetNotification(int idNotification);

        List<NotificationModel>? GetNotificationesByPaciente(int idPaciente);

        bool postponeNotification(int notificationId);

        bool vincularNotificationConPaciente(int idNotification, int idPaciente);

        bool updateDateEmision(int idNotification);

        bool checkExistGrowNotification(int idPaciente);
    }
}
