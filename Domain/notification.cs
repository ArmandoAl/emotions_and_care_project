using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public enum NotificationType
    {
        ReminderNotification,
        RecommendationNotification,
        NoteNotification,
    }

    public class NotificationModel
    {
        [Key]
        public int notificationId { get; set; }

        public NotificationType notificationType { get; set; }

        public string Titulo { get; set; } = "";

        public string Descripcion { get; set; } = "";

        public int? recomendationId { get; set; }
        public RecomendationType? recomendationType { get; set; }
        public string? reference { get; set; }
        public string? url { get; set; }

        public DateTime? emitDate { get; set; } = DateTime.Now;

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public DateTime FechaModificacion { get; set; } = DateTime.Now;
    }
}
