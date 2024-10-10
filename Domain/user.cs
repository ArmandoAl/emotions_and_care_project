using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class User
    {
        [Key]
        public int userId { get; set; }

        public string name { get; set; } = "";

        public string mail { get; set; } = "";

        public string password { get; set; } = "";

        public string phone { get; set; } = "";

        public DateTime bornDate { get; set; }

        public int age { get; set; }

        public string sex { get; set; } = "";

        public string token { get; set; } = "";

        public string relationalToken { get; set; } = "000000";

        public TermsAndConditions termsAndConditions { get; set; } = new TermsAndConditions();

        public DateTime dateCreated { get; set; } = DateTime.Now;

        public DateTime modifiedDate { get; set; } = DateTime.Now;

    }
}
