
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    /// <summary>
    /// Represents a flower assigned to the user in their interface.
    /// </summary>
    public class UserFlower
    {
        [Key]
        public int userFlowerId { get; set; }

        /// <summary>
        /// The flower object assigned to the user.
        /// </summary>
        public Flower flower { get; set; } = new Flower();

        /// <summary>
        /// The state of the flower (e.g., active, blooming, etc.).
        /// </summary>
        public int state { get; set; }

        /// <summary>
        /// Whether the flower is active (true/false).
        /// </summary>
        public bool active { get; set; } = false;

        /// <summary>
        /// The position of the flower (nullable, for UI arrangement).
        /// </summary>
        public int? position { get; set; }
    }
}