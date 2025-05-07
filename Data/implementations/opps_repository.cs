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
                //
                return db.opps.Where(x => x.BuserId == id)
                .Include(x => x.bukayoSakaDiary)
                .ThenInclude(x => x.SakaNotes)
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
        public int AddSakaNote(SakaNotes note, int userId)
        {
            if(note == null || userId <= 0) return 0;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
            .UseSqlServer(Data.Helpers.Constants.ConnectionString)
            .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                //var patient = db.patients.Where(x => x.userId == idPatient).Include(x => x.diary).FirstOrDefault();
                //And use the BukayoDiary
                var opps = db.opps.Where(x => x.BuserId == userId).Include(x => x.bukayoSakaDiary).FirstOrDefault();

                if (opps == null) return 0;

                var dairy = opps.bukayoSakaDiary;

                if (dairy == null) return 0;

                dairy.SakaNotes.Add(note);
                db.SaveChanges();
                return note.Id;
            }
        }
    }
}