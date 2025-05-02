    
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    
    /// <summary>
    /// Represents a questionnaire assigned to a patient.
    /// </summary>
    public class QuestionnaireForUser
    {
        [Key]
        public int questionnaireForUserId { get; set; }

        /// <summary>
        /// The ID of the questionnaire.
        /// </summary>
        public int questionnaireId { get; set; }
    }

}