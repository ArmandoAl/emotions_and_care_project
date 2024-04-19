using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public enum TipoNotificacion
    {
        NotificacionRecordatorio,
        NotificacionRecomendacion,
        NotificacionNota,
    }

    public class Notificacion
    {
        [Key]
        public int IdNotificacion { get; set; }

        public TipoNotificacion TipoNotificacion { get; set; }

        public string Titulo { get; set; } = "";

        public string Descripcion { get; set; } = "";

        public int? idRecomandacion { get; set; }
        public TipoRecomendacion? TipoRecomendacion { get; set; }
        public string? Referencia { get; set; }
        public string? Url { get; set; }

        public DateTime? FechaEmision { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaModificacion { get; set; }
    }
}
