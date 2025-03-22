using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain
{
    // Enum representing the type of goal
    public enum GoalType
    {
        // Goal related to follow-up on test results
        testFollow,

        // Goal related to following up with a specialist
        specialistFollow,

        // Goal related to receiving or providing recommendations
        recomendations,

        // Goal related to community engagement
        community,
    }

    // Represents a goal with a specific type, description, and associated stickers or flowers
    public class Goal
    {
        // The unique identifier for the goal (Primary Key)
        [Key]
        public int goalId { get; set; }

        // The name of the goal
        public string name { get; set; } = "";

        // The description of the goal
        public string desription { get; set; } = "";

        // The type of the goal, determined by the GoalType enum
        public GoalType type { get; set; }

        // The optional identifier for a sticker associated with the goal (nullable)
        public int? stickerId { get; set; }

        // The optional identifier for a flower associated with the goal (nullable)
        public int? flowerId { get; set; }

        //propuesta, definir cual es la mejor manera, si hacer otra tabla en la base de datos y asociarla con la tabla de goals o poner la url directamente aqui
        public string? logoUlr { get; set; }

    }
}
