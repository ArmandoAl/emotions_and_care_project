using Data.Contracts;
using Data.Helpers;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Data.Implementations
{
    /// <summary>
    /// Implementation of the IDummyUserRepository interface for managing DummyUser data access operations.
    /// </summary>
    public class DummyUserRepository : IDummyUserRepository
    {
        /// <summary>
        /// Adds a new DummyUser to the database.
        /// </summary>
        /// <param name="dummyUser">The DummyUser to add.</param>
        /// <returns>The ID of the added DummyUser, or 0 if the operation failed.</returns>
        public int Add(DummyUser dummyUser)
        {
            if (dummyUser == null) return 0;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                .Options;

            using (var db = new DBContext(options: connectionOptions))
            {
                // Initialize the badge collection if it's null
                if (dummyUser.badgeCollection == null)
                {
                    dummyUser.badgeCollection = new BadgeCollection();
                }

                // Set the user ID in the badge collection
                dummyUser.badgeCollection.userId = dummyUser.BuserId;

                db.dummyUsers.Add(dummyUser);
                db.SaveChanges();

                return dummyUser.BuserId;
            }
        }

        /// <summary>
        /// Gets a DummyUser by its ID.
        /// </summary>
        /// <param name="id">The ID of the DummyUser to retrieve.</param>
        /// <returns>The DummyUser if found, null otherwise.</returns>
        public DummyUser? Get(int id)
        {
            if (id <= 0) return null;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                .Options;

            using (var db = new DBContext(options: connectionOptions))
            {
                return db.dummyUsers
                    .Include(u => u.badgeCollection)
                    .ThenInclude(bc => bc.userBadges)
                    .ThenInclude(ub => ub.badge)
                    .FirstOrDefault(u => u.BuserId == id);
            }
        }

        /// <summary>
        /// Deletes a DummyUser by its ID.
        /// </summary>
        /// <param name="id">The ID of the DummyUser to delete.</param>
        /// <returns>True if the DummyUser was deleted, false otherwise.</returns>
        public bool Delete(int id)
        {
            if (id <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                .Options;

            using (var db = new DBContext(options: connectionOptions))
            {
                var dummyUser = db.dummyUsers
                    .Include(u => u.badgeCollection)
                    .FirstOrDefault(u => u.BuserId == id);

                if (dummyUser == null)
                {
                    return false;
                }

                // Remove the badge collection if it exists
                if (dummyUser.badgeCollection != null)
                {
                    db.badgeCollections.Remove(dummyUser.badgeCollection);
                }

                db.dummyUsers.Remove(dummyUser);
                db.SaveChanges();
                return true;
            }
        }

        /// <summary>
        /// Adds a badge to a specific dummy user.
        /// </summary>
        /// <param name="dummyUserId">The ID of the dummy user.</param>
        /// <param name="badgeId">The ID of the badge to add.</param>
        /// <returns>True if the badge was added successfully, false otherwise.</returns>
        public bool AddBadgeToDummy(int dummyUserId, int badgeId)
        {
            if (dummyUserId <= 0 || badgeId <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                .Options;

            using (var db = new DBContext(options: connectionOptions))
            {
                var dummyUser = db.dummyUsers
                    .Include(u => u.badgeCollection)
                    .ThenInclude(bc => bc.userBadges)
                    .FirstOrDefault(u => u.BuserId == dummyUserId);

                if (dummyUser == null)
                    return false;

                var badge = db.badges.Find(badgeId);
                if (badge == null)
                    return false;

                // Check if the user already has this badge
                if (dummyUser.badgeCollection.userBadges.Any(ub => ub.badgeId == badgeId))
                    return true; // User already has this badge

                var userBadge = new UserBadge
                {
                    badgeId = badgeId,
                    progress = 100, // Assuming the badge is earned immediately
                    dateEarned = DateTime.Now
                };

                dummyUser.badgeCollection.userBadges.Add(userBadge);
                db.SaveChanges();
                return true;
            }
        }

        /// <summary>
        /// Adds all available badges to all dummy users.
        /// </summary>
        /// <returns>True if all badges were added successfully, false otherwise.</returns>
        public bool AddAllBadgesToDummies()
        {
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                .Options;

            using (var db = new DBContext(options: connectionOptions))
            {
                var dummyUsers = db.dummyUsers
                    .Include(u => u.badgeCollection)
                    .ThenInclude(bc => bc.userBadges)
                    .ToList();

                var badges = db.badges.ToList();

                foreach (var dummyUser in dummyUsers)
                {
                    foreach (var badge in badges)
                    {
                        // Skip if user already has this badge
                        if (dummyUser.badgeCollection.userBadges.Any(ub => ub.badgeId == badge.badgeId))
                            continue;

                        var userBadge = new UserBadge
                        {
                            badgeId = badge.badgeId,
                            progress = 0, // Assuming the badge is earned immediately
                        };

                        dummyUser.badgeCollection.userBadges.Add(userBadge);
                    }
                }

                db.SaveChanges();
                return true;
            }
        }
    }
} 