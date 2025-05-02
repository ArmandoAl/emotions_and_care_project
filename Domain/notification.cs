using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    // Enum representing different types of notifications
    public enum NotificationType
    {
        // A reminder notification
        ReminderNotification,

        // A recommendation notification
        RecommendationNotification,

        // A note notification
        NoteNotification,

        goal,

        growNotifications
    }

    // Represents a notification model with various properties related to the notification details
    public class NotificationModel
    {
        // Unique identifier for each notification (Primary Key)
        [Key]
        public int notificationId { get; set; }

        // Type of the notification (uses the NotificationType enum to define it)
        public NotificationType notificationType { get; set; }

        // Title of the notification (default is an empty string if not provided)
        public string Titulo { get; set; } = "";

        // Description of the notification (default is an empty string if not provided)
        public string Descripcion { get; set; } = "";

        // Optional recommendation ID associated with the notification (nullable)
        public int? recomendationId { get; set; }

        // Optional recommendation type associated with the notification (nullable)
        public RecomendationType? recomendationType { get; set; }

        // Optional reference string, which could provide additional details about the notification (nullable)
        public string? reference { get; set; }

        // Optional URL associated with the notification (nullable)
        public string? url { get; set; }

        public int? stickerId { get; set; } = 0;

        // Date and time when the notification was emitted (nullable, default is current date and time)
        public DateTime? emitDate { get; set; } = DateTime.Now;

        // Date and time when the notification was created (default is current date and time)
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        // Date and time when the notification was last modified (default is current date and time)
        public DateTime FechaModificacion { get; set; } = DateTime.Now;
    }
}
