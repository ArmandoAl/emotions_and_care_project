using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{

    public class Answer
    {
        [Key]
        public int answerId { get; set; }

        public string answerText { get; set; } = "";

        public int value { get; set; } = 0;

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaModificacion { get; set; }
    }
}
