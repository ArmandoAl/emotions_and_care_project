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
    public class EspecialistaRepository : IEspecialistaRepository
    {
        public int Add(AgregarEspecialista especialista)
        {
            if (especialista == null) return 0;
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var thisEmailExist = db.Especialistas.FirstOrDefault(x => x.Correo == especialista.Correo);

                var thisNumberExist = db.Especialistas.FirstOrDefault(x => x.Telefono == especialista.Telefono);

                if (thisNumberExist != null) return -2;

                if (thisEmailExist == null)
                {
                    var isAPatientEmail = db.Pacientes.FirstOrDefault(x => x.Correo == especialista.Correo);
                    if (isAPatientEmail != null) return -1;

                    var newEspecialista = new Especialista
                    {
                        Nombre = especialista.Nombre,
                        Correo = especialista.Correo,
                        Contraseña = especialista.Contraseña,
                        Telefono = especialista.Telefono,
                        Edad = especialista.Edad,
                        Sexo = especialista.Sexo,
                        Token = especialista.Token,
                        Enfoque = especialista.Enfoque,
                        Institucion = especialista.Institucion,
                        Presentacion = especialista.Presentacion,
                        Ubicaion = especialista.Ubicaion,
                        CedulaProfesional = especialista.CedulaProfesional,
                        TokenRelacional = getTheFirstSixDigits(especialista.Token),
                        Terminosycondiciones = db.TerminosYCondiciones!.FirstOrDefault(x => x.IdTerminosYCondiciones! == especialista.TerminosycondicionesId)!,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };

                    db.Especialistas.Add(newEspecialista);
                    db.SaveChanges();
                    return newEspecialista.IdUsuario;
                }
                else
                {
                    return -1;
                }
            }
        }

        private string getTheFirstSixDigits(string token)
        {
            //before return the first six digits, we need to verify if that six digits are not already in use
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var posibleRelationalToken = token.Substring(0, 6);

                var thisTokenExist = db.Especialistas.FirstOrDefault(x => x.TokenRelacional == posibleRelationalToken);
                if (thisTokenExist == null)
                {
                    return posibleRelationalToken;
                }
                else
                {
                    return getTheFirstSixDigits(token.Substring(1, token.Length - 1));
                }
            }

        }
    

        public Especialista? Get(int id)
        {
            if (id <= 0) return null;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                return db.Especialistas.Where(x => x.IdUsuario == id).Include(x => x.Terminosycondiciones).FirstOrDefault();
            }
        }

        public bool Delete(int id)
        {
            if (id <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var especialista = db.Especialistas.FirstOrDefault(x => x.IdUsuario == id);
                if (especialista == null) return false;

                db.Especialistas.Remove(especialista);
                db.SaveChanges();
                return true;
            }
        }

        public bool Update(Especialista especialista)
        {
            if (especialista == null) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var thisEspecialista = db.Especialistas.FirstOrDefault(x => x.IdUsuario == especialista.IdUsuario);
                if (thisEspecialista == null) return false;

                thisEspecialista.Nombre = especialista.Nombre;
                thisEspecialista.Edad = especialista.Edad;
                thisEspecialista.Correo = especialista.Correo;
                thisEspecialista.Telefono = especialista.Telefono;
                thisEspecialista.Contraseña = especialista.Contraseña;
                thisEspecialista.CedulaProfesional = especialista.CedulaProfesional;
                thisEspecialista.TokenRelacional = especialista.TokenRelacional;
                thisEspecialista.FechaModificacion = DateTime.Now;

                db.Especialistas.Update(thisEspecialista);
                db.SaveChanges();
                return true;
            }
        }

        public string? GetByToken(int idPaciente)
        {
            if (idPaciente <= 0) return null;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var especialista = db.Especialistas.FirstOrDefault(x => x.IdUsuario == idPaciente);
                if (especialista == null) return null;

                return especialista.Token;
            }
        }

        public int login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password)) return 0;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var especialista = db.Especialistas.FirstOrDefault(x => x.Correo == email && x.Contraseña == password);
                if (especialista != null)
                {
                    if(especialista.Contraseña == password)
                    {
                        return especialista.IdUsuario;
                    }
                    else
                    {
                        return -1;
                    }
                }

                return 0;
            }
        }

        public bool vincularPaciente(int idSpecialist, string tokenPaciente)
        {
            if(idSpecialist <= 0 || string.IsNullOrEmpty(tokenPaciente)) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var especialista = db.Especialistas.FirstOrDefault(x => x.IdUsuario == idSpecialist);
                if(especialista == null) return false;

                var paciente = db.Pacientes.FirstOrDefault(x => x.Token == tokenPaciente);
                if(paciente == null) return false;

                especialista.Pacientes.Add(paciente);
                db.Especialistas.Update(especialista);
                db.SaveChanges();
                return true;
            }
        }

        public List<Paciente>? GetPacientes(int id)
        {
            if (id <= 0) return null;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var pacientes = db.Especialistas
                                    .Where(x => x.IdUsuario == id)
                                    .SelectMany(x => x.Pacientes)
                                    .Select(p => new Paciente
                                    {
                                        IdUsuario = p.IdUsuario,
                                        Nombre = p.Nombre,
                                        Correo = p.Correo,
                                        Telefono = p.Telefono,
                                        Edad = p.Edad,
                                        Sexo = p.Sexo,
                                        Token = p.Token,
                                        TokenRelacional = p.TokenRelacional,
                                        Terminosycondiciones = p.Terminosycondiciones,      
                                        // Asigna otras propiedades que necesites
                                    })
                                    .ToList();

                return pacientes;
            }
        }

        public List<Especialista> ListarEspecialistas(int offset, int limit)
        {
            if (offset < 0 || limit < 1) return new List<Especialista>();

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                return db.Especialistas
                    .Include(x => x.Terminosycondiciones)
                    .Skip(offset)
                    .Take(limit)
                    .ToList();
            }
        }

    }
}

