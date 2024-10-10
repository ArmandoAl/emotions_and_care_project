using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class TestQuestionWithAnswer
    {
        [Key]
        public int testQuestionWithAnswerId { get; set; }
        public string question { get; set; } = "";
        public string answer { get; set; } = "";

    }
}
