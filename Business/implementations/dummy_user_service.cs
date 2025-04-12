using Business.Contracts;
using Data.Contracts;
using Domain;

namespace Business.Implementations
{
    /// <summary>
    /// Implementation of the IDummyUserService interface.
    /// </summary>
    public class DummyUserService : IDummyUserService
    {
        private readonly IDummyUserRepository _dummyUserRepository;

        /// <summary>
        /// Initializes a new instance of the DummyUserService class.
        /// </summary>
        /// <param name="dummyUserRepository">The repository to use for data access.</param>
        public DummyUserService(IDummyUserRepository dummyUserRepository)
        {
            _dummyUserRepository = dummyUserRepository;
        }

        /// <summary>
        /// Adds a new DummyUser.
        /// </summary>
        /// <param name="dummyUser">The DummyUser to add.</param>
        /// <returns>The ID of the added DummyUser, or 0 if failed.</returns>
        public int Add(DummyUser dummyUser)
        {
            return _dummyUserRepository.Add(dummyUser);
        }

        /// <summary>
        /// Gets a DummyUser by its ID.
        /// </summary>
        /// <param name="id">The ID of the DummyUser to retrieve.</param>
        /// <returns>The DummyUser if found, null otherwise.</returns>
        public DummyUser? Get(int id)
        {
            return _dummyUserRepository.Get(id);
        }

        /// <summary>
        /// Deletes a DummyUser by its ID.
        /// </summary>
        /// <param name="id">The ID of the DummyUser to delete.</param>
        /// <returns>True if the DummyUser was deleted, false otherwise.</returns>
        public bool Delete(int id)
        {
            return _dummyUserRepository.Delete(id);
        }

        /// <summary>
        /// Adds a badge to a specific dummy user.
        /// </summary>
        /// <param name="dummyUserId">The ID of the dummy user.</param>
        /// <param name="badgeId">The ID of the badge to add.</param>
        /// <returns>True if the badge was added successfully, false otherwise.</returns>
        public bool AddBadgeToDummy(int dummyUserId, int badgeId)
        {
            return _dummyUserRepository.AddBadgeToDummy(dummyUserId, badgeId);
        }

        /// <summary>
        /// Adds all available badges to all dummy users.
        /// </summary>
        /// <returns>True if all badges were added successfully, false otherwise.</returns>
        public bool AddAllBadgesToDummies()
        {
            return _dummyUserRepository.AddAllBadgesToDummies();
        }
    }
} 