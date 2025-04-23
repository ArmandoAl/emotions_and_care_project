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
            { "validateFirstUser", CheckBadge_ValidateDejaVu },
            { "validateMeself", CheckBadge_BuenCamino },
            { "ElCaminoALaMejora", CheckBadge_ElCaminoALaMejora},
            { "validateFirstSpecialist", CheckBadge_PocoAyuda },
            { "validateFirstAppointment", CheckBadge_HoraDeLaVerdad },
            { "validateFirstDate", CheckBadge_EsoFueMasFacil },
            { "validateThreeDates", CheckBadge_AsistenciaProfesional },
            { "validateFirstLetter", CheckBadge_Consejos },
            { "validateFirstLetterAnswer", CheckBadge_Consejero },
            { "validateThreeLetters", CheckBadge_SacandoMisEmociones },
            { "validateFlowerGrowth", CheckBadge_Crecimiento},
            { "validateFullFlower", CheckBadge_CrecimientoCompleto}
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



        //Badge: 2 = DejaVu
        // The second time the user answers an individual questionnaire
        private static bool CheckBadge_ValidateDejaVu(int userId, int badgeId)
        {
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                .Options;

            using (var db = new DBContext(options: connectionOptions))
            {
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

                bool isBadgeEarned = userBadge.progress >= 2; // Assuming the badge is earned after the second questionnaire
                Console.WriteLine($"User {user.BuserId} has earned the badge: {isBadgeEarned}");

                if (isBadgeEarned)
                {
                    userBadge.dateEarned = DateTime.Now;
                }

                db.SaveChanges();
                return isBadgeEarned;
            }
        }

        //Badge: 3 = BuenCamnio
        // The user improves their score in the questionnaire
        // This function should compare the current score with the previous score
        // and return true if the current score is higher
        private static bool CheckBadge_BuenCamino(int userId, int badgeId)
        {
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                .Options;

            using (var db = new DBContext(options: connectionOptions))
            {
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

                // Assuming the last two questionnaires are the ones to compare
                if (questionnaires.Count < 2) return false;

                var lastQuestionnaire = questionnaires[questionnaires.Count - 1];
                var previousQuestionnaire = questionnaires[questionnaires.Count - 2];

                // Compare scores (assuming they have a Score property)
                //bool isImproved = lastQuestionnaire. > previousQuestionnaire.Score;
                bool isImproved = true; // Placeholder for actual score comparison logic

                Console.WriteLine($"User {user.BuserId} has improved their score: {isImproved}");

                if (isImproved)
                {
                    userBadge.progress = 1; // Assuming full progress for improvement
                    userBadge.dateEarned = DateTime.Now;
                    db.SaveChanges();
                    return true;
                }

                return false;
            }
            
        }

        //Badge: 4 = El Camino A La Mejora
        // The third time the user completes a questionnaire
        // This function should check if the user has completed 3 questionnaires
        private static bool CheckBadge_ElCaminoALaMejora(int userId, int badgeId)
        {
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                .Options;

            using (var db = new DBContext(options: connectionOptions))
            {
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

                bool isBadgeEarned = userBadge.progress >= 3; // Assuming the badge is earned after the third questionnaire
                Console.WriteLine($"User {user.BuserId} has earned the badge: {isBadgeEarned}");

                if (isBadgeEarned)
                {
                    userBadge.dateEarned = DateTime.Now;
                }

                db.SaveChanges();
                return isBadgeEarned;
            }
        }

        //badge: 5 = Un Poco Ayuda
        // The first time the user links their account with a specialist
        // This function should check if the user has linked their account with a specialist

        private static bool CheckBadge_PocoAyuda(int userId, int badgeId)
        {
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                .Options;

            using (var db = new DBContext(options: connectionOptions))
            {
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
                    .Include(p => p.specialist)
                    .FirstOrDefault(p => p.userId == 63);

                if (patient == null || patient.test == null)
                {
                    Console.WriteLine("Test patient not found or has no test data.");
                    return false;
                }

                 bool isLinkedWithSpecialist = patient.specialist != null;

                Console.WriteLine($"User {user.BuserId} has linked their account with a specialist: {isLinkedWithSpecialist}");

                if (isLinkedWithSpecialist)
                {
                    userBadge.progress = 1; // Assuming full progress for linking
                    userBadge.dateEarned = DateTime.Now;
                    db.SaveChanges();
                    return true;
                }

                return false;
            }
            
        }

        //Badge 6: La Hora de la Verdad
        // The first time the user confirms (or is confirmed) an appointment (date)
        private static bool CheckBadge_HoraDeLaVerdad(int userId, int badgeId)
        {
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                .Options;

            using (var db = new DBContext(options: connectionOptions))
            {
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
                    .Include(p => p.dates)
                    .FirstOrDefault(p => p.userId == 63);

                if (patient == null || patient.test == null)
                {
                    Console.WriteLine("Test patient not found or has no test data.");
                    return false;
                }

                bool isConfirmedAppointment = patient.dates.Any(d => d.patientConfirm == true || d.specialistConfirm == true);

                Console.WriteLine($"User {user.BuserId} has confirmed an appointment: {isConfirmedAppointment}");

                if (isConfirmedAppointment)
                {
                    userBadge.progress = 1; // Assuming full progress for confirming
                    userBadge.dateEarned = DateTime.Now;
                    db.SaveChanges();
                    return true;
                }

                return false;
            }
            
        }

        //Badge 7: Eso fue más fácil de lo que creí
        // The first time the user attends an appointment (date)
        private static bool CheckBadge_EsoFueMasFacil(int userId, int badgeId)
        {
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                .Options;

            using (var db = new DBContext(options: connectionOptions))
            {
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
                    .Include(p => p.dates)
                    .FirstOrDefault(p => p.userId == 63);

                if (patient == null || patient.test == null)
                {
                    Console.WriteLine("Test patient not found or has no test data.");
                    return false;
                }

                bool isAttendedAppointment = patient.dates.Any(d => d.done == true);

                Console.WriteLine($"User {user.BuserId} has attended an appointment: {isAttendedAppointment}");

                if (isAttendedAppointment)
                {
                    userBadge.progress = 1; // Assuming full progress for attending
                    userBadge.dateEarned = DateTime.Now;
                    db.SaveChanges();
                    return true;
                }

                return false;
            }
            
        }

        //Badge 8: Asistencia profesional, siempre bienvenida
        // Every time the user attends 3 appointments (dates)
        // This function should check if the user has attended 3 appointments
        private static bool CheckBadge_AsistenciaProfesional(int userId, int badgeId)
        {
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                .Options;

            using (var db = new DBContext(options: connectionOptions))
            {
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
                    .Include(p => p.dates)
                    .FirstOrDefault(p => p.userId == 63);

                if (patient == null || patient.test == null)
                {
                    Console.WriteLine("Test patient not found or has no test data.");
                    return false;
                }

                int attendedCount = patient.dates.Count(d => d.done == true);

                Console.WriteLine($"User {user.BuserId} has attended {attendedCount} appointments.");

                if (attendedCount >= 3)
                {
                    userBadge.progress += 1; // Increment progress for every 3 appointments attended
                    userBadge.dateEarned = DateTime.Now;
                    db.SaveChanges();
                    return true;
                }

                return false;
            }
            
        }

        //Badge 9: ¿Consejos?
        // The first time the user creates a letter (cart)
        private static bool CheckBadge_Consejos(int userId, int badgeId)
        {
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                .Options;

            using (var db = new DBContext(options: connectionOptions))
            {
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
                    .Include(p => p.carts)
                    .FirstOrDefault(p => p.userId == 63);

                if (patient == null || patient.test == null)
                {
                    Console.WriteLine("Test patient not found or has no test data.");
                    return false;
                }

                //letters are called carts
                bool hasCreatedLetter = patient.carts.Any(c => c.transmitterId == 63);

                Console.WriteLine($"User {user.BuserId} has created a letter: {hasCreatedLetter}");

                if (hasCreatedLetter)
                {
                    userBadge.progress = 1; // Assuming full progress for creating a letter
                    userBadge.dateEarned = DateTime.Now;
                    db.SaveChanges();
                    return true;
                }

                return false;
            }
            
        }

        //Badge 10: Me dicen, el consejero
        // The first time the user answers a letter (cart)
        // 
        private static bool CheckBadge_Consejero(int userId, int badgeId)
        {
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                .Options;

            using (var db = new DBContext(options: connectionOptions))
            {
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
                    .Include(p => p.carts)
                    .FirstOrDefault(p => p.userId == 63);

                if (patient == null || patient.test == null)
                {
                    Console.WriteLine("Test patient not found or has no test data.");
                    return false;
                }

                //letters are called carts
                bool hasAnsweredLetter = true;

                Console.WriteLine($"User {user.BuserId} has answered a letter: {hasAnsweredLetter}");

                if (hasAnsweredLetter)
                {
                    userBadge.progress = 1; // Assuming full progress for answering a letter
                    userBadge.dateEarned = DateTime.Now;
                    db.SaveChanges();
                    return true;
                }

                return false;
            }
            
        }
        

        //Badge 11: Sacando mis emociones, una carta a la vez
        // The third time the user creates a letter (cart)
        private static bool CheckBadge_SacandoMisEmociones(int userId, int badgeId)
        {
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                .Options;

            using (var db = new DBContext(options: connectionOptions))
            {
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
                    .Include(p => p.carts)
                    .FirstOrDefault(p => p.userId == 63);

                if (patient == null || patient.test == null)
                {
                    Console.WriteLine("Test patient not found or has no test data.");
                    return false;
                }

                //letters are called carts
                int letterCount = patient.carts.Count(c => c.transmitterId == 63);
                Console.WriteLine($"User {user.BuserId} has created {letterCount} letters.");
                if (letterCount >= 3)
                {
                    userBadge.progress = 1; // Assuming full progress for creating a letter
                    userBadge.dateEarned = DateTime.Now;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        //Badge 12: Qué rápido crece…
        // The first time a plant advances a stage, we are going to use a user's progress for that
        private static bool CheckBadge_Crecimiento(int userId, int badgeId)
        {
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                .Options;

            using (var db = new DBContext(options: connectionOptions))
            {
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
                    .Include(p => p.progress)
                    .FirstOrDefault(p => p.userId == 63);

                if (patient == null || patient.test == null)
                {
                    Console.WriteLine("Test patient not found or has no test data.");
                    return false;
                }

                // Assuming the progress stage is the one that indicates growth
                bool hasAdvancedStage = patient.progress.stage > 0;

                Console.WriteLine($"User {user.BuserId} has advanced a stage: {hasAdvancedStage}");

                if (hasAdvancedStage)
                {
                    userBadge.progress = 1; // Assuming full progress for advancing a stage
                    userBadge.dateEarned = DateTime.Now;
                    db.SaveChanges();
                    return true;
                }

                return false;
            }
            
        }

        //Badge 13: Juro que cabía en mi bolsillo la última vez que la vi.
        // The first time a plant grows completely, stage 6
        private static bool CheckBadge_CrecimientoCompleto(int userId, int badgeId)
        {
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                .Options;

            using (var db = new DBContext(options: connectionOptions))
            {
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
                    .Include(p => p.progress)
                    .FirstOrDefault(p => p.userId == 63);

                if (patient == null || patient.test == null)
                {
                    Console.WriteLine("Test patient not found or has no test data.");
                    return false;
                }

                // Assuming the progress stage is the one that indicates growth
                bool hasGrownCompletely = patient.progress.stage >= 6;

                Console.WriteLine($"User {user.BuserId} has grown completely: {hasGrownCompletely}");

                if (hasGrownCompletely)
                {
                    userBadge.progress = 1; // Assuming full progress for growing completely
                    userBadge.dateEarned = DateTime.Now;
                    db.SaveChanges();
                    return true;
                }

                return false;
            }
        }










        



    }
} 

/*


| Badge Name                                                  | How to Get It                                                                                 |
| ----------------------------------------------------------- | --------------------------------------------------------------------------------------------- |
| **Esa fue la cuestión**                                     | Primera vez que haces el cuestionario  -o                                                       |
| **Deja Vu**                                                 | Segunda vez que respondes un cuestionario    -o                                                 |
| **Vamos por buen camino**                                   | Mejora de resultado en cuestionario       -1/2                                                    |
| **El camino a la mejora es la frecuencia**                  | Cada vez que completas 3 cuestionarios        -o                                    |
| **Un poco de ayuda profesional nunca sobra**                | Primera vez que vinculas tu cuenta con un especialista    -o                                    |
| **La hora de la verdad**                                    | Primera vez que confirmas (o te confirman) una cita          -o                                 |
| **Eso fue más fácil de lo que creí**                        | Primera vez que asistes a una cita             -o                                               |
| **Asistencia profesional, siempre bienvenida**              | Cada vez que asistes a 3 citas    -o                                         |
| **¿Consejos?**                                              | Primera vez que creas una carta      -o                                                         |
| **Me dicen, el consejero**                                  | Primera vez que respondes una carta    -1/2x                                                      |
| **Sacando mis emociones, una carta a la vez**               | Tercera vez que creas una carta     -o                                                          |
| **Qué rápido crece…**                                       | Primera vez que una planta avanza una etapa      -o                                             |
| **Juro que cabía en mi bolsillo la última vez que la vi.** | Primera vez que una planta crece completamente  -o                                               |
| **Un nuevo comienzo.**                                      | Por primera vez plantas un retoño de una nueva planta                                          |
| **Mi invernadero personal.**                                | Cada vez que 3 plantas completan su crecimiento (repetible cada 3)                            |



*/