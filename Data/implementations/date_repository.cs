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
    public class DateRepository : IDateRepository
    {
        public int AddCita(Date cita, int idPaciente)
        {
            if (cita == null) return 0;
            if (idPaciente <= 0) return 0;
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var pattient = db.patients.FirstOrDefault(x => x.userId == idPaciente);

                if (pattient == null) return 0;

                
                var schedule = pattient.schedule;

                if (schedule == null) return 0;

                schedule.dates.Add(cita);

                db.SaveChanges();

                return cita.dateId;

            }

        }

        public bool DeleteCita(int idCita, int scheduleId)
        {
            if (idCita <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var schedule = db.schedule.FirstOrDefault(x => x.scheduleId == scheduleId);

                if (schedule == null) return false;


                var cita = schedule.dates.FirstOrDefault(x => x.dateId == idCita);



                if (cita == null) return false;

                schedule.dates.Remove(cita);

                db.SaveChanges();

                return true;
            }
        }

        public Date? GetCita(int idCita, int scheduleId)
        {
            if (idCita <= 0) return null;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var schedule = db.schedule.FirstOrDefault(x => x.scheduleId == scheduleId);

                if (schedule == null) return null;

                return schedule.dates.FirstOrDefault(x => x.dateId == idCita);

            }
        }

        public List<Date>? GetCitasPorPaciente(int idPaciente)
        {
            if(idPaciente <= 0) return null;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;

            List<Date> citas = new List<Date>();

            using (var db = new DBContext(options: connectionOptions))
            {
               var paciente = db.patients.FirstOrDefault(x => x.userId == idPaciente);

                if (paciente == null) return null;

                citas = paciente.schedule.dates.ToList();

                //ordenar citas por fecha de mas reciente a mas antigua
                citas = citas.OrderByDescending(x => x.date).ToList();

                return citas;
            }
        }

        public bool UpdateCita(Date cita, int scheduleId)
        {
            if (cita == null) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var schedule = db.schedule.FirstOrDefault(x => x.scheduleId == scheduleId);

                if (schedule == null) return false;

                var citaToUpdate = schedule.dates.FirstOrDefault(x => x.dateId == cita.dateId);


                if (citaToUpdate == null) return false;

                citaToUpdate.date = cita.date;
                citaToUpdate.hour = cita.hour;
                citaToUpdate.place = cita.place;
                citaToUpdate.description = cita.description;

                db.SaveChanges();

                return true;
            }
        }

        public bool vincularCitaConPeciente(int idCita, int idPaciente, int scheduleId)
        {
            if (idCita <= 0 || idPaciente <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;
            using (var db = new DBContext(options: connectionOptions))
            {

                var schedule = db.schedule.FirstOrDefault(x => x.scheduleId == scheduleId);

                if (schedule == null) return false;


                var cita = schedule.dates.FirstOrDefault(x => x.dateId == idCita);
                if (cita == null) return false;

                var paciente = db.patients.FirstOrDefault(x => x.userId == idPaciente);
                if (paciente == null) return false;

                paciente.schedule.dates.Add(cita);

                db.SaveChanges();
                return true;
            }
        }

        public bool confirmarCitaPorPaciente(int idCita, int idPaciente, int scheduleId)
        {
            if (idCita <= 0 || idPaciente <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var schedule = db.schedule.FirstOrDefault(x => x.scheduleId == scheduleId);

                if (schedule == null) return false;


                var cita = schedule.dates.FirstOrDefault(x => x.dateId == idCita);
                if (cita == null) return false;

                var paciente = db.patients.FirstOrDefault(x => x.userId == idPaciente);
                if (paciente == null) return false;

                cita.patientConfirm = true;
                db.SaveChanges();
                return true;
            }
        }   

        public bool cancelarCitaPorPaciente(int idCita, int idPaciente, int scheduleId)
        {
            if (idCita <= 0 || idPaciente <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var schedule = db.schedule.FirstOrDefault(x => x.scheduleId == scheduleId);

                if (schedule == null) return false;

                var cita = schedule.dates.FirstOrDefault(x => x.dateId == idCita);
                if (cita == null) return false;

                var paciente = db.patients.FirstOrDefault(x => x.userId == idPaciente);
                if (paciente == null) return false;

                cita.patientConfirm = false;
                if(cita.specialistConfirm == false)
                {
                    schedule.dates.Remove(cita);
                }
                db.SaveChanges();
                return true;
            }
        }

        public bool confirmarCitaPorEspecialista(int idCita, int idEspecialista, int scheduleId)
        {
            if (idCita <= 0 || idEspecialista <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var schedule = db.schedule.FirstOrDefault(x => x.scheduleId == scheduleId);

                if (schedule == null) return false;

                var cita = schedule.dates.FirstOrDefault(x => x.dateId == idCita);
                if (cita == null) return false;

                var especialista = db.specialists.FirstOrDefault(x => x.userId == idEspecialista);
                if (especialista == null) return false;

                cita.specialistConfirm = true;
                db.SaveChanges();
                return true;
            }
        }

        public bool cancelarCitaPorEspecialista(int idCita, int idEspecialista, int scheduleId)
        {
            if (idCita <= 0 || idEspecialista <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;
            using (var db = new DBContext(options: connectionOptions))
            {

                var schedule = db.schedule.FirstOrDefault(x => x.scheduleId == scheduleId);

                if (schedule == null) return false;

                var cita = schedule.dates.FirstOrDefault(x => x.dateId == idCita);
                if (cita == null) return false;

                var especialista = db.specialists.FirstOrDefault(x => x.userId == idEspecialista);
                if (especialista == null) return false;

               
                cita.specialistConfirm = false;
                if (cita.patientConfirm == false)
                {
                    schedule.dates.Remove(cita);
                }
                db.SaveChanges();
                return true;
            }
        }

        public int AddSolicitudCita(Date cita, int i)
        {
            if(cita == null) return 0;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;

            using (var db = new DBContext(options: connectionOptions))
            {
                var especialista = db.specialists.FirstOrDefault(x => x.userId == i);
                if (especialista == null) return 0;

                DateRequest solicitudCita = new DateRequest();

                solicitudCita.Cita = cita;

                especialista.dateRequests.Add(solicitudCita);
                db.SaveChanges();
                return solicitudCita.dateRequestId;
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
                var especialista = db.specialists.FirstOrDefault(x => x.userId == idEspecialista);
                if (especialista == null) return false;

                var solicitudCita = db.dateRequests.FirstOrDefault(x => x.dateRequestId == idSolicitudCita);
                if (solicitudCita == null) return false;

                especialista.dateRequests.Add(solicitudCita);
                db.SaveChanges();
                return true;    
            }
        }

        public DateRequest? GetSolicitudCita(int idCita)
        {
            if(idCita <= 0) return null;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;

            using (var db = new DBContext(options: connectionOptions))
            {
                return db.dateRequests.FirstOrDefault(x => x.Cita!.dateId == idCita);
            }
        }

        public bool vincularCitaConEspecialista(int idEspecialista, int idCita, int scheduleId)
        {
            if(idCita <= 0 || idEspecialista <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;

            using (var db = new DBContext(options: connectionOptions))
            {
                var especialista = db.specialists.FirstOrDefault(x => x.userId == idEspecialista);
                if (especialista == null) return false;


                var schedule = db.schedule.FirstOrDefault(x => x.scheduleId == scheduleId);

                if (schedule == null) return false;


                var cita = schedule.dates.FirstOrDefault(x => x.dateId == idCita);
                if (cita == null) return false;

                especialista.dates.Add(cita);
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
                var solicitudCita = db.dateRequests.FirstOrDefault(x => x.dateRequestId == idCita);
                if (solicitudCita == null) return false;

                db.dateRequests.Remove(solicitudCita);
                db.SaveChanges();
                return true;
            }
        }

        public List<DateRequest>? GetSolicitudesCitas(int id)
        {
            if(id <= 0) return null;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;

            List<DateRequest> solicitudesCitas = new List<DateRequest>();

            using (var db = new DBContext(options: connectionOptions))
            {
                solicitudesCitas = db.Especialistas.Where(x => x.IdUsuario == id).SelectMany(x => x.SolicitudesCita).Include(x => x.Cita)
                    .Select(x => new DateRequest
                    {
                        dateRequestId = x.IdSolicitudCita,
                        Cita = new Date
                        {
                            dateId = x.Cita!.IdCita,
                            date = x.Cita.Fecha,
                            hour = x.Cita.Hora,
                            place = x.Cita.Lugar,
                            description = x.Cita.Descripcion,
                            specialistConfirm = x.Cita.ConfirmadaPorEspecialista,
                            patient = x.Cita.ConfirmadaPorPaciente,
                            patientConfirm = new Patient
                            {
                                userId = x.Cita!.Paciente!.IdUsuario,
                                name = x.Cita.Paciente.Nombre,
                                mail = x.Cita.Paciente.Correo,
                                phone = x.Cita.Paciente.Telefono,
                                sex = x.Cita.Paciente.Sexo,
                                age = x.Cita.Paciente.Edad,
                                token = x.Cita.Paciente.Token,
                                termsAndConditions = x.Cita.Paciente.Terminosycondiciones,
                               
                            }
                        }
                    }).ToList();

                return solicitudesCitas;
            }   
        }

        public List<Date>? GetCitasPorEspecialista(int idSpecialist)
        {
            if(idSpecialist <= 0) return null;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;

            List<Date> citas = new List<Date>();

            using (var db = new DBContext(options: connectionOptions))
            {
                citas = db.Especialistas.Where(x => x.IdUsuario == idSpecialist).SelectMany(x => x.Citas).Include(x => x.Paciente).Select(
                                       x => new Date
                                      {
                            dateId = x.Cita!.IdCita,
                            date = x.Cita.Fecha,
                            hour = x.Cita.Hora,
                            place = x.Cita.Lugar,
                            description = x.Cita.Descripcion,
                            specialistConfirm = x.Cita.ConfirmadaPorEspecialista,
                            patient = x.Cita.ConfirmadaPorPaciente,
                            patientConfirm = new Patient
                            {
                                userId = x.Cita!.Paciente!.IdUsuario,
                                name = x.Cita.Paciente.Nombre,
                                mail = x.Cita.Paciente.Correo,
                                phone = x.Cita.Paciente.Telefono,
                                sex = x.Cita.Paciente.Sexo,
                                age = x.Cita.Paciente.Edad,
                                token = x.Cita.Paciente.Token,
                                termsAndConditions = x.Cita.Paciente.Terminosycondiciones,
                               
                            }

                                       }).ToList();

                return citas;
            }
        }
    }

}