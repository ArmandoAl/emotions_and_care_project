using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain
{
    // Represents a sticker with a unique identifier and a URL
    public class Sticker
    {
        // Unique identifier for the sticker (primary key in the database)
        [Key]
        public int stickerId { get; set; }

        // URL of the sticker, typically where the image or graphic is stored
        public string url { get; set; } = "";


    }
}


/*

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
    

*/
