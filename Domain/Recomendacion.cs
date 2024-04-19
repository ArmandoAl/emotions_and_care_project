using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public enum TipoRecomendacion
    {
        Sueño,
        Alimentacion,
        TecnicasDeRelajacion,
        ActividadFísica,
        VidaSocial
    }
    public class Recomendacion
    {
        [Key]
        public int IdRecomendacion { get; set; }
        public string Titulo { get; set; } = "";
        public string Contenido { get; set; } = "";
        public TipoRecomendacion Tipo { get; set; }
        public string Referencia { get; set; } = "";
        public string? Url { get; set; } = "";

        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime FechaModificacion { get; set; } = DateTime.Now;

        public List<RecomendacionCompletada> recomendacionCompletadas { get; set; } = new List<RecomendacionCompletada>();
    }

    public class RecomendacionCompletada
    {
        [Key]
        public int IdRecomendacion { get; set; }
        public int IdUsuario { get; set; }
        public DateTime FechaCompletada { get; set; } = DateTime.Now;
    }
}
