using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Note
    {
        [Key]
        public int noteId { get; set; }

        public string title { get; set; } = "";
 
        public string content { get; set; } = "";

        public Emotion emotion { get; set; } = new Emotion();

        public DateTime dateCreated { get; set; } = DateTime.Now;

        public DateTime modifiedDate { get; set;} = DateTime.Now;

        public bool visible { get; set; } = true;
    }

    public class GoalWithNote {
        public Goal? Logro { get; set; } = new Goal();
        public int noteId { get; set; }
    }
}
