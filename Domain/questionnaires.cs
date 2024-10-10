using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain
{
    public class Questionnaire
    {
        [Key]
        public int questionnaireId { get; set; }

        //agregar la descripcion y el objetivo
        public string questionnaireName { get; set; } = "";

        public string description { get; set; } = "";

        public string objective { get; set; } = "";

        public string instructions { get; set; } = "";

        public List<Question> questions { get; set; } = new List<Question>();

        public List<QuestionnaireResult> questionnaireResults { get; set; } = new List<QuestionnaireResult>();

        public DateTime dateCreated { get; set; } = DateTime.Now;

        public DateTime modifiedDate { get; set; } = DateTime.Now;
    }


    public class QuestionnaireResult
    {
        [Key]
        public int questionnaireResultId { get; set; }

        public int value { get; set; } = 0;

        public string result { get; set; } = "";

        public DateTime dateCreated { get; set; } = DateTime.Now;

        public DateTime modifiedDate { get; set; } = DateTime.Now;
    }
}
