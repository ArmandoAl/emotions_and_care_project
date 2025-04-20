using Data.Contracts;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Implementations
{
    public class AchievementRepository : IAchievementRepository
    {
        public int AddAchievement(Achievement achievement)
        {
            if (achievement == null) return 0;

            achievement.dateCreated = DateTime.Now;
            achievement.dateModified = DateTime.Now;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                db.achievements.Add(achievement);
                db.SaveChanges();
                return achievement.achievementId;
            }
        }

        public bool DeleteAchievement(int achievementId)
        {
            if (achievementId <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var achievement = db.achievements.FirstOrDefault(x => x.achievementId == achievementId);
                if (achievement == null) return false;

                db.achievements.Remove(achievement);
                db.SaveChanges();
                return true;
            }
        }

        public Achievement? GetAchievement(int achievementId)
        {
            if (achievementId <= 0) return null;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                return db.achievements.FirstOrDefault(x => x.achievementId == achievementId);
            }
        }

        public List<Achievement>? GetAllAchievements()
        {
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                return db.achievements.ToList();
            }
        }

        public bool UpdateAchievement(Achievement achievement)
        {
            if (achievement == null || achievement.achievementId <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var existingAchievement = db.achievements.FirstOrDefault(x => x.achievementId ==
                    achievement.achievementId);
                if (existingAchievement == null) return false;
                existingAchievement.name = achievement.name;
                existingAchievement.description = achievement.description;
                existingAchievement.dateModified = DateTime.Now;
                db.SaveChanges();
                return true;
            }
        }
    }
}