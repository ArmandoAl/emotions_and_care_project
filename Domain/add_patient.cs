using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    /// <summary>
    /// Represents a class for adding a new patient to the system.
    /// </summary>
    public class AddPatient
    {
        /// <summary>
        /// Gets or sets the name of the patient.
        /// </summary>
        public string name { get; set; } = "";

        /// <summary>
        /// Gets or sets the email address of the patient.
        /// </summary>
        public string mail { get; set; } = "";

        /// <summary>
        /// Gets or sets the password for the patient's account.
        /// </summary>
        public string password { get; set; } = "";

        /// <summary>
        /// Gets or sets the phone number of the patient.
        /// </summary>
        public string phone { get; set; } = "";

        /// <summary>
        /// Gets or sets the birth date of the patient.
        /// Default value is the current date and time.
        /// </summary>
        public DateTime bornDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets or sets the sex of the patient.
        /// </summary>
        public string sex { get; set; } = "";

        /// <summary>
        /// Gets or sets the token associated with the patient's account.
        /// </summary>
        public string token { get; set; } = "";

        /// <summary>
        /// Gets or sets the ID of the terms the patient has agreed to.
        /// </summary>
        public int termsiD { get; set; }
    }
}
