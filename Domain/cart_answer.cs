using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    // Represents an answer to a cart (CARD), containing various metadata about the answer
    public class CartAnswer
    {
        // Unique identifier for each cart answer (Primary Key)
        [Key]
        public int cartAnswerId { get; set; }

        // Identifier for the cart associated with this answer
        public int cartId { get; set; }

        // Identifier for the receiver of the cart answer
        public int receiverId { get; set; }

        // Initial letter representing the receiver's name or identity
        public char receiverInitial { get; set; }

        // Content of the cart answer (default is an empty string if not provided)
        public string content { get; set; } = "";

        // Optional identifier for a sticker associated with the cart answer (nullable)
        public int? stickerId { get; set; }

        // Indicates whether the cart answer has been read or not (default is false)
        public bool read { get; set; } = false;

        // Date and time when the cart answer was created (default is current date and time)
        public DateTime dateCreated { get; set; } = DateTime.Now;

        // Date and time when the cart answer was last modified (default is current date and time)
        public DateTime modifiedDate { get; set; } = DateTime.Now;
    }

    // Represents a goal associated with a specific cart answer
    public class GoalWithCartAnswer
    {
        // The goal associated with the cart answer (nullable)
        public Goal? goal { get; set; } = new Goal();

        // The identifier of the cart answer
        public int cartAnswerId { get; set; }
    }
}
