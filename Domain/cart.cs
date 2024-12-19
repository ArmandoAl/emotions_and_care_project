using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    // Enum to define the possible states of a cart (or "card" as requested)
    public enum CartState
    {
        sent,    // Indicates that the cart has been sent
        expired  // Indicates that the cart has expired
    }

    // Represents a cart object within the system
    public class Cart //(CARD)
    {
        // Unique identifier for the cart (primary key in the database)
        [Key]
        public int cartId { get; set; }

        // Identifier for the associated community
        public int communityId { get; set; }    

        // Identifier for the transmitter (the entity sending the cart)
        public int transmitterId { get; set; }

        // Initial of the transmitter's name or identifier
        public char transmitterInitial { get; set; }

        // Type of user associated with the cart (e.g., "Admin", "Customer")
        public string userType { get; set; } = "";

        // Content of the cart, describing the items or products within it
        public string content { get; set; } = "";

        // The current state of the cart (default is 'sent')
        public CartState state { get; set; } = CartState.sent;

        // List of answers associated with the cart (e.g., responses to cart-related questions)
        public List<CartAnswer> cartAnswers { get; set; } = new List<CartAnswer>();

        // Date and time when the cart was created
        public DateTime dateCreated { get; set; } = DateTime.Now;

        // Date and time when the cart was last modified
        public DateTime modifiedDate { get; set; } = DateTime.Now;
    }

    // Represents a goal associated with a cart
    public class GoalWithCart
    {
        // An optional goal associated with the cart (could be null)
        public Goal? goal { get; set; } = new Goal();

        // The unique identifier of the cart associated with the goal
        public int cartId { get; set; }
    }
}
