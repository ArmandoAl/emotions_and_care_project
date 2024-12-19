using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    // The 'QuestionnairesHistory' class represents the history of a specific questionnaire.
    public class QuestionnairesHistory
    {
        // The primary key for the 'QuestionnairesHistory' entity.
        [Key]
        public int questionnairesHistoryId { get; set; }

        // The unique identifier of the associated questionnaire.
        public int questionnaireId { get; set; }

        // The name of the questionnaire history entry. Defaults to an empty string.
        public string name { get; set; } = "";

        // A list of 'TestInfoModel' objects related to the questionnaire history entry.
        // Represents the individual test results or data related to this history entry.
        public List<TestInfoModel> testInfoModels { get; set; } = new List<TestInfoModel>();
    }
}

