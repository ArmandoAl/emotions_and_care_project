
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain {   
    /// <summary>
    /// Represents the user interface settings for a patient.
    /// </summary>
    public class UserInterface
    {
        [Key]
        public int userInterfaceId { get; set; }

        /// <summary>
        /// A list of flowers assigned to the user.
        /// </summary>
        public List<UserFlower> userFlowers { get; set; } = new List<UserFlower>();

        /// <summary>
        /// A list of stickers assigned to the user.
        /// </summary>
        public List<UserSticker> userStickers { get; set; } = new List<UserSticker>();

        /// <summary>
        /// Background image URL or ID for the user interface.
        /// </summary>
        public int backgroundUrl { get; set; }

        /// <summary>
        /// The ID of the theme used by the patient.
        /// </summary>
        public int themeId { get; set; }
    }
    }