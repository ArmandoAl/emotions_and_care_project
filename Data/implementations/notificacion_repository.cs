using Data.Contracts;
using Domain;
using FirebaseAdmin.Messaging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Implementations
{
    public class NotificationRepository : INotificationRepository
    {   
        public int AddNotification(NotificationModel notification)
        {
            if(notification == null) return 0;

            if(notification.notificationType == NotificationType.RecommendationNotification) {
               
                notification.emitDate = DateTime.Now.AddDays(7);
            }


            notification.FechaCreacion = DateTime.Now;
            notification.FechaModificacion = DateTime.Now;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
            .UseSqlServer(Data.Helpers.Constants.ConnectionString)
            .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                db.notifications.Add(notification);
                db.SaveChanges();
                return notification.notificationId;
            }
        }

        public bool DeleteNotification(int idNotification)
        {
            if(idNotification <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
            .UseSqlServer(Data.Helpers.Constants.ConnectionString)
            .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var notification = db.notifications.FirstOrDefault(x => x.notificationId == idNotification);
                if(notification == null) return false;

                db.notifications.Remove(notification);
                db.SaveChanges();
                return true;
            }
        }

        public NotificationModel? GetNotification(int idNotification)
        {
            if(idNotification <= 0) return null;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
            .UseSqlServer(Data.Helpers.Constants.ConnectionString)
            .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                return db.notifications.FirstOrDefault(x => x.notificationId == idNotification);
            }
        }

        public List<NotificationModel>? GetNotificationesByPaciente(int idPaciente)
        {
            if(idPaciente <= 0) return null;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
            .UseSqlServer(Data.Helpers.Constants.ConnectionString)
            .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                return db.patients.Where(x => x.userId == idPaciente).SelectMany(x => x.notifications).ToList();
            }
        }

        public bool updateDateEmision(int idNotification)
        {
            if(idNotification <= 0) return false;
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
            .UseSqlServer(Data.Helpers.Constants.ConnectionString)
            .Options;
            using (var db = new DBContext(options: connectionOptions))
            {

                var notification = db.notifications.FirstOrDefault(x => x.notificationId == idNotification);
                if(notification == null) return false;

                notification.emitDate = DateTime.Now.AddDays(10);

                db.notifications.Update(notification);
                db.SaveChanges();
                return true;
            }

        }

        public bool UpdateNotification(NotificationModel notification)
        {   
            if(notification == null) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
            .UseSqlServer(Data.Helpers.Constants.ConnectionString)
            .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                db.notifications.Update(notification);
                db.SaveChanges();
                return true;
            }
        }

        public bool vincularNotificationConPaciente(int idNotification, int idPaciente)
        {
            if(idNotification <= 0 || idPaciente <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
            .UseSqlServer(Data.Helpers.Constants.ConnectionString)
            .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var notification = db.notifications.FirstOrDefault(x => x.notificationId == idNotification);
                var paciente = db.patients.FirstOrDefault(x => x.userId == idPaciente);

                if(notification == null || paciente == null) return false;
                
                paciente.notifications.Add(notification);
                db.SaveChanges();
                return true;
            }
        }

        public bool checkExistGrowNotification(int idPaciente)
        {
            if(idPaciente <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
            .UseSqlServer(Data.Helpers.Constants.ConnectionString)
            .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var paciente = db.patients.Where(x => x.userId == idPaciente).Include(x => x.notifications).FirstOrDefault();
                if(paciente == null) return false;

                return paciente.notifications.Any(x => x.notificationType == NotificationType.growNotifications);
            }

        }
    }
}
