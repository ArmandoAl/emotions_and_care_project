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
    public class TerminosYCondicionesRepository : ITermsAndConditionsRepository
    {

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
        public bool DeleteTerminosYCondiciones(int idTerminosYCondiciones)
        {
            if(idTerminosYCondiciones <= 0) return false;

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

        public TermsAndConditions? GetTerminosYCondiciones(int idTerminosYCondiciones)

        {
            if (idTerminosYCondiciones <= 0) return null;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                return db.terms.FirstOrDefault(x => x.termsAndConditionsId == idTerminosYCondiciones)!;
            }
        }   

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


