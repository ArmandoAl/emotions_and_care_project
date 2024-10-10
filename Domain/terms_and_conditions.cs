using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class TermsAndConditions
    {
        [Key]
        public int termsAndConditionsId { get; set; }

        public string termsAndConditions { get; set; } = "";
    }
}
