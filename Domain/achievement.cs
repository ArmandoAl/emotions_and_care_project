using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{

    public enum AchievementCategory
    {
        Cuestionarios,
        Recomendaciones,
        Planta,
        Comunidad,
        SeguimientoConEspecialista,
        Otros
    }

    public class Achievement
    {
        /// <summary>
        /// Gets or sets the unique identifier for the badge. These are basically achievements.
        /// </summary>
        [Key]
        public int achievementId { get; set; }

        /// <summary>
        /// Gets or sets the name of the badge.
        /// </summary>
        public string name { get; set; } = "";

        /// <summary>
        /// Gets or sets the description of the badge.
        /// </summary>
        public string description { get; set; } = "";

        /// <summary>
        /// Gets or sets the category of the badge.
        /// </summary>
        public BadgeCategory category { get; set; } = BadgeCategory.Otros;

        /// <summary>
        /// Gets or sets the image URL of the badge.
        /// </summary>
        public string imageUrl { get; set; } = "";

        public string progressMap { get; set; } = "";

        public DateTime dateCreated { get; set; } = DateTime.Now;
        public DateTime dateModified { get; set; } = DateTime.Now;
    }

    public class AchievementCollection
    {
        [Key]
        public int achievementCollectionId { get; set; }

        /// <summary>
        /// Gets or sets the user ID associated with this badge collection.
        /// </summary>
        //public int userId { get; set; }

        /// <summary>
        /// Gets or sets the list of user badges in this collection.
        /// </summary>
        public List<UserAchievement>? userAchievements { get; set; } = new List<UserAchievement>();

        /// <summary>
        /// Gets or sets the date when this collection was created.
        /// </summary>
        public DateTime dateCreated { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets or sets the date when this collection was last modified.
        /// </summary>
        public DateTime dateModified { get; set; } = DateTime.Now;
    }

    public class UserAchievement
    {
        /// <summary>
        /// Gets or sets the unique identifier for the user badge.
        /// </summary>
        [Key]
        public int userAchievementId { get; set; }

        /// <summary>
        /// Gets or sets the badge associated with the user.
        /// </summary>
        public int achievementId { get; set; }
        public Achievement achievement { get; set; }

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


}