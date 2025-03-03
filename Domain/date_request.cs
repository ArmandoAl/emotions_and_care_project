using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    // Represents a request for a date.
    public class DateRequest
    {
        // The unique identifier for the DateRequest. 
        // This property is marked as the primary key for the database.
        [Key]
        public int dateRequestId { get; set; }

        // The requested date (Cita). 
        // This is an optional field (nullable DateTime).
        // If no date is provided, it will be null.
        public Date? Cita { get; set; }

        public DateTime? dateCreated { get; set; } = DateTime.Now;
    }
}

