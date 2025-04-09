using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    // Represents a request made by a patient in the system
    public class PatientRequest
    {
        // Unique identifier for the patient request (primary key in the database)
        [Key]
        public int patientRequestId { get; set; }

        // The patient associated with this request (an object of type Patient)
        public Patient patient { get; set; } = new Patient();


        public DateTime? date { get; set; } = DateTime.Now;
    }
}
