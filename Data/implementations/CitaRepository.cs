using Data.Contracts;
using Data.Implementations;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Implementations
{
    public class CitaRepository : ICitaRepository
    {
        public int AddCita(Cita cita, int idPaciente)
        {
            if (cita == null) return 0;
            if (idPaciente <= 0) return 0;
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                cita.Paciente = db.Pacientes.FirstOrDefault(x => x.IdUsuario == idPaciente);
                db.Citas.Add(cita);
                db.SaveChanges();
                return cita.IdCita;

            }

        }

        public bool DeleteCita(int idCita)
        {
            if (idCita <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var cita = db.Citas.FirstOrDefault(x => x.IdCita == idCita);
                if (cita == null) return false;

                db.Citas.Remove(cita);
                db.SaveChanges();
                return true;
            }
        }

        public Cita? GetCita(int idCita)
        {
            if (idCita <= 0) return null;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                return db.Citas.Where(x => x.IdCita == idCita).Include(x => x.Paciente).Select(
                    x => new Cita
                    {   
                        IdCita = x.IdCita,
                        Fecha = x.Fecha,
                        Hora = x.Hora,
                        Lugar = x.Lugar,
                        Descripcion = x.Descripcion,
                        ConfirmadaPorEspecialista = x.ConfirmadaPorEspecialista,
                        ConfirmadaPorPaciente = x.ConfirmadaPorPaciente,
                        Paciente = new Paciente
                        {
                            IdUsuario = x.Paciente!.IdUsuario,
                            Nombre = x.Paciente.Nombre,
                            Correo = x.Paciente.Correo,
                            Telefono = x.Paciente.Telefono,
                        }

                    }).FirstOrDefault();
            }
        }

        public List<Cita>? GetCitasPorPaciente(int idPaciente)
        {
            if(idPaciente <= 0) return null;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;

            List<Cita> citas = new List<Cita>();

            using (var db = new DBContext(options: connectionOptions))
            {
                citas = db.Pacientes.Where(x => x.IdUsuario == idPaciente).SelectMany(x => x.Citas).Include(x => x.Paciente).Select(
                                       x => new Cita
                                       {
                        IdCita = x.IdCita,
                        Fecha = x.Fecha,
                        Hora = x.Hora,
                        Descripcion = x.Descripcion,
                        Lugar = x.Lugar,
                        ConfirmadaPorEspecialista = x.ConfirmadaPorEspecialista,
                        ConfirmadaPorPaciente = x.ConfirmadaPorPaciente,
                        Paciente = new Paciente
                        {
                            IdUsuario = x.Paciente!.IdUsuario,
                            Nombre = x.Paciente.Nombre,
                            Correo = x.Paciente.Correo,
                            Telefono = x.Paciente.Telefono,
                            Terminosycondiciones = x.Paciente.Terminosycondiciones,
                            Sexo = x.Paciente.Sexo,
                            Edad = x.Paciente.Edad,
                            Token = x.Paciente.Token,
                            TokenRelacional = x.Paciente.TokenRelacional,
                        }

                    }).ToList();

                return citas;
            }
        }

        public bool UpdateCita(Cita cita)
        {
            if (cita == null) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                db.Citas.Update(cita);
                db.SaveChanges();
                return true;
            }
        }

        public bool vincularCitaConPeciente(int idCita, int idPaciente)
        {
            if (idCita <= 0 || idPaciente <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var cita = db.Citas.FirstOrDefault(x => x.IdCita == idCita);
                if (cita == null) return false;

                var paciente = db.Pacientes.FirstOrDefault(x => x.IdUsuario == idPaciente);
                if (paciente == null) return false;

                paciente.Citas.Add(cita);
                db.SaveChanges();
                return true;
            }
        }

        public bool confirmarCitaPorPaciente(int idCita, int idPaciente)
        {
            if (idCita <= 0 || idPaciente <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var cita = db.Citas.FirstOrDefault(x => x.IdCita == idCita);
                if (cita == null) return false;

                var paciente = db.Pacientes.FirstOrDefault(x => x.IdUsuario == idPaciente);
                if (paciente == null) return false;

                cita.ConfirmadaPorPaciente = true;
                db.SaveChanges();
                return true;
            }
        }   

        public bool cancelarCitaPorPaciente(int idCita, int idPaciente)
        {
            if (idCita <= 0 || idPaciente <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var cita = db.Citas.FirstOrDefault(x => x.IdCita == idCita);
                if (cita == null) return false;

                var paciente = db.Pacientes.FirstOrDefault(x => x.IdUsuario == idPaciente);
                if (paciente == null) return false;

                cita.ConfirmadaPorPaciente = false;
                if(cita.ConfirmadaPorEspecialista == false)
                {
                    db.Citas.Remove(cita);
                }
                db.SaveChanges();
                return true;
            }
        }

        public bool confirmarCitaPorEspecialista(int idCita, int idEspecialista)
        {
            if (idCita <= 0 || idEspecialista <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var cita = db.Citas.FirstOrDefault(x => x.IdCita == idCita);
                if (cita == null) return false;

                var especialista = db.Especialistas.FirstOrDefault(x => x.IdUsuario == idEspecialista);
                if (especialista == null) return false;

                cita.ConfirmadaPorEspecialista = true;
                db.SaveChanges();
                return true;
            }
        }

        public bool cancelarCitaPorEspecialista(int idCita, int idEspecialista)
        {
            if (idCita <= 0 || idEspecialista <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var cita = db.Citas.FirstOrDefault(x => x.IdCita == idCita);
                if (cita == null) return false;

                var especialista = db.Especialistas.FirstOrDefault(x => x.IdUsuario == idEspecialista);
                if (especialista == null) return false;

               
                cita.ConfirmadaPorEspecialista = false;
                if (cita.ConfirmadaPorPaciente == false)
                {
                    db.Citas.Remove(cita);
                }
                db.SaveChanges();
                return true;
            }
        }

        public int AddSolicitudCita(Cita cita, int i)
        {
            if(cita == null) return 0;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;

            using (var db = new DBContext(options: connectionOptions))
            {
                var especialista = db.Especialistas.FirstOrDefault(x => x.IdUsuario == i);
                if (especialista == null) return 0;

                SolicitudCita solicitudCita = new SolicitudCita();

                solicitudCita.Cita = cita;

                especialista.SolicitudesCita.Add(solicitudCita);
                db.SaveChanges();
                return solicitudCita.IdSolicitudCita;
            }
                
        }

        public bool vinvularSolicitudCitaConEspecialista(int idEspecialista, int idSolicitudCita)
        {
            if(idSolicitudCita <= 0 || idEspecialista <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;

            using (var db = new DBContext(options: connectionOptions))
            {
                var especialista = db.Especialistas.FirstOrDefault(x => x.IdUsuario == idEspecialista);
                if (especialista == null) return false;

                var solicitudCita = db.SolicitudesCita.FirstOrDefault(x => x.IdSolicitudCita == idSolicitudCita);
                if (solicitudCita == null) return false;

                especialista.SolicitudesCita.Add(solicitudCita);
                db.SaveChanges();
                return true;    
            }
        }

        public SolicitudCita? GetSolicitudCita(int idCita)
        {
            if(idCita <= 0) return null;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;

            using (var db = new DBContext(options: connectionOptions))
            {
                return db.SolicitudesCita.FirstOrDefault(x => x.Cita!.IdCita == idCita);
            }
        }

        public bool vincularCitaConEspecialista(int idEspecialista, int idCita)
        {
            if(idCita <= 0 || idEspecialista <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;

            using (var db = new DBContext(options: connectionOptions))
            {
                var especialista = db.Especialistas.FirstOrDefault(x => x.IdUsuario == idEspecialista);
                if (especialista == null) return false;

                var cita = db.Citas.FirstOrDefault(x => x.IdCita == idCita);
                if (cita == null) return false;

                especialista.Citas.Add(cita);
                db.SaveChanges();
                return true;
            }
        }

        public bool eliminarSolicitudCita(int idCita)
        {
            if(idCita <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;

            using (var db = new DBContext(options: connectionOptions))

            {
                var solicitudCita = db.SolicitudesCita.FirstOrDefault(x => x.IdSolicitudCita == idCita);
                if (solicitudCita == null) return false;

                db.SolicitudesCita.Remove(solicitudCita);
                db.SaveChanges();
                return true;
            }
        }

        public List<SolicitudCita>? GetSolicitudesCitas(int id)
        {
            if(id <= 0) return null;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;

            List<SolicitudCita> solicitudesCitas = new List<SolicitudCita>();

            using (var db = new DBContext(options: connectionOptions))
            {
                solicitudesCitas = db.Especialistas.Where(x => x.IdUsuario == id).SelectMany(x => x.SolicitudesCita).Include(x => x.Cita)
                    .Select(x => new SolicitudCita
                    {
                        IdSolicitudCita = x.IdSolicitudCita,
                        Cita = new Cita
                        {
                            IdCita = x.Cita!.IdCita,
                            Fecha = x.Cita.Fecha,
                            Hora = x.Cita.Hora,
                            Lugar = x.Cita.Lugar,
                            Descripcion = x.Cita.Descripcion,
                            ConfirmadaPorEspecialista = x.Cita.ConfirmadaPorEspecialista,
                            ConfirmadaPorPaciente = x.Cita.ConfirmadaPorPaciente,
                            Paciente = new Paciente
                            {
                                IdUsuario = x.Cita!.Paciente!.IdUsuario,
                                Nombre = x.Cita.Paciente.Nombre,
                                Correo = x.Cita.Paciente.Correo,
                                Telefono = x.Cita.Paciente.Telefono,
                                Sexo = x.Cita.Paciente.Sexo,
                                Edad = x.Cita.Paciente.Edad,
                                Token = x.Cita.Paciente.Token,
                                Terminosycondiciones = x.Cita.Paciente.Terminosycondiciones,
                               
                            }
                        }
                    }).ToList();

                return solicitudesCitas;
            }   
        }

        public List<Cita>? GetCitasPorEspecialista(int idSpecialist)
        {
            if(idSpecialist <= 0) return null;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;

            List<Cita> citas = new List<Cita>();

            using (var db = new DBContext(options: connectionOptions))
            {
                citas = db.Especialistas.Where(x => x.IdUsuario == idSpecialist).SelectMany(x => x.Citas).Include(x => x.Paciente).Select(
                                       x => new Cita
                                       {
                                           IdCita = x.IdCita,
                                           Fecha = x.Fecha,
                                           Hora = x.Hora,
                                           Descripcion = x.Descripcion,
                                           Lugar = x.Lugar,
                                           ConfirmadaPorEspecialista = x.ConfirmadaPorEspecialista,
                                           ConfirmadaPorPaciente = x.ConfirmadaPorPaciente,
                                           Paciente = new Paciente
                                           {
                                               IdUsuario = x.Paciente!.IdUsuario,
                                               Nombre = x.Paciente.Nombre,
                                               Correo = x.Paciente.Correo,
                                               Telefono = x.Paciente.Telefono,
                                               Terminosycondiciones = x.Paciente.Terminosycondiciones,
                                               Sexo = x.Paciente.Sexo,
                                               Edad = x.Paciente.Edad,
                                               Token = x.Paciente.Token,
                                               TokenRelacional = x.Paciente.TokenRelacional,
                                           }

                                       }).ToList();

                return citas;
            }
        }
    }

}