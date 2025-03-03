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
                
                db.dates.Add(cita);

                db.SaveChanges();

                return cita.dateId;

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


                var cita = db.dates.FirstOrDefault(x => x.dateId == idCita);



                if (cita == null) return false;

                db.dates.Remove(cita);

                db.SaveChanges();

                return true;
            }
        }

        public Date? GetCita(int idCita)
        {
            if (idCita <= 0) return null;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;
            using (var db = new DBContext(options: connectionOptions))
            {

                return db.dates.FirstOrDefault(x => x.dateId == idCita);

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

                citas = paciente.dates.ToList();

                //ordenar citas por fecha de mas reciente a mas antigua
                citas = citas.OrderByDescending(x => x.date).ToList();

                return citas;
            }
        }

        public bool UpdateCita(Date cita)
        {
            if (cita == null) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;
            using (var db = new DBContext(options: connectionOptions))
            {

                var citaToUpdate = db.dates.FirstOrDefault(x => x.dateId == cita.dateId);


                if (citaToUpdate == null) return false;

                citaToUpdate.date = cita.date;
                citaToUpdate.hour = cita.hour;
                citaToUpdate.place = cita.place;
                citaToUpdate.description = cita.description;


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

                var cita = db.dates.FirstOrDefault(x => x.dateId == idCita);
                if (cita == null) return false;

                var paciente = db.patients.FirstOrDefault(x => x.userId == idPaciente);
                if (paciente == null) return false;

                paciente.dates.Add(cita);

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

                var cita = db.dates.FirstOrDefault(x => x.dateId == idCita);
                if (cita == null) return false;

                var paciente = db.patients.FirstOrDefault(x => x.userId == idPaciente);
                if (paciente == null) return false;

                cita.patientConfirm = true;
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
                var cita = db.dates.FirstOrDefault(x => x.dateId == idCita);
                if (cita == null) return false;

                var paciente = db.patients.FirstOrDefault(x => x.userId == idPaciente);
                if (paciente == null) return false;

                cita.patientConfirm = false;
                if(cita.specialistConfirm == false)
                {
                    db.dates.Remove(cita);
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

                var cita = db.dates.FirstOrDefault(x => x.dateId == idCita);
                if (cita == null) return false;

                var especialista = db.specialists.FirstOrDefault(x => x.userId == idEspecialista);
                if (especialista == null) return false;

                cita.specialistConfirm = true;
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


                var cita = db.dates.FirstOrDefault(x => x.dateId == idCita);
                if (cita == null) return false;

                var especialista = db.specialists.FirstOrDefault(x => x.userId == idEspecialista);
                if (especialista == null) return false;

               
                cita.specialistConfirm = false;
                if (cita.patientConfirm == false)
                {
                    db.dates.Remove(cita);
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

        public bool vincularCitaConEspecialista(int idEspecialista, int idCita)
        {
            if(idCita <= 0 || idEspecialista <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;

            using (var db = new DBContext(options: connectionOptions))
            {
                var especialista = db.specialists.FirstOrDefault(x => x.userId == idEspecialista);
                if (especialista == null) return false;


                var cita = db.dates.FirstOrDefault(x => x.dateId == idCita);
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
                solicitudesCitas = db.specialists.Where(x => x.userId == id).SelectMany(x => x.dateRequests).Include(x => x.Cita)
                    .Select(x => new DateRequest
                    {
                        dateRequestId = x.dateRequestId,
                        Cita = new Date
                        {
                            dateId = x.Cita!.dateId,
                            date = x.Cita.date,
                            hour = x.Cita.hour,
                            place = x.Cita.place,
                            description = x.Cita.description,
                            specialistConfirm = x.Cita.specialistConfirm,
                            patientConfirm = x.Cita.patientConfirm,
                            patient = new Patient
                            {
                                userId = x.Cita!.patient!.userId,
                                name = x.Cita.patient.name,
                                mail = x.Cita.patient.mail,
                                phone = x.Cita.patient.phone,
                                sex = x.Cita.patient.sex,
                                age = x.Cita.patient.age,
                                token = x.Cita.patient.token,
                                termsAndConditions = x.Cita.patient.termsAndConditions,
                               
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
                citas = db.specialists.Where(x => x.userId == idSpecialist).SelectMany(x => x.dates).Include(x => x.patient).Select(
                                       x => new Date
                                      {
                            dateId = x.dateId,
                            date = x.date,
                            hour = x.hour,
                            place = x.place,
                            description = x.description,
                            specialistConfirm = x.specialistConfirm,
                            patientConfirm = x.patientConfirm,
                            patient = new Patient
                            {
                                userId = x.patient!.userId,
                                name = x.patient.name,
                                mail = x.patient.mail,
                                phone = x.patient.phone,
                                sex = x.patient.sex,
                                age = x.patient.age,
                                token = x.patient.token,
                                termsAndConditions = x.patient.termsAndConditions,
                               
                            }

                                       }).ToList();

                return citas;
            }
        }
    }

}