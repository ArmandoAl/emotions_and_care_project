using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class QuestionnairesHistory
    {
        [Key]
        public int questionnairesHistoryId { get; set; }

        public int questionnaireId { get; set; }
        
        public string name { get; set; } = "";

        public List<TestInfoModel> testInfoModels { get; set; } = new List<TestInfoModel>();
    }
}
