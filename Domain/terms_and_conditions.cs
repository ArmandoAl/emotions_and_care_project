using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    // Represents the terms and conditions of the application or service
    public class TermsAndConditions
    {
        // The unique identifier for the terms and conditions (Primary Key)
        [Key]
        public int termsAndConditionsId { get; set; }

        // The actual text content of the terms and conditions
        public string termsAndConditions { get; set; } = "";
    }
}

