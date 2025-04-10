using Data.Contracts;
using Data.Implementations;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Implementations
{
    public class DummyRepository : IDummyRepository {

        public int Add(DummyUser dummy)
        {
            if(dummy == null) return 0;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
            .UseSqlServer(Data.Helpers.Constants.ConnectionString)
            .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var dummyUser = new DummyUser {
                    name = dummy.name,
                    mail = dummy.mail,
                    password = dummy.password,
                    dateCreated = DateTime.Now,
                };

                db.dummyUsers.Add(dummyUser);
                db.SaveChanges();
                return dummyUser.userId;
            }
        }

        public DummyUser? Get(int id)
        {
            if(id <= 0) return null;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
            .UseSqlServer(Data.Helpers.Constants.ConnectionString)
            .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                //
                return db.dummyUsers.Where(x => x.userId == id)
                .Include(x => x.badgeCollection)
                .ThenInclude(x => x.userBadges)
                .FirstOrDefault();
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
                var opps = db.opps
                    .Include(o => o.bukayoSakaDiary)   // Include the related Bukayo
                    .ThenInclude(b => b.SakaNotes)     // Include related SakaNotes
                    .FirstOrDefault(o => o.BuserId == id);

                
                if(opps == null) return false;

                // Delete related SakaNotes
                db.sakaNotes.RemoveRange(opps.bukayoSakaDiary.SakaNotes);
                db.bukayos.Remove(opps.bukayoSakaDiary);
                

                db.opps.Remove(opps);
                db.SaveChanges();
                return true;
            }
        }
    }
}