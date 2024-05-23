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

                var thisNumberExist = db.Pacientes.FirstOrDefault(x => x.Telefono == paciente.Telefono);    

                if (thisNumberExist != null) return -2;

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
                        FechaNacimiento = paciente.FechaNacimiento,
                        Edad = getEdadFromBirthDate(paciente.FechaNacimiento),
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

        
        private int getEdadFromBirthDate(DateTime birthDate)
        {
            DateTime now = DateTime.Now;
            int age = now.Year - birthDate.Year;
            if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day))
            {
                age--;
            }
            return age;
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
                return db.Pacientes.Where(x => x.IdUsuario == id).Include(x => x.Especialista).Include(x => x.logros).
                Include(x => x.Terminosycondiciones).Select
                    (x => new Paciente
                    {
                    IdUsuario = x.IdUsuario,
                    Nombre = x.Nombre,
                    Correo = x.Correo,
                    Contraseña = x.Contraseña,
                    Telefono = x.Telefono,
                    FechaNacimiento = x.FechaNacimiento,
                    Edad = x.Edad,
                    Sexo = x.Sexo,
                    Token = x.Token,
                    TokenRelacional = x.TokenRelacional,
                    logros = x.logros,
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
                        Enfoque = x.Especialista.Enfoque,
                        Institucion = x.Especialista.Institucion,
                        Presentacion = x.Especialista.Presentacion,
                        Ubicaion = x.Especialista.Ubicaion,
                        CedulaProfesional = x.Especialista.CedulaProfesional,
                        Terminosycondiciones = x.Especialista.Terminosycondiciones,
                        TokenRelacional = x.Especialista.TokenRelacional,
                        FechaCreacion = x.Especialista.FechaCreacion,
                        FechaModificacion = x.Especialista.FechaModificacion
                        
                    },
                    registerSet = x.registerSet
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

        public bool MoficarConfiguracionNotificaciones(int id, bool notificacionesActivas, bool dirioActivado, bool progresoActivado)
        {
            if (id <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var thisPaciente = db.Pacientes.FirstOrDefault(x => x.IdUsuario == id);
                if (thisPaciente == null) return false;

                thisPaciente.Configuracion.NotificacionesActivas = notificacionesActivas;
                thisPaciente.Configuracion.DirioActivado = dirioActivado;
                thisPaciente.Configuracion.ProgresoActivado = progresoActivado;
                thisPaciente.FechaModificacion = DateTime.Now;

                db.Pacientes.Update(thisPaciente);
                db.SaveChanges();
                return true;
            }
        }

        public int AgregarFlorInicial(int id)
        {
            if (id <= 0) return 0;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
               

                var thisFlor = db.Flores.FirstOrDefault(x => x.IdFlor == 1);
                if (thisFlor == null) return 0;

              db.FloresDelUsuario.Add(
                    new FloresDelUsuarioModel
                    {
                        Flor = thisFlor,
                        Active = true,
                        Etapa = EtapaFlor.initialFlowet,
                        idUsuario = id
                    }

                );
                db.SaveChanges();
                return  thisFlor.IdFlor;
            }
        }

        public int agregarStickerDeUsuarioModel(int? index, int idUsuario) {
              var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
              List<Sticker> stickers = db.Sticker.ToList();

                StickerDeUsuarioModel stickerDeUsuario; 

              if(index == null) {

               stickerDeUsuario = getStickerFromIndex(stickers, 1, 2, idUsuario);

              } else {
                stickerDeUsuario = stickers.Where(x => x.IdSticker != index && x.IdSticker < 10)
                    .Select(x => new StickerDeUsuarioModel
                    {
                        Sticker = x,
                        Posicion = null
                    }).FirstOrDefault()!;
              }

              db.StickersDeUsuario.Add(
                    stickerDeUsuario
              );
                db.SaveChanges();
                return stickerDeUsuario.IdStickerDeUsuarioModel;
            }
        }


        private StickerDeUsuarioModel getStickerFromIndex(List<Sticker> stickers, int index, int maxIndex, int idUsuario)
        {
          //get a random sticker from the list of stickers and return it
            Random random = new Random();
            int randomIndex = random.Next(index, maxIndex);
            return new StickerDeUsuarioModel
            {
                Sticker = stickers[randomIndex],
                Posicion = null,
                idUsuario = idUsuario
            };       
        }

        public bool registerSet(int id)
        {
            if (id <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var thisPaciente = db.Pacientes.FirstOrDefault(x => x.IdUsuario == id);
                if (thisPaciente == null) return false;

                thisPaciente.registerSet = true;
                thisPaciente.FechaModificacion = DateTime.Now;

                db.Pacientes.Update(thisPaciente);
                db.SaveChanges();
                return true;
            }
        }
    }
}


