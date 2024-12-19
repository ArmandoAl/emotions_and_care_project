using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    // The 'Date' class represents an appointment or scheduled event
    public class Date
    {
        // The primary key for the 'Date' entity
        [Key]
        public int dateId { get; set; }

        // The date and time of the event. Defaults to the current date and time
        public DateTime date { get; set; } = DateTime.Now;

        // An optional property for the hour of the event (e.g., "10:00 AM")
        public string? hour { get; set; }

        // An optional property for the place where the event will take place
        public string? place { get; set; }

        // An optional property to describe the event (e.g., "Consultation with Dr. Smith")
        public string? description { get; set; }

        // A boolean indicating whether the patient has confirmed the event
        public bool patientConfirm { get; set; } = false;

        // A boolean indicating whether the specialist has confirmed the event
        public bool specialistConfirm { get; set; } = false;

        // A boolean indicating whether the event has been completed or not
        public bool done { get; set; } = false;

        // A reference to the 'Patient' object associated with this event (can be null)
        public Patient? patient { get; set; }
    }

    // The 'goalWithDate' class represents a link between a 'Goal' and a 'Date'
    public class goalWithDate 
    {
        // A reference to a 'Goal' associated with this date (can be null)
        public Goal? goal { get; set; } = new Goal();

        // The unique identifier for the associated date
        public int dateId { get; set; }
    }
}
