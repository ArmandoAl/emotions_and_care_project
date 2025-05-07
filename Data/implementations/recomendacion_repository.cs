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
    public class RecomendationRepository : IRecomendationRepository
    {
        public int Add(Recomendation recomendation)
        {
            if(recomendation == null) return 0;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
          .UseSqlServer(Data.Helpers.Constants.ConnectionString)
          .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                db.recomendation.Add(recomendation);
                db.SaveChanges();
                return recomendation.recomendationId;
            }
        }

        // public bool AddRecomendationCompletada(int idRecomendation, int idUsuario)
        // {
        //     if (idRecomendation <= 0 || idUsuario <= 0) return false;

        //     var connectionOptions = new DbContextOptionsBuilder<DBContext>()
        //   .UseSqlServer(Data.Helpers.Constants.ConnectionString)
        //   .Options;
        //     using (var db = new DBContext(options: connectionOptions))
        //     {
        //         var recomendation = db.recomendation.Find(idRecomendation);
        //         if (recomendation == null) return false;
        //         var usuario = db.patients.FirstOrDefault(u => u.userId == idUsuario);
        //         if (usuario == null) return false;
        //         var recomendationCompletada = new RecomendationCompletada
        //         {
        //             IdRecomendation = idRecomendation,
        //             IdUsuario = idUsuario
        //         };
                
        //         recomendation.recomendationCompletadas.Add(recomendationCompletada);
        //         db.Recomendationes.Update(recomendation);
        //         db.SaveChanges();
        //         return true;
        //     }
        // }

        public bool Delete(int idRecomendation)
        {
           
            if (idRecomendation <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
          .UseSqlServer(Data.Helpers.Constants.ConnectionString)
          .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var recomendation = db.recomendation.FirstOrDefault(r => r.recomendationId == idRecomendation);
                if (recomendation == null) return false;
                db.recomendation.Remove(recomendation);
                db.SaveChanges();
                return true;
            }
        }

        public Recomendation? Get(int idRecomendation)
        {
          
            if (idRecomendation <= 0) return null;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
          .UseSqlServer(Data.Helpers.Constants.ConnectionString)
          .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                return db.recomendation.FirstOrDefault(r => r.recomendationId == idRecomendation);
            }
        }

        public List<Recomendation> GetRecomentaciones()
        {
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
          .UseSqlServer(Data.Helpers.Constants.ConnectionString)
          .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                return db.recomendation.ToList();
            }
        }

        public bool recomendationCompleted(int idRecomendation, int idUsuario)
        {
            if (idRecomendation <= 0 || idUsuario <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
          .UseSqlServer(Data.Helpers.Constants.ConnectionString)
          .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var recomendation = db.recomendation.Find(idRecomendation);
                if (recomendation == null) return false;
                var usuario = db.patients.FirstOrDefault(u => u.userId == idUsuario);
                if (usuario == null) return false;
                var recomendationComplete1 = new RecomendationComplete
                {
                    recomendationId = idRecomendation,
                    dateCompleted = DateTime.Now

                };

                usuario.completeRecomendations.Add(recomendationComplete1);
                db.patients.Update(usuario);
                db.SaveChanges();

                return true;
            }
        }

        

        public bool Update(Recomendation recomendation)
        {
            if (recomendation == null) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
          .UseSqlServer(Data.Helpers.Constants.ConnectionString)
          .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                db.recomendation.Update(recomendation);
                db.SaveChanges();
                return true;
            }
        }

        public List<Recomendation>? GetCompletedRecomendations(int idUsuario)
        {
            if (idUsuario <= 0) return null;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
          .UseSqlServer(Data.Helpers.Constants.ConnectionString)
          .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var usuario = db.patients.Include(u => u.completeRecomendations).FirstOrDefault(u => u.userId == idUsuario);
                if (usuario == null) return null;
                var completedRecomendations = usuario.completeRecomendations.Select(cr => db.recomendation.FirstOrDefault(r => r.recomendationId == cr.recomendationId)).ToList();
                return completedRecomendations!;
            }
        }
    }
}
