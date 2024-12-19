using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain
{
    // Represents the information related to questionnaires, including a list of questionnaires,
    // completed questionnaires, and the history of questionnaires.
    public class QuestionnairesInfo
    {
        // List of questionnaires that are available or being tracked
        public List<Questionnaire> questionnaires { get; set; } = new List<Questionnaire>();
        
        // List of completed questionnaires, which could represent questionnaires that have been answered or finalized
        public List<CompleteQuestionnaires> completeQuestionnaires { get; set; } = new List<CompleteQuestionnaires>();

        // List representing the history of questionnaires, which may track previous responses or states
        public List<QuestionnairesHistory> questionnairesHistory { get; set; } = new List<QuestionnairesHistory>();
    }
}
