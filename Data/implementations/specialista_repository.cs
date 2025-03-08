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
    public class SpecialistRepository : ISpecialistRepository
    {

        /// <summary>
        /// Adds a new specialist to the database.
        /// </summary>
        /// <param name="specialist">The specialist entity containing the details to be added.</param>
        /// <returns>
        /// The ID of the newly added specialist, or:
        /// <list type="bullet">
        /// <item><description>0 if the input is null.</description></item>
        /// <item><description>-1 if the email already exists or matches a patient's email.</description></item>
        /// <item><description>-2 if the phone number already exists.</description></item>
        /// </list>
        /// </returns>
        public int Add(AddSpecialist specialist)
        {
            if (specialist == null) return 0;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                .Options;

            using (var db = new DBContext(options: connectionOptions))
            {
                // Check if the email already exists for a specialist
                var thisEmailExist = db.specialists.FirstOrDefault(x => x.mail == specialist.mail);

                // Check if the phone number already exists for a specialist
                var thisNumberExist = db.specialists.FirstOrDefault(x => x.phone == specialist.phone);
                if (thisNumberExist != null) return -2;

                if (thisEmailExist == null)
                {
                    // Check if the email matches a patient's email
                    var isAPatientEmail = db.patients.FirstOrDefault(x => x.mail == specialist.mail);
                    if (isAPatientEmail != null) return -1;

                    // Create a new specialist entity
                    var newEspecialista = new Specialist
                    {
                        name = specialist.name,
                        mail = specialist.mail,
                        password = specialist.password,
                        phone = specialist.phone,
                        age = specialist.age,
                        sex = specialist.sex,
                        token = specialist.token,
                        focus = specialist.focus,
                        institution = specialist.institution,
                        presentation = specialist.presentation,
                        adress = specialist.adress,
                        license = specialist.license,        
                        relationalToken = getTheFirstSixDigits(specialist.token),
                        termsAndConditions = db.terms!.FirstOrDefault(x => x.termsAndConditionsId! == specialist.termsId)!,
                        dateCreated = DateTime.Now,
                        modifiedDate = DateTime.Now
                    };

                    // Add the specialist to the database and save changes
                    db.specialists.Add(newEspecialista);
                    db.SaveChanges();

                    return newEspecialista.userId;
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

                var thisTokenExist = db.specialists.FirstOrDefault(x => x.relationalToken == posibleRelationalToken);
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

        public bool refreshToken(int id, string token)
        {
            if (id <= 0 || string.IsNullOrEmpty(token)) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var especialista = db.specialists.FirstOrDefault(x => x.userId == id);
                if (especialista == null) return false;

                especialista.token = token;
                especialista.relationalToken = getTheFirstSixDigits(token);
                especialista.modifiedDate = DateTime.Now;

                db.specialists.Update(especialista);
                db.SaveChanges();
                return true;
            }
        }
    

        public Specialist? Get(int id)
        {
            if (id <= 0) return null;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                return db.specialists.Where(x => x.userId == id).Include(x => x.termsAndConditions).FirstOrDefault();
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
                var especialista = db.specialists.FirstOrDefault(x => x.userId == id);
                if (especialista == null) return false;

                db.specialists.Remove(especialista);
                db.SaveChanges();
                return true;
            }
        }

        public bool Update(Specialist specialist)
        {
            if (specialist == null) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var thisEspecialista = db.specialists.FirstOrDefault(x => x.userId == specialist.userId);
                if (thisEspecialista == null) return false;

                thisEspecialista.name = specialist.name;
                thisEspecialista.age = specialist.age;
                thisEspecialista.mail = specialist.mail;
                thisEspecialista.phone = specialist.phone;
                thisEspecialista.password = specialist.password;
                thisEspecialista.license = specialist.license;
                thisEspecialista.modifiedDate = DateTime.Now;

                db.specialists.Update(thisEspecialista);
                db.SaveChanges();
                return true;
            }
        }

        public Specialist? GetByToken(string relatedToken)
        {
            if (string.IsNullOrEmpty(relatedToken)) return null;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var especialista = db.specialists.FirstOrDefault(x => x.relationalToken == relatedToken);
                if (especialista == null) return null;

                return especialista;
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
                var especialista = db.specialists.FirstOrDefault(x => x.mail == email && x.password == password);
                if (especialista != null)
                {
                    if(especialista.password == password)
                    {
                        return especialista.userId;
                    }
                    else
                    {
                        return -1;
                    }
                }

                return 0;
            }
        }

        public bool aceptarSolicitud(int idSpecialist, int pacientId)
        {
            if(idSpecialist <= 0 || pacientId <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var especialista = db.specialists.FirstOrDefault(x => x.userId == idSpecialist);
                if(especialista == null) return false;

                var paciente = db.patients.FirstOrDefault(x => x.userId == pacientId);
                if(paciente == null) return false;

                especialista.patients.Add(paciente);

                //encontrar el patientRequest que tenga el usuario y eliminalo
                PatientRequest? patientRequest = db.patientRequest.FirstOrDefault(x => x.patient.userId == pacientId);

                if (patientRequest != null)
                {
                    db.patientRequest.Remove(patientRequest);
                }
                especialista.patientsRequests = db.patientRequest.Where(x => x.patient.userId != pacientId).ToList();
                paciente.specialist = especialista;

                db.specialists.Update(especialista);
                db.patients.Update(paciente);
                db.SaveChanges();
                return true;
            }
        }

        public bool rechazarSolicitud(int idSpecialist, int pacientId)
        {
            if (idSpecialist <= 0 || pacientId <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var especialista = db.specialists.FirstOrDefault(x => x.userId == idSpecialist);
                if (especialista == null) return false;

                var paciente = db.patients.FirstOrDefault(x => x.userId == pacientId);
                if (paciente == null) return false;

                //encontrar el patientRequest que tenga el usuario y eliminalo
                PatientRequest? patientRequest = db.patientRequest.FirstOrDefault(x => x.patient.userId == pacientId);

                if (patientRequest != null)
                {
                    db.patientRequest.Remove(patientRequest);
                }
                especialista.patientsRequests = db.patientRequest.Where(x => x.patient.userId != pacientId).ToList();
                db.specialists.Update(especialista);
                db.SaveChanges();
                return true;
            }
        }

        public List<Patient>? GetPacientes(int id)
        {
            if (id <= 0) return null;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var pacientes = db.specialists
                                    .Where(x => x.userId == id)
                                    .SelectMany(x => x.patients)
                                    .Select(p => new Patient
                                    {
                                        userId = p.userId,
                                        name = p.name,
                                        mail = p.mail,
                                        phone = p.phone,
                                        age = p.age,
                                        sex = p.sex,
                                        token = p.token,
                                        relationalToken = p.relationalToken,
                                        syncDate = p.syncDate,
                                        // dates = [
                                          
                                          

                                        // ],

                                        
                                        // Asigna otras propiedades que necesites
                                    })
                                    .ToList();

                return pacientes;
            }
        }

        public List<Specialist> ListarEspecialistas(int offset, int limit)
        {
            if (offset < 0 || limit < 1) return new List<Specialist>();

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                return db.specialists
                    .Include(x => x.termsAndConditions)
                    .Skip(offset)
                    .Take(limit)
                    .ToList();
            }
        }

    }
}

