using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    // Enum representing the type of recommendation
    public enum RecomendationType
    {
        // Recommendation related to sleep
        Sleep,

        // Recommendation related to food or nutrition
        Food,

        // Recommendation related to relaxation techniques
        RelaxationTechniques,

        // Recommendation related to physical activity
        PhysicalActivity,

        // Recommendation related to social life or social activities
        SocialLife
    }

    // Enum representing the subtitle in Spanish for each recommendation type
    public enum SubTitle
    {
        // Spanish translation for "Sleep"
        Sueño,

        // Spanish translation for "Food"
        Alimentacion,

        // Spanish translation for "Relaxation Techniques"
        TecnicasDeRelajacion,

        // Spanish translation for "Physical Activity"
        ActividadFísica,

        // Spanish translation for "Social Life"
        VidaSocial
    }

    // Represents a recommendation given to the patient
    public class Recomendation
    {
        // The unique identifier for the recommendation (Primary Key)
        [Key]
        public int recomendationId { get; set; }

        // The title or name of the recommendation
        public string title { get; set; } = "";

        // Detailed content or description of the recommendation
        public string content { get; set; } = "";

        // The type of recommendation, specified by the RecomendationType enum
        public RecomendationType type { get; set; }

        // The subtitle of the recommendation in Spanish, specified by the SubTitle enum
        public SubTitle subTitle { get; set; }

        // A reference for the recommendation, e.g., a source or guideline
        public string reference { get; set; } = "";

        // An optional URL providing more information about the recommendation
        public string? url { get; set; } = "";

        // The date when the recommendation was created
        public DateTime createdDate { get; set; } = DateTime.Now;

        // The date when the recommendation was last modified
        public DateTime modifiedDate { get; set; } = DateTime.Now;
    }
}