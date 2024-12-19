using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{

    /// <summary>
    /// Represents an answer in the emotions and care project.
    /// </summary>
    public class Answer
    {
        /// <summary>
        /// Gets or sets the unique identifier for the answer.
        /// </summary>
        [Key]
        public int answerId { get; set; }

        /// <summary>
        /// Gets or sets the text of the answer.
        /// </summary>
        public string answerText { get; set; } = "";

        /// <summary>
        /// Gets or sets the value associated with the answer.
        /// </summary>
        public int value { get; set; } = 0;

        /// <summary>
        /// Gets or sets the creation date of the answer.
        /// </summary>
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        /// Gets or sets the modification date of the answer.
        /// </summary>
        public DateTime FechaModificacion { get; set; }
    }
}
