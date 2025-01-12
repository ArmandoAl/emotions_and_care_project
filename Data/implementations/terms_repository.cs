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
    /// <summary>
    /// Repository implementation for managing Terms and Conditions entities.
    /// </summary>
    public class TerminosYCondicionesRepository : ITermsAndConditionsRepository
    {
        /// <summary>
        /// Adds a new Terms and Conditions record to the database.
        /// </summary>
        /// <param name="terminosYCondiciones">The Terms and Conditions entity to add.</param>
        /// <returns>The ID of the newly added Terms and Conditions record, or 0 if the input is null.</returns>
        public int AddTerminosYCondiciones(TermsAndConditions terminosYCondiciones)
        {
            if (terminosYCondiciones == null) return 0;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                .Options;

            using (var db = new DBContext(options: connectionOptions))
            {
                db.terms.Add(terminosYCondiciones);
                db.SaveChanges();
                return terminosYCondiciones.termsAndConditionsId;
            }
        }

        /// <summary>
        /// Deletes a Terms and Conditions record from the database by its ID.
        /// </summary>
        /// <param name="idTerminosYCondiciones">The ID of the Terms and Conditions record to delete.</param>
        /// <returns><c>true</c> if the record was deleted successfully; otherwise, <c>false</c>.</returns>
        public bool DeleteTerminosYCondiciones(int idTerminosYCondiciones)
        {
            if (idTerminosYCondiciones <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                .Options;

            using (var db = new DBContext(options: connectionOptions))
            {
                var terminosYCondiciones = db.terms.FirstOrDefault(x => x.termsAndConditionsId == idTerminosYCondiciones);
                if (terminosYCondiciones == null) return false;

                db.terms.Remove(terminosYCondiciones);
                db.SaveChanges();
                return true;
            }
        }

        /// <summary>
        /// Retrieves a Terms and Conditions record from the database by its ID.
        /// </summary>
        /// <param name="idTerminosYCondiciones">The ID of the Terms and Conditions record to retrieve.</param>
        /// <returns>The corresponding Terms and Conditions entity, or <c>null</c> if not found or if the ID is invalid.</returns>
        public TermsAndConditions? GetTerminosYCondiciones(int idTerminosYCondiciones)
        {
            if (idTerminosYCondiciones <= 0) return null;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                .Options;

            using (var db = new DBContext(options: connectionOptions))
            {
                return db.terms.FirstOrDefault(x => x.termsAndConditionsId == idTerminosYCondiciones);
            }
        }

        /// <summary>
        /// Updates an existing Terms and Conditions record in the database.
        /// </summary>
        /// <param name="terminosYCondiciones">The Terms and Conditions entity to update.</param>
        /// <returns><c>true</c> if the record was updated successfully; otherwise, <c>false</c>.</returns>
        public bool UpdateTerminosYCondiciones(TermsAndConditions terminosYCondiciones)
        {
            if (terminosYCondiciones == null) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                .Options;

            using (var db = new DBContext(options: connectionOptions))
            {
                db.terms.Update(terminosYCondiciones);
                db.SaveChanges();
                return true;
            }
        }
    }
}



