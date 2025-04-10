using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{

    public class Badge
    {
        /// <summary>
        /// Gets or sets the unique identifier for the badge. These are basically achievements.
        /// </summary>
        [Key]
        public int badgeId { get; set; }

        /// <summary>
        /// Gets or sets the name of the badge.
        /// </summary>
        public string name { get; set; } = "";

        /// <summary>
        /// Gets or sets the description of the badge.
        /// </summary>
        public string description { get; set; } = "";

        /// <summary>
        /// Gets or sets the image URL of the badge.
        /// </summary>
        public string imageUrl { get; set; } = "";

        public string progressMap { get; set; } = "";

        public DateTime dateCreated { get; set; } = DateTime.Now;
        public DateTime dateModified { get; set; } = DateTime.Now;
    }

    public class UserBadge
    {
        /// <summary>
        /// Gets or sets the unique identifier for the user badge.
        /// </summary>
        [Key]
        public int userBadgeId { get; set; }

        /// <summary>
        /// Gets or sets the badge associated with the user.
        /// </summary>
        public Badge badge { get; set; } = new Badge();

        public int progress { get; set; } = 0;

        public DateTime? dateEarned { get; set; }

        public DateTime dateCreated { get; set; } = DateTime.Now;


    }
}

/*




*/