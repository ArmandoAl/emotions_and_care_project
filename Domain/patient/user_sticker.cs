
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    /// <summary>
    /// Represents a sticker assigned to the user in their interface. These are given when a user does a recommendation.
    /// </summary>
    public class UserSticker
    {
        [Key]
        public int userStickerId { get; set; }

        /// <summary>
        /// The sticker object assigned to the user.
        /// </summary>
        public Sticker sticker { get; set; } = new Sticker();

        public int timesEarned { get; set; } = 0; //Should be 1, when the sticker is given for the first time

        /// <summary>
        /// The position of the sticker (nullable, for UI arrangement).
        /// </summary>
        public int? position { get; set; }

        public DateTime dayGiven { get; set; } = DateTime.Now;
        public DateTime latestUpdate { get; set; } = DateTime.Now;
    }



}