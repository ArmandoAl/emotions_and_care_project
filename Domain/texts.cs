using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    //enum for text Types
    public enum TextType
    {
        //Tutorial
        Tutorial,

        // Terms and Conditions
        TermsAndConditions,

        // Privacy Policy
        Questionnaires,

        // About
        AboutUs,


    }
    // These are the texts used in the application, put documentation in xml format
    public class InAppText
    {
        [Key]
        public int textId { get; set; }
        public string text { get; set; }

        // The type of text (e.g., tutorial, terms and conditions, etc.)
        public TextType textType { get; set; } = TextType.Tutorial;

        // The date and time when the text was created in the system.
        // Defaults to the current date and time when the object is instantiated.
        public DateTime dateCreated { get; set; } = DateTime.Now;

        // The date and time when the user information was last modified.
        // Defaults to the current date and time when the object is instantiated.
        public DateTime modifiedDate { get; set; } = DateTime.Now;

    }
}