using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    // Represents the settings for a user or system configuration.
    public class SettingsP
    {
        // The unique identifier for the settings.
        // This is marked as the primary key for the database.
        [Key]
        public int settingsId { get; set; }

        // Indicates whether notifications are active.
        // Default is set to 'true', meaning notifications are enabled by default.
        public bool notificationsActive { get; set; } = true;

        // Indicates whether the diary feature is active.
        // Default is set to 'true', meaning the diary is enabled by default.
        public bool diaryActive { get; set; } = true;

        // Indicates whether the questionnaire feature is active.
        // Default is set to 'true', meaning the questionnaire is enabled by default.
        public bool questionnaireActive { get; set; } = true;
    }
}
