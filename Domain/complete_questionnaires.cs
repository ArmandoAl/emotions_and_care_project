using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    /// <summary>
    /// Represents a completed questionnaire for a patient.
    /// </summary>
    public class CompleteQuestionnaires
    {
        /// <summary>
        /// Gets or sets the unique identifier for a completed questionnaire entry.
        /// </summary>
        [Key]
        public int completeQuestionnaireId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the patient who completed the questionnaire.
        /// </summary>
        public int patientId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the questionnaire.
        /// </summary>
        public int questionnaireId { get; set; }

        /// <summary>
        /// Gets or sets the date when the questionnaire was completed.
        /// </summary>
        public DateTime dateCompleted { get; set; }
    }
}
