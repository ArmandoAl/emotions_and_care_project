using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class PatientRequest
    {
        [Key]
        public int patientRequestId { get; set; }

        public Patient patient { get; set; } = new Patient();
    }
}
