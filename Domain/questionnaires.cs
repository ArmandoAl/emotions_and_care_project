using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain
{
    // Represents a questionnaire with details such as name, description, objective, instructions, and questions
    public class Questionnaire
    {
        // Unique identifier for the questionnaire (primary key in the database)
        [Key]
        public int questionnaireId { get; set; }

        // The name of the questionnaire
        public string questionnaireName { get; set; } = "";

        // A description of the questionnaire, providing more context
        public string description { get; set; } = "";

        // The objective of the questionnaire, explaining its purpose
        public string objective { get; set; } = "";

        // Instructions on how to complete the questionnaire
        public string instructions { get; set; } = "";

        // A list of questions that belong to this questionnaire
        public List<Question> questions { get; set; } = new List<Question>();

        // A list of results for this questionnaire (tracking multiple questionnaire results over time or for different users)
        public List<QuestionnaireResult> questionnaireResults { get; set; } = new List<QuestionnaireResult>();

        // The date and time when the questionnaire was created
        public DateTime dateCreated { get; set; } = DateTime.Now;

        // The date and time when the questionnaire was last modified
        public DateTime modifiedDate { get; set; } = DateTime.Now;
    }

    // Represents the result of a questionnaire submission, storing the value and result
    public class QuestionnaireResult
    {
        // Unique identifier for the questionnaire result (primary key in the database)
        [Key]
        public int questionnaireResultId { get; set; }

        // The numeric value associated with the result (could represent a score, ranking, or points)
        public int value { get; set; } = 0;

        // The textual result of the questionnaire (e.g., "Passed", "Failed", "Completed", etc.)
        public string result { get; set; } = "";

        // The date and time when the result was created
        public DateTime dateCreated { get; set; } = DateTime.Now;

        // The date and time when the result was last modified
        public DateTime modifiedDate { get; set; } = DateTime.Now;
    }
}
