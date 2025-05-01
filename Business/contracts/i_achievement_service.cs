using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Contracts
{
    public interface IAchievementService
    {
        int AddAchievement(Achievement achievement);
        Achievement? GetAchievement(int achievementId);
        List<Achievement>? GetAllAchievements();
        bool UpdateAchievement(Achievement achievement);
        bool DeleteAchievement(int achievementId);
    }
}