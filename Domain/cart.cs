    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{

    public enum CartState
    {
        sent,
           
        expired,
    }
    public class Cart
    {
        [Key]
        public int cartId { get; set; }

        public int communityId { get; set; }    
        public int transmitterId { get; set; }
        public char transmitterInitial { get; set; }

        public string userType { get; set; } = "";
        public string content { get; set; } = "";

        public CartState state { get; set; } = CartState.sent;

        public List<CartAnswer> cartAnswers { get; set; } = new List<CartAnswer>();

        public DateTime dateCreated { get; set; } = DateTime.Now;
        public DateTime modifiedDate { get; set; } = DateTime.Now;
    }

    public class GoalWithCart
    {
        public Goal? Logro { get; set; } = new Goal();
        public int cartiD { get; set; }
    }
}
