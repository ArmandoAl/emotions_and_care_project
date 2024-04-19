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
    public class RecomendacionRepository : IRecomendacionRepository
    {
        public int Add(Recomendacion recomendacion)
        {
            if(recomendacion == null) return 0;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
          .UseSqlServer(Data.Helpers.Constants.ConnectionString)
          .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                db.Recomendaciones.Add(recomendacion);
                db.SaveChanges();
                return recomendacion.IdRecomendacion;
            }
        }

        public bool AddRecomendacionCompletada(int idRecomendacion, int idUsuario)
        {
            if (idRecomendacion <= 0 || idUsuario <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
          .UseSqlServer(Data.Helpers.Constants.ConnectionString)
          .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var recomendacion = db.Recomendaciones.Find(idRecomendacion);
                if (recomendacion == null) return false;
                var usuario = db.Pacientes.FirstOrDefault(u => u.IdUsuario == idUsuario);
                if (usuario == null) return false;
                var recomendacionCompletada = new RecomendacionCompletada
                {
                    IdRecomendacion = idRecomendacion,
                    IdUsuario = idUsuario
                };
                
                recomendacion.recomendacionCompletadas.Add(recomendacionCompletada);
                db.Recomendaciones.Update(recomendacion);
                db.SaveChanges();
                return true;
            }
        }

        public bool Delete(int idRecomendacion)
        {
           
            if (idRecomendacion <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
          .UseSqlServer(Data.Helpers.Constants.ConnectionString)
          .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var recomendacion = db.Recomendaciones.FirstOrDefault(r => r.IdRecomendacion == idRecomendacion);
                if (recomendacion == null) return false;
                db.Recomendaciones.Remove(recomendacion);
                db.SaveChanges();
                return true;
            }
        }

        public Recomendacion? Get(int idRecomendacion)
        {
          
            if (idRecomendacion <= 0) return null;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
          .UseSqlServer(Data.Helpers.Constants.ConnectionString)
          .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                return db.Recomendaciones.FirstOrDefault(r => r.IdRecomendacion == idRecomendacion);
            }
        }

        public List<Recomendacion> GetRecomentaciones()
        {
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
          .UseSqlServer(Data.Helpers.Constants.ConnectionString)
          .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                return db.Recomendaciones.ToList();
            }
        }

        public bool Update(Recomendacion recomendacion)
        {
            if (recomendacion == null) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
          .UseSqlServer(Data.Helpers.Constants.ConnectionString)
          .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                db.Recomendaciones.Update(recomendacion);
                db.SaveChanges();
                return true;
            }
        }
    }
}
