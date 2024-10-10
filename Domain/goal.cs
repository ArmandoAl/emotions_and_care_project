using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain
{

    public enum GoalType
    {
        testFollow,
        specialistFollow,
        recomendations,
        community,
    }   
    

    public class Goal
    {
        [Key]
        public int goalId { get; set; }
        public string name { get; set; } = "";
        public string desription { get; set; } = "";
        public GoalType type { get; set; }

        public int? stickerId { get; set; }
        public int? flowerId { get; set; }
    }
}
