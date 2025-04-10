using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Contracts
{
    public interface IBadgeRepository
    {
        /// <summary>
        /// Adds a new badge to the database.
        /// </summary>
        /// <param name="badge">The badge to add.</param>
        /// <returns>The ID of the added badge.</returns>
        int AddBadge(Badge badge);

        /// <summary>
        /// Updates an existing badge in the database.
        /// </summary>
        /// <param name="badge">The badge to update.</param>
        /// <returns>True if the update was successful; otherwise, false.</returns>
        bool UpdateBadge(Badge badge);

        /// <summary>
        /// Deletes a badge from the database.
        /// </summary>
        /// <param name="badgeId">The ID of the badge to delete.</param>
        /// <returns>True if the deletion was successful; otherwise, false.</returns>
        bool DeleteBadge(int badgeId);

        /// <summary>
        /// Gets a badge by its ID.
        /// </summary>
        /// <param name="badgeId">The ID of the badge to retrieve.</param>
        /// <returns>The badge with the specified ID, or null if not found.</returns>
        Badge? GetBadge(int badgeId);

        /// <summary>
        /// Gets all badges from the database.
        /// </summary>
        /// <returns>A list of all badges.</returns>
        List<Badge>? GetBadges();
    }
}