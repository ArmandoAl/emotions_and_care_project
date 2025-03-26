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
    public class OppsRepository : IOppsRepository {

        public int Add(OppsAdd opps)
        {
            if(opps == null) return 0;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
            .UseSqlServer(Data.Helpers.Constants.ConnectionString)
            .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var oppsEntity = new Opps();
                oppsEntity.name = opps.Name;
                oppsEntity.mail = opps.Email;
                oppsEntity.password = opps.Password;

                db.opps.Add(oppsEntity);
                db.SaveChanges();
                return oppsEntity.BuserId;
            }
        }

        public Opps? Get(int id)
        {
            if(id <= 0) return null;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
            .UseSqlServer(Data.Helpers.Constants.ConnectionString)
            .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                return db.opps.FirstOrDefault(x => x.BuserId == id);
            }
        }

        public bool Delete(int id)
        {
            if(id <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
            .UseSqlServer(Data.Helpers.Constants.ConnectionString)
            .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var opps = db.opps.FirstOrDefault(x => x.BuserId == id);
                if(opps == null) return false;

                db.opps.Remove(opps);
                db.SaveChanges();
                return true;
            }
        }
    }
}