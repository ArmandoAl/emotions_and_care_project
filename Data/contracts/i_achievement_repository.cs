using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Contracts
{
    public interface IAchievementRepository
    {
        /// <summary>
        /// Adds a new achievement to the database.
        /// </summary>
        /// <param name="achievement">The achievement to add.</param>
        /// <returns>The ID of the added achievement.</returns>
        int AddAchievement(Achievement achievement);

        /// <summary>
        /// Updates an existing achievement in the database.
        /// </summary>
        /// <param name="achievement">The achievement to update.</param>
        /// <returns>True if the update was successful; otherwise, false.</returns>
        bool UpdateAchievement(Achievement achievement);

        /// <summary>
        /// Deletes an achievement from the database.
        /// </summary>
        /// <param name="achievementId">The ID of the achievement to delete.</param>
        /// <returns>True if the deletion was successful; otherwise, false.</returns>
        bool DeleteAchievement(int achievementId);

        /// <summary>
        /// Gets an achievement by its ID.
        /// </summary>
        /// <param name="achievementId">The ID of the achievement to retrieve.</param>
        /// <returns>The achievement with the specified ID, or null if not found.</returns>
        Achievement? GetAchievement(int achievementId);

        /// <summary>
        /// Gets all achievements from the database.
        /// </summary>
        /// <returns>A list of all achievements.</returns>
        List<Achievement>? GetAllAchievements();
    }
}