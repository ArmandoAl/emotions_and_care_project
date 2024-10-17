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
        public int testInfoModelId { get; set; }

        public string result { get; set; } = "pending";

        public DateTime date { get; set; }
        
        public List<TestQuestionWithAnswer> testQuestionWithAnswers { get; set; } = new List<TestQuestionWithAnswer>();

        public bool visible { get; set; } = true;
    }

    public class GoalWithTestInfoModel
    {
     public TestInfoModel TestInfoModel { get; set; } = new TestInfoModel();
     public  Goal? Logro { get; set; }
    }
}
