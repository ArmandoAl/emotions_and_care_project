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

    public class BadgeCollection
    {
        [Key]
        public int badgeCollectionId { get; set; }

        /// <summary>
        /// Gets or sets the user ID associated with this badge collection.
        /// </summary>
        public int userId { get; set; }

        /// <summary>
        /// Gets or sets the list of user badges in this collection.
        /// </summary>
        public List<UserBadge>? userBadges { get; set; } = new List<UserBadge>();

        /// <summary>
        /// Gets or sets the date when this collection was created.
        /// </summary>
        public DateTime dateCreated { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets or sets the date when this collection was last modified.
        /// </summary>
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
        public int badgeId { get; set; }
        public Badge badge { get; set; }

        /// <summary>
        /// Gets or sets the progress of the user towards earning this badge.
        /// </summary>
        public int progress { get; set; } = 0;

        /// <summary>
        /// Gets or sets the date when the badge was earned (null if not earned yet).
        /// </summary>
        public DateTime? dateEarned { get; set; }

        /// <summary>
        /// Gets or sets the date when this user badge was created.
        /// </summary>
        public DateTime dateCreated { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets or sets the date when this user badge was last modified.
        /// </summary>
        public DateTime dateModified { get; set; } = DateTime.Now;
    }

    public class DummyUser : BUser
    {
        public string favoritePlayer { get; set; } = "Saka";
        public string favoriteTeam { get; set; } = "Arsenal";
        public string favoriteStadium { get; set; } = "Emirates Stadium";

        /// <summary>
        /// Gets or sets the badge collection for this user.
        /// </summary>
        public BadgeCollection badgeCollection { get; set; } = new BadgeCollection();
    }
}

/*




*/