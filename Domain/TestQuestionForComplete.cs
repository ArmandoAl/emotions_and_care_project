using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    // Represents a test question and its corresponding answer.
    public class TestQuestionForComplete
    {
        // The unique identifier for the question.
        public int questionId { get; set; }

        // The unique identifier for the answer to the question.
        public int answerId { get; set; }
    }
}
