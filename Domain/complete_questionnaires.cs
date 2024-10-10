using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class CompleteQuestionnaires
    {
        [Key]
        public int completeQuestionnaireId { get; set; }

        public int patientId { get; set; }

        public int questionnaireId { get; set; }

        public DateTime dateCompleted { get; set; }
    }
}
