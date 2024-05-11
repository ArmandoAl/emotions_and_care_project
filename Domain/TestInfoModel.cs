using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class TestInfoModel
    {
        [Key]
        public int Id { get; set; }

        public string Result { get; set; } = "Pendiente";

        public DateTime Date { get; set; }
        
        public List<TestQuestionWithAnswer> TestQuestionWithAnswers { get; set; } = new List<TestQuestionWithAnswer>();

        public bool Visible { get; set; } = true;
    }

    public class LogroWithTestInfoModel
    {
     public TestInfoModel TestInfoModel { get; set; } = new TestInfoModel();
     public  Logro? Logro { get; set; }
    }
}
