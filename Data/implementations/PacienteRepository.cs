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
    public class PacienteRepository : IPacienteRepository
    {
        public int Add(AgregarPaciente paciente)
        {
            if (paciente == null) return 0;
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var thisEmailExist = db.Pacientes.FirstOrDefault(x => x.Correo == paciente.Correo);

                if (thisEmailExist == null)
                {
                    var isaEspacialistaEmail = db.Especialistas.FirstOrDefault(x => x.Correo == paciente.Correo);
                    if (isaEspacialistaEmail != null) return -1;


                    var newpaciente = new Paciente
                    {
                        Nombre = paciente.Nombre,
                        Correo = paciente.Correo,
                        Contraseña = paciente.Contraseña,
                        Telefono = paciente.Telefono,
                        Edad = paciente.Edad,
                        Sexo = paciente.Sexo,
                        Token = paciente.Token,
                        TokenRelacional = getTheFirstSixDigits(paciente.Token),
                        Terminosycondiciones = db.TerminosYCondiciones!.FirstOrDefault(x => x.IdTerminosYCondiciones == paciente.TerminosycondicionesId)!,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };

                    db.Pacientes.Add(newpaciente);
                    db.SaveChanges();
                    return newpaciente.IdUsuario;

                } else
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

                var thisTokenExist = db.Pacientes.FirstOrDefault(x => x.TokenRelacional == posibleRelationalToken);
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


        public Paciente? Get(int id)
        {
            if (id <= 0) return null;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                return db.Pacientes.Where(x => x.IdUsuario == id).Include(x => x.Especialista).Include(x => x.Terminosycondiciones).Select
                    (x => new Paciente
                    {
                    IdUsuario = x.IdUsuario,
                    Nombre = x.Nombre,
                    Correo = x.Correo,
                    Contraseña = x.Contraseña,
                    Telefono = x.Telefono,
                    Edad = x.Edad,
                    Sexo = x.Sexo,
                    Token = x.Token,
                    TokenRelacional = x.TokenRelacional,
                    FechaCreacion = x.FechaCreacion,
                    FechaModificacion = x.FechaModificacion,
                    Terminosycondiciones = x.Terminosycondiciones,
                    Especialista = x.Especialista == null ? null : new Especialista
                    {
                        IdUsuario = x.Especialista.IdUsuario,
                        Nombre = x.Especialista.Nombre,
                        Correo = x.Especialista.Correo,
                        Contraseña = x.Especialista.Contraseña,
                        Telefono = x.Especialista.Telefono,
                        Edad = x.Especialista.Edad,
                        Sexo = x.Especialista.Sexo,
                        Token = x.Especialista.Token,
                        CedulaProfesional = x.Especialista.CedulaProfesional,
                        Terminosycondiciones = x.Especialista.Terminosycondiciones,
                        TokenRelacional = x.Especialista.TokenRelacional,
                        FechaCreacion = x.Especialista.FechaCreacion,
                        FechaModificacion = x.Especialista.FechaModificacion
                        
                    }
                })
                   
                    .FirstOrDefault();
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
                var paciente = db.Pacientes.FirstOrDefault(x => x.IdUsuario == id);
                if (paciente == null) return false;

                db.Pacientes.Remove(paciente);
                db.SaveChanges();
                return true;
            }
        }

        public bool Update(Paciente paciente)
        {
            if (paciente == null) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var thisPaciente = db.Pacientes.FirstOrDefault(x => x.IdUsuario == paciente.IdUsuario);
                if (thisPaciente == null) return false;

                thisPaciente.Nombre = paciente.Nombre;
                thisPaciente.Edad = paciente.Edad;
                thisPaciente.Correo = paciente.Correo;
                thisPaciente.Telefono = paciente.Telefono;
                thisPaciente.Contraseña = paciente.Contraseña;
                thisPaciente.FechaModificacion = DateTime.Now;

                db.Pacientes.Update(thisPaciente);
                db.SaveChanges();
                return true;
            }
        }

          public bool VincularEspecialista(int id, string tokenEspecialista)
          {
            if (id <= 0 || string.IsNullOrEmpty(tokenEspecialista)) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var thisPaciente = db.Pacientes.FirstOrDefault(x => x.IdUsuario == id);
                if (thisPaciente == null) return false;

                var thisEspecialista = db.Especialistas.FirstOrDefault(x => x.TokenRelacional == tokenEspecialista);
                if (thisEspecialista == null) return false;

                thisPaciente.Especialista = thisEspecialista;
                thisPaciente.FechaModificacion = DateTime.Now;
                thisEspecialista.FechaModificacion = DateTime.Now;
                thisEspecialista.
                    Pacientes.Add(thisPaciente);

                db.Pacientes.Update(thisPaciente);
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
                var thisPaciente = db.Pacientes.FirstOrDefault(x => x.IdUsuario == idPaciente);
                if (thisPaciente == null) return null;

                return thisPaciente.Token;
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
                var paciente = db.Pacientes.FirstOrDefault(x => x.Correo == email);

                if (paciente != null)
                {
                    if (paciente.Contraseña == password)
                    {
                        return paciente.IdUsuario;
                    }

                    return -1;
                }

                return 0;
            }
        }
    }
}
