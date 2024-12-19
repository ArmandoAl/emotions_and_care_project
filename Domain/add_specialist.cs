using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    /// <summary>
    /// Represents a specialist to be added to the system.
    /// </summary>
    public class AddSpecialist
    {
        /// <summary>
        /// Gets or sets the name of the specialist.
        /// </summary>
        public string name { get; set; } = "";

        /// <summary>
        /// Gets or sets the email of the specialist.
        /// </summary>
        public string mail { get; set; } = "";

        /// <summary>
        /// Gets or sets the password of the specialist.
        /// </summary>
        public string password { get; set; } = "";

        /// <summary>
        /// Gets or sets the phone number of the specialist.
        /// </summary>
        public string phone { get; set; } = "";

        /// <summary>
        /// Gets or sets the age of the specialist.
        /// </summary>
        public int age { get; set; }

        /// <summary>
        /// Gets or sets the sex of the specialist.
        /// </summary>
        public string sex { get; set; } = "";

        /// <summary>
        /// Gets or sets the token of the specialist.
        /// </summary>
        public string token { get; set; } = "";

        /// <summary>
        /// Gets or sets the terms ID associated with the specialist.
        /// </summary>
        public int termsId { get; set; }

        /// <summary>
        /// Gets or sets the license of the specialist.
        /// </summary>
        public string license { get; set; } = "";

        /// <summary>
        /// Gets or sets the focus area of the specialist.
        /// </summary>
        public string focus { get; set; } = "";

        /// <summary>
        /// Gets or sets the institution associated with the specialist.
        /// </summary>
        public string institution { get; set; } = "";

        /// <summary>
        /// Gets or sets the presentation of the specialist.
        /// </summary>
        public string presentation { get; set; } = "";

        /// <summary>
        /// Gets or sets the address of the specialist.
        /// </summary>
        public string adress { get; set; } = "";
    }
}
