using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class AddSpecialist
    { 
      
        public string name { get; set; } = "";
        public string mail { get; set; } = "";
        public string password { get; set; } = "";
        public string phone { get; set; } = "";
        public int age { get; set; }
        public string sex { get; set; } = "";
        public string token { get; set; } = "";
        public int termsId { get; set; }
        public string license { get; set; } = "";
        public string focus { get; set; } = "";
        public string institution { get; set; } = "";
        public string presentation { get; set; } = "";
        public string adress { get; set; } = "";
    }
}
