using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    // Enum representing different types of questions.
    public enum QuestionType
    {
        // A numeric question, expecting a numeric answer.
        Numeric,

        // A boolean question, expecting a true/false answer.
        Boolean,

        // A multiple choice question, expecting one or more predefined answers.
        MultipleChoice,

        // An open-ended question, expecting a free-text answer.
        Open
    }

    // Represents a question in a test or survey.
    public class Question
    {
        // The unique identifier for the question.
        // This is marked as the primary key for the database.
        [Key]
        public int questionId { get; set; }

        // The statement or text of the question.
        // Initialized to an empty string to ensure it has a value by default.
        public string statement { get; set; } = "";

        // The type of question, such as numeric, boolean, etc.
        // This is an enum representing the question's expected answer type.
        public QuestionType type { get; set; }

        // A list of possible answers for the question.
        // This is nullable, as some questions (e.g., open-ended) might not have predefined answers.
        public List<Answer>? answers { get; set; } = new List<Answer>();

        // The date the question was created.
        // Defaults to the current date and time when the object is instantiated.
        public DateTime dateCreated { get; set; } = DateTime.Now;

        // The date the question was last modified.
        // Defaults to the current date and time when the object is instantiated.
        public DateTime modifiedDate { get; set; } = DateTime.Now;
    }
}

