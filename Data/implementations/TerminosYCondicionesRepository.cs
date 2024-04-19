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
    public class TerminosYCondicionesRepository : ITerminosYCondicionesRepository
    {

        public int AddTerminosYCondiciones(TerminosYCondiciones terminosYCondiciones)
        {
            if (terminosYCondiciones == null) return 0;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
            .UseSqlServer(Data.Helpers.Constants.ConnectionString)
            .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                db.TerminosYCondiciones.Add(terminosYCondiciones);
                db.SaveChanges();
                return terminosYCondiciones.IdTerminosYCondiciones;
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
                var terminosYCondiciones = db.TerminosYCondiciones.FirstOrDefault(x => x.IdTerminosYCondiciones == idTerminosYCondiciones);
                if (terminosYCondiciones == null) return false;

                db.TerminosYCondiciones.Remove(terminosYCondiciones);
                db.SaveChanges();
                return true;
            }

            }

        public TerminosYCondiciones? GetTerminosYCondiciones(int idTerminosYCondiciones)

        {
            if (idTerminosYCondiciones <= 0) return null;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                return db.TerminosYCondiciones.FirstOrDefault(x => x.IdTerminosYCondiciones == idTerminosYCondiciones)!;
            }
        }   

        public bool UpdateTerminosYCondiciones(TerminosYCondiciones terminosYCondiciones)
        {
            if (terminosYCondiciones == null) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                db.TerminosYCondiciones.Update(terminosYCondiciones);
                db.SaveChanges();
                return true;
            }
        }

        }

    }


