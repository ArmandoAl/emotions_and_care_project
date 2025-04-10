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
    public class BadgeRepository : IBadgeRepository
    {
        public int AddBadge(Badge badge)
        {
            if (badge == null) return 0;

            badge.dateCreated = DateTime.Now;
            badge.dateCreated = DateTime.Now;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                db.badges.Add(badge);
                db.SaveChanges();
                return badge.badgeId;
            }
        }

        public bool DeleteBadge(int badgeId)
        {
            if (badgeId <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var badge = db.badges.FirstOrDefault(x => x.badgeId == badgeId);
                if (badge == null) return false;

                db.badges.Remove(badge);
                db.SaveChanges();
                return true;
            }
        }

        public Badge? GetBadge(int badgeId)
        {
            if (badgeId <= 0) return null;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                return db.badges.FirstOrDefault(x => x.badgeId == badgeId);
            }
        }

        public List<Badge>? GetBadges()
        {
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                return db.badges.ToList();
            }
        }

        public bool UpdateBadge(Badge badge)
        {
            if (badge == null) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var existingBadge = db.badges.FirstOrDefault(x => x.badgeId == badge.badgeId);
                if (existingBadge == null) return false;

                existingBadge.name = badge.name;
                existingBadge.description = badge.description;
                existingBadge.imageUrl = badge.imageUrl;
                existingBadge.progressMap = badge.progressMap;
                existingBadge.dateModified = DateTime.Now;
                db.SaveChanges();
                return true;
            }
        }
        
    }
}