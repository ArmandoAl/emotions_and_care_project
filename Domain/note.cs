using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    // The 'Note' class represents a note that can be created, edited, and stored.
    public class Note
    {
        // The primary key for the 'Note' entity
        [Key]
        public int noteId { get; set; }

        // The title of the note. Defaults to an empty string.
        public string title { get; set; } = "";

        // The content of the note. Defaults to an empty string.
        public string content { get; set; } = "";

        // A reference to an 'Emotion' object representing the emotion associated with the note.
        // Defaults to a new 'Emotion' instance.
        public Emotion emotion { get; set; } = new Emotion();

        // The date and time when the note was created. Defaults to the current date and time.
        public DateTime dateCreated { get; set; } = DateTime.Now;

        // The date and time when the note was last modified.
        // Defaults to the current date and time.
        public DateTime modifiedDate { get; set; } = DateTime.Now;

        // A boolean indicating whether the note is visible to the user. Defaults to true.
        public bool visible { get; set; } = true;
    }

    // The 'GoalWithNote' class represents a link between a 'Goal' and a 'Note'.
    public class GoalWithNote 
    {
        // A reference to a 'Goal' associated with this note. Defaults to a new 'Goal' instance.
        public Goal? goal { get; set; } = new Goal();

        // The unique identifier for the associated note.
        public int noteId { get; set; }
    }
}

