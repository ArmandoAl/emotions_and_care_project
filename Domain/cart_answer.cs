using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class CartAnswer
    {
        [Key]
        public int cartId { get; set; }

        public int receiverId { get; set; }

        public char receiverInitial { get; set; }

        public string content { get; set; } = "";

        public int? stickerId { get; set; }

        public bool read { get; set; } = false;

        public DateTime dateCreated { get; set; } = DateTime.Now;

        public DateTime modifiedDate { get; set; } = DateTime.Now;
    }

    public class GoalWithCartAnswer
    {
        public Goal? Logro { get; set; } = new Goal();
        public int IdRespuestaCarta { get; set; }
    }
}
