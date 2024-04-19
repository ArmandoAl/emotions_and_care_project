using Data.Contracts;
using Domain;
using FirebaseAdmin.Messaging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Implementations
{
    public class NotificacionRepository : INotificacionRepository
    {   
        public int AddNotificacion(Notificacion notificacion)
        {
            if(notificacion == null) return 0;

            if(notificacion.TipoNotificacion ==TipoNotificacion.NotificacionRecomendacion) {
                //notificacion.FechaEmision = DateTime.Now.AddDays(7);
                notificacion.FechaEmision = DateTime.Now;
            }


            notificacion.FechaCreacion = DateTime.Now;
            notificacion.FechaModificacion = DateTime.Now;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
            .UseSqlServer(Data.Helpers.Constants.ConnectionString)
            .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                db.Notificaciones.Add(notificacion);
                db.SaveChanges();
                return notificacion.IdNotificacion;
            }
        }

        public bool DeleteNotificacion(int idNotificacion)
        {
            if(idNotificacion <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
            .UseSqlServer(Data.Helpers.Constants.ConnectionString)
            .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var notificacion = db.Notificaciones.FirstOrDefault(x => x.IdNotificacion == idNotificacion);
                if(notificacion == null) return false;

                db.Notificaciones.Remove(notificacion);
                db.SaveChanges();
                return true;
            }
        }

        public Notificacion? GetNotificacion(int idNotificacion)
        {
            if(idNotificacion <= 0) return null;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
            .UseSqlServer(Data.Helpers.Constants.ConnectionString)
            .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                return db.Notificaciones.FirstOrDefault(x => x.IdNotificacion == idNotificacion);
            }
        }

        public List<Notificacion>? GetNotificacionesByPaciente(int idPaciente)
        {
            if(idPaciente <= 0) return null;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
            .UseSqlServer(Data.Helpers.Constants.ConnectionString)
            .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                return db.Pacientes.Where(x => x.IdUsuario == idPaciente).SelectMany(x => x.Notificaciones).ToList();
            }
        }

        public bool updateDateEmision(int idNotificacion)
        {
            if(idNotificacion <= 0) return false;
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
            .UseSqlServer(Data.Helpers.Constants.ConnectionString)
            .Options;
            using (var db = new DBContext(options: connectionOptions))
            {

                var notificacion = db.Notificaciones.FirstOrDefault(x => x.IdNotificacion == idNotificacion);
                if(notificacion == null) return false;

                notificacion.FechaEmision = DateTime.Now.AddDays(10);

                db.Notificaciones.Update(notificacion);
                db.SaveChanges();
                return true;
            }

        }

        public bool UpdateNotificacion(Notificacion notificacion)
        {   
            if(notificacion == null) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
            .UseSqlServer(Data.Helpers.Constants.ConnectionString)
            .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                db.Notificaciones.Update(notificacion);
                db.SaveChanges();
                return true;
            }
        }

        public bool vincularNotificacionConPaciente(int idNotificacion, int idPaciente)
        {
            if(idNotificacion <= 0 || idPaciente <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
            .UseSqlServer(Data.Helpers.Constants.ConnectionString)
            .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var notificacion = db.Notificaciones.FirstOrDefault(x => x.IdNotificacion == idNotificacion);
                var paciente = db.Pacientes.FirstOrDefault(x => x.IdUsuario == idPaciente);

                if(notificacion == null || paciente == null) return false;
                
                paciente.Notificaciones.Add(notificacion);
                db.SaveChanges();
                return true;
            }
        }
    }
}
