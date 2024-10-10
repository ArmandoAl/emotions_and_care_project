using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class SettingsP
    {
        public int settingsId { get; set; }

        public bool notificationsActive { get; set; } = true;

        public bool diaryActive { get; set; } = true;

        public bool questionnaireActive { get; set; } = true;
    }
}
