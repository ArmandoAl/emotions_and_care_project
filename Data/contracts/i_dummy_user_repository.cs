using Domain;

namespace Data.Contracts
{
    /// <summary>
    /// Interface for managing DummyUser data access operations.
    /// </summary>
    public interface IDummyUserRepository
    {
        /// <summary>
        /// Adds a new DummyUser to the database.
        /// </summary>
        /// <param name="dummyUser">The DummyUser to add.</param>
        /// <returns>The ID of the added DummyUser, or 0 if the operation failed.</returns>
        int Add(DummyUser dummyUser);

        /// <summary>
        /// Gets a DummyUser by its ID.
        /// </summary>
        /// <param name="id">The ID of the DummyUser to retrieve.</param>
        /// <returns>The DummyUser if found, null otherwise.</returns>
        DummyUser? Get(int id);

        /// <summary>
        /// Deletes a DummyUser by its ID.
        /// </summary>
        /// <param name="id">The ID of the DummyUser to delete.</param>
        /// <returns>True if the DummyUser was deleted, false otherwise.</returns>
        bool Delete(int id);

        /// <summary>
        /// Adds a badge to a specific dummy user.
        /// </summary>
        /// <param name="dummyUserId">The ID of the dummy user.</param>
        /// <param name="badgeId">The ID of the badge to add.</param>
        /// <returns>True if the badge was added successfully, false otherwise.</returns>
        bool AddBadgeToDummy(int dummyUserId, int badgeId);

        /// <summary>
        /// Adds all available badges to all dummy users.
        /// </summary>
        /// <returns>True if all badges were added successfully, false otherwise.</returns>
        bool AddAllBadgesToDummies();

        /// <summary>
        /// Checks if a user has any badges.
        /// </summary>
        /// <param name="userId">The ID of the user to check.</param>
        /// <returns>A list of booleans indicating whether the user has each badge.</returns>
        /// <remarks>
        List<bool> CheckBadges(int userId);
    }
} 