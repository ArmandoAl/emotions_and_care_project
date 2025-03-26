using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Opps : BUser
    {
        public Bukayo bukayoSakaDiary { get; set; } = new Bukayo();
    }

    public class OppsAdd
    {
        public string Name { get; set; } = "";
        public string Email { get; set; }= "";
        public string Password { get; set; }= "";
    }

    public class Bukayo
    {
        [Key]
        public int Id { get; set; }

        public List<SakaNotes> SakaNotes { get; set; } = new List<SakaNotes>();

    }

    public class SakaNotes
    {
        [Key]
        public int Id { get; set; }
        public string Note { get; set; } = "";
        public DateTime Date { get; set; } = DateTime.Now;
    }

    public class RedCard
    {
        [Key]
        public int Id { get; set; }
        public string Reason { get; set; } = "";
        public DateTime Date { get; set; } = DateTime.Now;
    }


}

namespace Domain
{
    // Represents a user in the system, including personal details and authentication information.
    public class BUser
    {
        // The unique identifier for the user.
        // This property is marked as the primary key for the database.
        [Key]
        public int BuserId { get; set; }

        // The name of the user.
        // Initialized to an empty string to avoid null reference issues.
        public string name { get; set; } = "";

        // The email address of the user.
        // Initialized to an empty string to avoid null reference issues.
        public string mail { get; set; } = "";

        // The password of the user (hashed or encrypted).
        // Initialized to an empty string to avoid null reference issues.
        public string password { get; set; } = "";

        // The phone number of the user.
        // Initialized to an empty string to avoid null reference issues.
        public string phone { get; set; } = "";

        // The birth date of the user.
        // This will be set when the object is created.
        public DateTime bornDate { get; set; }

        // The age of the user, calculated based on the birth date.
        // This property should be updated automatically or manually based on the current date.
        public int age { get; set; }

        // The sex of the user (e.g., "Male", "Female").
        // Initialized to an empty string to avoid null reference issues.
        public string sex { get; set; } = "";

        // A unique token associated with the user's session or authentication.
        // Initialized to an empty string to avoid null reference issues.
        public string token { get; set; } = "";

        // A relational token, possibly used for linking or associating the user with another entity (Specialist or Patient).
        // Defaults to "000000".
        public string relationalToken { get; set; } = "000000";

        // The terms and conditions agreed upon by the user.
        // Initialized with a new instance of the TermsAndConditions class.
        //public TermsAndConditions termsAndConditions { get; set; } = new TermsAndConditions();

        // The date and time when the user was created in the system.
        // Defaults to the current date and time when the object is instantiated.
        public DateTime dateCreated { get; set; } = DateTime.Now;

        // The date and time when the user information was last modified.
        // Defaults to the current date and time when the object is instantiated.
        public DateTime modifiedDate { get; set; } = DateTime.Now;
    }
}







