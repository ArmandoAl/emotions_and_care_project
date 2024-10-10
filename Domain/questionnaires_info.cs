using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class QuestionnairesInfo
    {
        public List<Questionnaire> questionnaires { get; set; } = new List<Questionnaire>();
        
        public List<CompleteQuestionnaires> completeQuestionnaires { get; set; } = new List<CompleteQuestionnaires>();

        public List<QuestionnairesHistory> questionnairesHistory { get; set; } = new List<QuestionnairesHistory>();
    }
}
