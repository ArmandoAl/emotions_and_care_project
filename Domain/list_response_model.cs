using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    // Represents a response model that contains a list of TestQuestionForComplete objects.
    public class ListResponseModel
    {
        // A list that holds the test questions with their associated answers.
        // Initializes the list to avoid null reference issues.
        public List<TestQuestionForComplete> questions { get; set; } = new List<TestQuestionForComplete>();
    }
}

