using System.Runtime;
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

public List<progressBool> CheckBadges(int userId)
{
    Console.WriteLine($"Checking badges for user {userId}");
    if (userId <= 0) return new List<progressBool>();

    var connectionOptions = new DbContextOptionsBuilder<DBContext>()
        .UseSqlServer(Data.Helpers.Constants.ConnectionString)
        .Options;

    using (var db = new DBContext(options: connectionOptions))
    {
        var user = db.dummyUsers
            .Include(u => u.badgeCollection)
                .ThenInclude(bc => bc.userBadges)
                    .ThenInclude(ub => ub.badge)
            .FirstOrDefault(u => u.BuserId == userId);

        if (user == null || user.badgeCollection == null)
            return new List<progressBool>();

        List<progressBool> results = new();

        foreach (var userBadge in user.badgeCollection.userBadges
                     .Where(ub => ub.dateEarned == null)) // 👈 Only not-yet-earned badges
        {
            if (_badgeFunctions.TryGetValue(userBadge.badge.progressMap, out var badgeFunction))
            {
                Console.WriteLine($"Checking badge {userBadge.badgeId} - {userBadge.badge.name}");
                Console.WriteLine($"Function: {userBadge.badge.progressMap}");

                bool result = badgeFunction(userId, userBadge.badgeId);

                if (result)
                {
                    results.Add(new progressBool
                    {
                        EntityId = userBadge.badgeId,
                        type = progreesBoolType.Badges
                    });
                }
            }
        }

        return results;
    }
}




        // based on each badge's functionMap, I need a Dictionary to get the function name, 
        // and "assign it a function" the functions will receive the userId, and badgeId
        // and return true or false, depending on the function and update the UserBadge progress

        private readonly Dictionary<string, Func<int, int, bool>> _badgeFunctions = new()
        {
            { "EsaFueLaCuestion", CheckBadge_EsaEsLaCuestion },
            { "validateFirstUser", CheckBadge_2 },
            { "validateMeself", CheckBadge_3 },
            { "ElCaminoALaMejora", CheckBadge_ElCaminoALaMejora}


            // Add more functions as needed
        };

        //Badge: 1 = Esa Es La Cuestion: 
        private static bool CheckBadge_EsaEsLaCuestion(int userId, int badgeId)
{
    var connectionOptions = new DbContextOptionsBuilder<DBContext>()
        .UseSqlServer(Data.Helpers.Constants.ConnectionString)
        .Options;

    using (var db = new DBContext(options: connectionOptions))
    {
        int ProgressTarget = 1;

        // Get the user with badgeCollection and badges
        var user = db.dummyUsers
            .Include(u => u.badgeCollection)
                .ThenInclude(bc => bc.userBadges)
                    .ThenInclude(ub => ub.badge)
            .FirstOrDefault(u => u.BuserId == userId);

        if (user == null || user.badgeCollection == null)
        {
            Console.WriteLine("User or badge collection not found.");
            return false;
        }

        var userBadge = user.badgeCollection.userBadges
            .FirstOrDefault(ub => ub.badgeId == badgeId);

        if (userBadge == null)
        {
            Console.WriteLine("Badge not found for user.");
            return false;
        }

        // Using test patient (id 63)
        var patient = db.patients
            .Include(p => p.test)
                .ThenInclude(t => t.completeQuestionnaires)
            .FirstOrDefault(p => p.userId == 63);

        if (patient == null || patient.test == null)
        {
            Console.WriteLine("Test patient not found or has no test data.");
            return false;
        }

        var questionnaires = patient.test.completeQuestionnaires ?? new List<CompleteQuestionnaires>();

        userBadge.progress = questionnaires.Count;

        Console.WriteLine($"Patient name: {patient.name} (Testing UserId: 63)");
        Console.WriteLine($"User {user.BuserId} has completed {userBadge.progress} questionnaires.");

        bool isBadgeEarned = userBadge.progress >= ProgressTarget;
        Console.WriteLine($"User {user.BuserId} has earned the badge: {isBadgeEarned}");

        if (isBadgeEarned)
        {
            userBadge.dateEarned = DateTime.Now;
        }

        db.SaveChanges();
        return isBadgeEarned;
    }
}



        //Badge: 2 = validateFirstUser
        private static bool CheckBadge_2(int userId, int badgeId)
        {
            // Implement the logic for this badge
            // For example, check if the user is the first user in the database
            // Return true or false based on the condition
            return true; // Placeholder
        }

        //Badge: 3 = validateMeself
        private static bool CheckBadge_3(int userId, int badgeId)
        {
            // Implement the logic for this badge
            // For example, check if the user has completed a specific task
            // Return true or false based on the condition
            return false; // Placeholder
        }

        //Badge: 4 = El Camino A La Mejora
        private static bool CheckBadge_ElCaminoALaMejora(int userId, int badgeId)
        {
            // Implement the logic for this badge
            // For example, check if the user has completed a specific task
            // Return true or false based on the condition
            return false; // Placeholder
        }



    }
} 