using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Date
    {
        [Key]
        public int dateId { get; set; }

        public DateTime date { get; set; } = DateTime.Now;

        public string? hour { get; set; }

        public string? place { get; set; }

        public string? description { get; set; }

        public bool patientConfirm { get; set; } = false;

        public bool specialistConfirm { get; set; } = false;

        public bool done { get; set; } = false;

        public Patient? patient { get; set; }
    }

    public class goalWithDate {
        public Goal? Logro { get; set; } = new Goal();
        public int dateId { get; set; }
    }
}
