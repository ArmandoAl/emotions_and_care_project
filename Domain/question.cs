using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{

    public enum QuestionType
    {
        Numeric,
        Boolean,
        MultipleChoice,
        Open
    }
    public class Question
    {
        [Key]
        public int questionId { get; set; }

        public string statement { get; set; } = "";

        public QuestionType type { get; set; }

        public List<Answer>? answers { get; set; } = new List<Answer>();

        public DateTime dateCreated { get; set; } = DateTime.Now;

        public DateTime modifiedDate { get; set; } = DateTime.Now;
    }
}
