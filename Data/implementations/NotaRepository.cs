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
    public class NotaRepository : INotaRepository
    {
        
        public int AddNota(Nota nota)
        {
            if (nota == null) return 0;
            nota.FechaCreacion = DateTime.Now;
            nota.FechaModificacion = DateTime.Now;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                nota.Emocion = db.Emociones!.FirstOrDefault(x => x.IdEmocion! == nota.Emocion.IdEmocion)!;
                db.Notas.Add(nota);
                db.SaveChanges();
                return nota.IdNota;
            }
        }

        public bool DeleteNota(int idNota)
        {
            if (idNota <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var nota = db.Notas.FirstOrDefault(x => x.IdNota == idNota);
                if (nota == null) return false;

                db.Notas.Remove(nota);
                db.SaveChanges();
                return true;
            }
        }

        public Nota? GetNota(int idNota)
        {
            if (idNota <= 0) return null;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                return db.Notas.FirstOrDefault(x => x.IdNota == idNota);
            }
        }

        public List<Nota>? GetNotasByPaciente(int idPaciente)
        {
            if(idPaciente <= 0) return null;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;


            List<Nota> notas = new List<Nota>();
            using (var db = new DBContext(options: connectionOptions))
            {
                notas = db.Pacientes.Where(x => x.IdUsuario == idPaciente).SelectMany(x => x.Notas).
                Include(x => x.Emocion).ToList();

                //ordenar de mas reciente a mas antigua
                notas = notas.OrderByDescending(x => x.FechaCreacion).ToList();

                return notas;
            }
        }

        public int getTimeWithoutNotes(int idPaciente)
        {
            if (idPaciente <= 0) return 0;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;

            using (var db = new DBContext(options: connectionOptions))
            {
                var notas = db.Pacientes.Where(x => x.IdUsuario == idPaciente).SelectMany(x => x.Notas).ToList();   

                if (notas.Count == 0) return 0;

                var lastNote = notas.OrderByDescending(x => x.FechaCreacion).FirstOrDefault();

                var days = (DateTime.Now - lastNote!.FechaCreacion).Days;

                return days;
            }
        }

        public bool UpdateNota(Nota nota)
        {
            if (nota == null) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var thisNota = db.Notas.FirstOrDefault(x => x.IdNota == nota.IdNota);
                if (thisNota == null) return false;

                thisNota.Titulo = nota.Titulo;
                thisNota.Contenido = nota.Contenido;
                thisNota.FechaModificacion = DateTime.Now;

                db.Notas.Update(thisNota);
                db.SaveChanges();
                return true;
            }
        }

        public bool vincularNotaConEmocion(int idNota, int idEmocion)
        {
            if (idNota <= 0 || idEmocion <= 0) return false;
            
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;

            using (var db = new DBContext(options: connectionOptions))
            {
                var nota = db.Notas.FirstOrDefault(x => x.IdNota == idNota);
                if (nota == null) return false;

                var emocion = db.Emociones.FirstOrDefault(x => x.IdEmocion == idEmocion);
                if (emocion == null) return false;

                nota.Emocion = emocion;
                db.Notas.Update(nota);
                db.SaveChanges();
                return true;
            }

        }

        public bool vincularNotaConPaciente(int idNota, int idPaciente)
        {
            if (idNota <= 0 || idPaciente <= 0) return false;
            
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;

            using (var db = new DBContext(options: connectionOptions))
            {
                var nota = db.Notas.FirstOrDefault(x => x.IdNota == idNota);
                if (nota == null) return false;

                var paciente = db.Pacientes.FirstOrDefault(x => x.IdUsuario == idPaciente);
                if (paciente == null) return false;

                paciente.Notas.Add(nota);
                db.Notas.Update(nota);
                db.SaveChanges();
                return true;
            }
          }
        }
    }
        
        

