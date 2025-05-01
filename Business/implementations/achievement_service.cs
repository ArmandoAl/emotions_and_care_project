using Business.Contracts;
using Data.contracts;
using Data.Contracts;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Implementations
{
    public class AchievementService : IAchievementService
    {
        private readonly IAchievementRepository _achievementRepository;

        public AchievementService(IAchievementRepository achievementRepository)
        {
            _achievementRepository = achievementRepository;
        }

        public int AddAchievement(Achievement achievement)
        {
            if (achievement == null) return 0;
            return _achievementRepository.AddAchievement(achievement);
        }

        public Achievement? GetAchievement(int achievementId)
        {
            if (achievementId <= 0) return null;
            return _achievementRepository.GetAchievement(achievementId);
        }

        public List<Achievement>? GetAllAchievements()
        {
            return _achievementRepository.GetAllAchievements();
        }

        public bool UpdateAchievement(Achievement achievement)
        {
            if (achievement == null) return false;
            return _achievementRepository.UpdateAchievement(achievement);
        }

        public bool DeleteAchievement(int achievementId)
        {
            if (achievementId <= 0) return false;
            return _achievementRepository.DeleteAchievement(achievementId);
        }
    }
}