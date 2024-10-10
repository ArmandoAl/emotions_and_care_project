using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain
{
    public class Emotion
    {
        [Key]
        public int emotionId { get; set; }
 
        public string name { get; set; } = "";

        public DateTime dateCreated { get; set; } = DateTime.Now;

        public DateTime modifiedDate { get; set; } = DateTime.Now;

    }
}
