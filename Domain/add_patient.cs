using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Domain
{
    public class AddPatient
    {
//Puto
        public string name { get; set; } = "";

        public string mail { get; set; } = "";

        public string password { get; set; } = "";

        public string phone { get; set; } = "";

        public DateTime bornDate { get; set; } = DateTime.Now;

        public string sex { get; set; } = "";

        public string token { get; set; } = "";

        public int termsiD { get; set; }
    }
}
