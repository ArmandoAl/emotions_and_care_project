using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public enum RecomendationType
    {
        Sleep,
        Food,
        RelaxationTechniques,
        PhysicalActivity,
        SocialLife
    }

    public enum SubTitle
    {
        Sueño,
        Alimentacion,
        TecnicasDeRelajacion,
        ActividadFísica,
        VidaSocial
    }
    public class Recomendation
    {
        [Key]
        public int recomendationId { get; set; }
        
        public string title { get; set; } = "";
        public string content { get; set; } = "";
        public RecomendationType type { get; set; }

        public SubTitle subTitle { get; set; }
        public string reference { get; set; } = "";
        public string? url { get; set; } = "";

        public DateTime createdDate { get; set; } = DateTime.Now;
        public DateTime modifiedDate { get; set; } = DateTime.Now;
    }
}
