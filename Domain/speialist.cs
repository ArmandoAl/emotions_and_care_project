using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    // The 'Specialist' class represents a specialist user, inheriting from the 'User' class.
    public class Specialist : User
    {
        // The professional license number of the specialist. Defaults to an empty string.
        public string license { get; set; } = "";

        // The focus or specialty of the specialist (e.g., "Cardiologist", "Dermatologist").
        public string focus { get; set; } = "";

        // The institution or organization where the specialist works. This is optional.
        // Defaults to an empty string.
        public string? institution { get; set; } = "";

        // A brief presentation or bio of the specialist. This is optional.
        // Defaults to an empty string.
        public string? presentation { get; set; } = "";

        // The address of the specialist's office or clinic. This is optional.
        // Defaults to an empty string.
        public string? adress { get; set; } = "";

        // A list of 'Cart' objects associated with the specialist's community.
        // Represents the carts where the specialist has been assigned or is involved with.
        public List<Cart> communityCarts { get; set; } = new List<Cart>();

        // A list of 'Date' objects representing the specialist's scheduled dates (appointments).
        public List<Date> dates { get; set; } = new List<Date>();

        // A list of 'Patient' objects associated with the specialist.
        // Represents the patients that the specialist is treating or working with.
        public List<Patient> patients { get; set; } = new List<Patient>();


        // A list of 'DateRequest' objects representing date requests made by the specialist.
        public List<DateRequest> dateRequests { get; set; } = new List<DateRequest>();

        // A list of 'PatientRequest' objects representing patient requests associated with the specialist.
        public List<PatientRequest> patientsRequests { get; set; } = new List<PatientRequest>();
    }
}
