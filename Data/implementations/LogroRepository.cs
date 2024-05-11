using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.contracts;
using Data.Implementations;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Data.implementations
{
    public class LogroRepository : ILogroRepository
    {
        public int AddLogro(Logro logro)
        {
            if(logro == null) return 0;
            
             var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                db.Logros.Add(logro);
                db.SaveChanges();
                return logro.IdLogro;
            }
        }

        public int AgregarLogroAPaciente(int idPaciente, int id)
        {
            if(idPaciente == 0 || id == 0) return 0;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;

            using (var db = new DBContext(options: connectionOptions))
            {
                var paciente = db.Pacientes.Find(idPaciente);
                var logro = db.Logros.Find(id);

                if(paciente == null || logro == null) return 0;

                paciente.logros.Add(logro);
                db.SaveChanges();
                return logro.IdLogro;
            }
        }

        public bool DeleteLogro(int id)
        {
            if(id == 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var logro = db.Logros.Find(id);
                db.Logros.Remove(logro!);
                db.SaveChanges();
                return true;
            }
        }

        public List<Logro> GetAllLogros()
        {
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                return db.Logros.ToList();
            }
        }

        public Logro GetLogro(int id)
        {
            if(id == 0) return null!;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                return db.Logros.Find(id)!;
            }
        }

        public Logro GetLogro(string nombre)
        {
            if(string.IsNullOrEmpty(nombre)) return null!;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                return db.Logros.Where(l => l.Nombre == nombre).FirstOrDefault()!;
            }
        }

        public bool UpdateLogro(Logro logro)
        {
            if(logro == null) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                db.Logros.Update(logro);
                db.SaveChanges();
                return true;
            }
        }
    }
}