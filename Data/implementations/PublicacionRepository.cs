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
    public class PublicacionRepository : IPublicacionRepository
    {
        public int Add(Publicacion publicacion)
        {
            if (publicacion == null) return 0;
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
          .UseSqlServer(Data.Helpers.Constants.ConnectionString)
          .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                db.Publicaciones.Add(publicacion);
                db.SaveChanges();
                return publicacion.IdPublicacion;
            }

        }

        public bool AddComent(int idPublicacion, Comentario comentario)
        { 
            if (comentario == null) return false;
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
          .UseSqlServer(Data.Helpers.Constants.ConnectionString)
          .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var publicacion = db.Publicaciones.Find(idPublicacion);
                if (publicacion == null) return false;
                publicacion.Comentarios.Add(comentario);
                db.SaveChanges();
                return true;
            }
        }

        public bool AddLike(int idPublicacion)
        {
            if(idPublicacion == 0) return false;
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
          .UseSqlServer(Data.Helpers.Constants.ConnectionString)
          .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var publicacion = db.Publicaciones.Find(idPublicacion);
                if (publicacion == null) return false;
               
                publicacion.Likes.CompareTo(publicacion.Likes + 1);
                db.SaveChanges();
                return true;
            }
        }

        public bool Delete(int idPublicacion)
        {    
            if (idPublicacion == 0) return false;
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
          .UseSqlServer(Data.Helpers.Constants.ConnectionString)
          .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var publicacion = db.Publicaciones.Find(idPublicacion);
                if (publicacion == null) return false;
                db.Publicaciones.Remove(publicacion);
                db.SaveChanges();
                return true;
            }
        }

        public Publicacion? Get(int idPublicacion)
        {
            if (idPublicacion == 0) return null;
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
          .UseSqlServer(Data.Helpers.Constants.ConnectionString)
          .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                return db.Publicaciones.Find(idPublicacion);
            }
        }

        public List<Publicacion> GetPublicaciones()
        {
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
          .UseSqlServer(Data.Helpers.Constants.ConnectionString)
          .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                return db.Publicaciones.ToList();
            }   
        }

        public bool RemoveComent(int idPublicacion, int idComentario)
        {
            if (idPublicacion == 0 || idComentario == 0) return false;
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
          .UseSqlServer(Data.Helpers.Constants.ConnectionString)
          .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var publicacion = db.Publicaciones.Find(idPublicacion);
                if (publicacion == null) return false;
                var comentario = publicacion.Comentarios.Find(c => c.IdComentario == idComentario);
                if (comentario == null) return false;
                publicacion.Comentarios.Remove(comentario);
                db.SaveChanges();
                return true;
            }
        }

        public bool RemoveLike(int idPublicacion)
        {
            if (idPublicacion == 0) return false;
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
          .UseSqlServer(Data.Helpers.Constants.ConnectionString)
          .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var publicacion = db.Publicaciones.Find(idPublicacion);
                if (publicacion == null) return false;

                publicacion.Likes.CompareTo(publicacion.Likes - 1);
                db.SaveChanges();
                return true;
            }
        }

        public bool Update(Publicacion publicacion)
        {
            if (publicacion == null) return false;
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
          .UseSqlServer(Data.Helpers.Constants.ConnectionString)
          .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                db.Publicaciones.Update(publicacion);
                db.SaveChanges();
                return true;
            }
        }
    }
}
