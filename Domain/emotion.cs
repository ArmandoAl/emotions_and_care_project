using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain
{
    // Represents an emotion entity with properties for its name and creation/modification dates
    public class Emotion
    {
        // Unique identifier for each emotion (Primary Key)
        [Key]
        public int emotionId { get; set; }

        // Name of the emotion (default is an empty string if not provided)
        public string name { get; set; } = "";

        // Date and time when the emotion entity was created (default is current date and time)
        public DateTime dateCreated { get; set; } = DateTime.Now;

        // Date and time when the emotion entity was last modified (default is current date and time)
        public DateTime modifiedDate { get; set; } = DateTime.Now;
    }
}
