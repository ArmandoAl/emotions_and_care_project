using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    // The 'TestQuestionWithAnswer' class represents a question and its associated answer.
    public class TestQuestionWithAnswer
    {
        // The primary key for the 'TestQuestionWithAnswer' entity.
        [Key]
        public int testQuestionWithAnswerId { get; set; }

        // The text of the question in the test.
        // Defaults to an empty string.
        public string question { get; set; } = "";

        // The text of the answer to the question in the test.
        // Defaults to an empty string.
        public string answer { get; set; } = "";
    }
}

