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
    public class EmocionRepository : IEmocionRepository
    {
        public int AddEmocion(Emocion emocion)
        {
            if (emocion == null) return 0;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                db.Emociones.Add(emocion);
                db.SaveChanges();
                return emocion.IdEmocion;
            }
        }

        
        public bool DeleteEmocion(int idEmocion)
        {
            if (idEmocion <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var emocion = db.Emociones.FirstOrDefault(x => x.IdEmocion == idEmocion);
                if (emocion == null) return false;

                db.Emociones.Remove(emocion);
                db.SaveChanges();
                return true;
            }
        }

        public Emocion? GetEmocion(int idEmocion)
        {
            if (idEmocion <= 0) return null;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                return db.Emociones.FirstOrDefault(x => x.IdEmocion == idEmocion);
            }
        }

        public List<Emocion> GetEmociones()
        {
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                return db.Emociones.ToList();
            }
        }

        public bool UpdateEmocion(Emocion emocion)
        {
            if (emocion == null) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var thisEmocion = db.Emociones.FirstOrDefault(x => x.IdEmocion == emocion.IdEmocion);
                if (thisEmocion == null) return false;

                db.Emociones.Update(emocion);
                db.SaveChanges();
                return true;
            }
        }

        public List<Emocion>? GetEmocionesByPaciente(int idPaciente)
        {
            if (idPaciente <= 0) return null;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                return db.Emociones.ToList();
            }
        }

        }
    }


        