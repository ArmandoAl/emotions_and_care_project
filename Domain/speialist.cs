using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Specialist : User
    {
        public string license { get; set; } = "";

        public string focus { get; set; } = "";

        public string? institution { get; set; } = "";

        public string? presentation { get; set; } = "";

        public string? adress { get; set; } = "";

        public List<Cart> communityCarts { get; set; } = new List<Cart>();

        public List<Date> dates { get; set; } = new List<Date>();

        public List<Patient> patients { get; set; } = new List<Patient>();

        public List<DateRequest> dateRequests { get; set; } = new List<DateRequest>();

        public List<PatientRequest> patientsRequests { get; set; } = new List<PatientRequest>();

    }
}
