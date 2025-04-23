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
    public class PacienteRepository : IPatientRepository
{
    // Adds a new patient to the database.
    public int Add(AddPatient patient)
    {
        if (patient == null) return 0;

        var connectionOptions = new DbContextOptionsBuilder<DBContext>()
            .UseSqlServer(Data.Helpers.Constants.ConnectionString)
            .Options;

        using (var db = new DBContext(options: connectionOptions))
        {
            // Check if the email already exists for another patient
            var thisEmailExist = db.patients.FirstOrDefault(x => x.mail == patient.mail);

            // Check if the phone number already exists for another patient
            var thisNumberExist = db.patients.FirstOrDefault(x => x.phone == patient.phone);    

            // If the phone number exists, return error code -2
            if (thisNumberExist != null) return -2;

            // If the email doesn't exist
            if (thisEmailExist == null)
            {
                // Check if the email is associated with a specialist
                var isaEspacialistaEmail = db.specialists.FirstOrDefault(x => x.mail == patient.mail);
                if (isaEspacialistaEmail != null) return -1;

                // Create a new patient entity
                var newpaciente = new Patient
                {
                    name = patient.name,
                    mail = patient.mail,
                    password = patient.password,
                    phone = patient.phone,
                    bornDate = patient.bornDate,
                    age = getEdadFromBirthDate(patient.bornDate), // Get age from birth date
                    sex = patient.sex,
                    token = patient.token,
                    relationalToken = getTheFirstSixDigits(patient.token), // Generate relational token
                    termsAndConditions = db.terms!.FirstOrDefault(x => x.termsAndConditionsId == patient.termsiD)!,
                };

                // Add the new patient to the database and save changes
                db.patients.Add(newpaciente);
                db.SaveChanges();
                return newpaciente.userId;
            }
            else
            {
                // If the email already exists, return error code -1
                return -1;
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
            var thisPaciente = db.patients.FirstOrDefault(x => x.userId == id);
            if (thisPaciente == null) return false;

            thisPaciente.token = token;
            thisPaciente.relationalToken = getTheFirstSixDigits(token);
            thisPaciente.modifiedDate = DateTime.Now;

            db.patients.Update(thisPaciente);
            db.SaveChanges();
            return true;
        }
    }

    // Method to calculate the patient's age based on their birth date
    private int getEdadFromBirthDate(DateTime birthDate)
    {
        DateTime now = DateTime.Now;
        int age = now.Year - birthDate.Year;

        // Adjust age if the birthday hasn't occurred yet this year
        if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day))
        {
            age--;
        }

        return age;
    }

    // Method to generate the first six digits of the relational token
    // It recursively checks to make sure the token isn't already in use
    private string getTheFirstSixDigits(string token)
    {
        var connectionOptions = new DbContextOptionsBuilder<DBContext>()
            .UseSqlServer(Data.Helpers.Constants.ConnectionString)
            .Options;
        using (var db = new DBContext(options: connectionOptions))
        {
            var posibleRelationalToken = token.Substring(0, 6);

            // Check if the first six digits of the token already exist in the database
            var thisTokenExist = db.patients.FirstOrDefault(x => x.relationalToken == posibleRelationalToken);
            if (thisTokenExist == null)
            {
                // If it doesn't exist, return the six digits
                return posibleRelationalToken;
            }
            else
            {
                // If it exists, recursively call the method with a shifted substring
                return getTheFirstSixDigits(token.Substring(1, token.Length - 1));
            }
        }
    }

    // Retrieves a patient by their user ID from the database, including related data such as goals, user interface, and specialist.
    public Patient? Get(int id)
    {
        if (id <= 0) return null;

        var connectionOptions = new DbContextOptionsBuilder<DBContext>()
            .UseSqlServer(Data.Helpers.Constants.ConnectionString)
            .Options;
        using (var db = new DBContext(options: connectionOptions))
        {
            // Retrieve additional information from related tables like stickers, flowers, and goals
            var stickers = db.stickers.ToList();
            var flowers = db.flowers.ToList();

            // Return the patient with all their related information
            return db.patients.Where(x => x.userId == id)
                .Include(x => x.specialist)
                .Include(x => x.goals)
                .Include(x => x.userInterface)
                .Include(x => x.userInterface.userFlowers)
                .Include(x => x.userInterface.userStickers)
                .Include(x => x.settings)
                .Include(x => x.termsAndConditions)
                .Select(x => new Patient
                {
                    userId = x.userId,
                    name = x.name,
                    mail = x.mail,
                    password = x.password,
                    phone = x.phone,
                    bornDate = x.bornDate,
                    age = x.age,
                    sex = x.sex,
                    token = x.token,
                    relationalToken = x.relationalToken,
                    goals = x.goals.Select(y => new Goal
                    {
                        goalId = y.goalId,
                        name = y.name,
                        desription = y.desription,
                        type = y.type,
                        stickerId = y.stickerId,
                        flowerId = y.flowerId,
                    }).ToList(),
                    dateCreated = x.dateCreated,
                    modifiedDate = x.modifiedDate,
                    termsAndConditions = x.termsAndConditions,
                    settings = x.settings,
                    progress = new Progress
                    { 
                        progressId = x.progress!.progressId,
                        stage = x.progress.stage,
                        lastDate = x.progress.lastDate,
                        begginDate = x.progress.begginDate,
                       
                    },
                    userInterface = new UserInterface
                    {
                        userInterfaceId = x.userInterface.userInterfaceId,
                        userFlowers = x.userInterface.userFlowers.Select(y => new UserFlower {
                            userFlowerId = y.userFlowerId,
                            flower = new Flower
                            {
                                flowerId = y.flower.flowerId,
                                name = y.flower.name,
                                images = y.flower.images,
                            },
                            position = y.position,
                            state = y.state
                        }).ToList(),
                        userStickers = x.userInterface.userStickers.Select(y => new UserSticker
                        {
                            userStickerId = y.userStickerId,
                            sticker =  new Sticker
                            {
                                stickerId = y.sticker.stickerId,
                                url = y.sticker.url,
                            },
                            position = y.position
                        }).ToList(),
                        backgroundUrl = x.userInterface.backgroundUrl,
                        themeId = x.userInterface.themeId
                    },
                    specialist = x.specialist == null ? null : new Specialist
                    {
                        userId = x.specialist.userId,
                        name = x.specialist.name,
                        mail = x.specialist.mail,
                        password = x.specialist.password,
                        phone = x.specialist.phone,
                        age = x.specialist.age,
                        sex = x.specialist.sex,
                        token = x.specialist.token,
                        focus = x.specialist.focus,
                        institution = x.specialist.institution,
                        presentation = x.specialist.presentation,
                        adress = x.specialist.adress,
                        license = x.specialist.license,
                        relationalToken = x.specialist.relationalToken,
                        dateCreated = x.specialist.dateCreated,
                        modifiedDate = x.specialist.modifiedDate
                    },
                    registerState = x.registerState
                })
                .FirstOrDefault();
        }
    }

/*

public bool Delete(int id)
        {
            if(id <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
            .UseSqlServer(Data.Helpers.Constants.ConnectionString)
            .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var opps = db.opps
                    .Include(o => o.bukayoSakaDiary)   // Include the related Bukayo
                    .ThenInclude(b => b.SakaNotes)     // Include related SakaNotes
                    .FirstOrDefault(o => o.BuserId == id);

                
                if(opps == null) return false;

                // Delete related SakaNotes
                db.sakaNotes.RemoveRange(opps.bukayoSakaDiary.SakaNotes);
                db.bukayos.Remove(opps.bukayoSakaDiary);
                

                db.opps.Remove(opps);
                db.SaveChanges();
                return true;
            }
        }

*/
    public bool Delete(int id)
    {
        var allowedIds = new List<int> { 29, 49, 60, 61, 62, 63, 64, 67, 76, 77, 78, 79, 80, 83, 85, 86, 87, 88, 89, 90, 91, 92, 93, 94, 95 };
        var connectionOptions = new DbContextOptionsBuilder<DBContext>()
        .UseSqlServer(Data.Helpers.Constants.ConnectionString)
        .Options;

        using (var db = new DBContext(options: connectionOptions))
        {
            var diariesToDelete = db.diaries
    .Where(d => !allowedIds.Contains(d.diaryId))
    .ToList();

db.diaries.RemoveRange(diariesToDelete);



            Console.WriteLine("Notas encontradas:" + diariesToDelete.Count);

             return true;
        }
    }
    // Deletes a patient from the database based on their user ID
    /*
        public bool Delete(int id)
        {
            if (id <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var patient = db.patients
                    .Include(o => o.specialist)
                    .Include(o => o.goals)
                    .Include(o => o.carts).ThenInclude(o => o.cartAnswers)
                    .Include(o => o.userInterface)
                    .Include(o => o.notifications)
                    .Include(o => o.userInterface.userFlowers)
                    .Include(o => o.userInterface.userStickers)
                    .Include(o => o.settings)
                    .Include(o => o.termsAndConditions)
                    .Include(o => o.progress)
                    .Include(o => o.diary)   // Include the related Bukayo
                    .ThenInclude(b => b.notes)     // Include related SakaNotes
                    .FirstOrDefault(o => o.userId == id);
                
                if (patient == null) return false;

                Console.WriteLine("Paciente encontrado:" + patient.userId + " - " + patient.name);

                 //DELETE DIARY
                db.notes.RemoveRange(patient.diary.notes);
                db.diaries.Remove(patient.diary);

                //DELETE CARTS AND ANSWERS
                db.cartAnswers.RemoveRange(patient.carts.SelectMany(x => x.cartAnswers));
                db.carts.RemoveRange(patient.carts);





                
                //DELETE NOTIFICATIONS
                db.notifications.RemoveRange(patient.notifications);

                //DELETE CARTS
                db.carts.RemoveRange(patient.carts);

                if (patient.specialist != null)
                {

                    var requests = db.patientRequests.Where(x => x.patient.userId == id).ToList();
                    if (requests != null)
                    {
                        db.patientRequests.RemoveRange(requests);
                    }
                    patient.specialist.patients.Remove(patient);
                }

                //GOALS
                db.goals.RemoveRange(patient.goals);
                Console.WriteLine("Metas eliminadas");

                db.userInterfaces.Remove(patient.userInterface);
                Console.WriteLine("Interfaz eliminada");

                db.settings.Remove(patient.settings);
                Console.WriteLine("Configuraciones eliminadas");

                db.terms.Remove(patient.termsAndConditions);
                Console.WriteLine("Terminos eliminados");

                db.progresses.Remove(patient.progress);
                Console.WriteLine("Progreso eliminado");




                db.SaveChanges();
                return true;
            }
        }

        */




        public int Update(Patient patient)
        {
            if (patient == null) return 0;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var thisPaciente = db.patients.FirstOrDefault(x => x.userId == patient.userId);
                if (thisPaciente == null) return 0;


                var thisEmailExist = db.patients.FirstOrDefault(x => x.mail == patient.mail && x.userId != patient.userId);

                if (thisEmailExist != null) {
                    return -1;
                } else {
                    var spacialistEmail = db.specialists.Any(x => x.mail == patient.mail);

                    if (spacialistEmail) {
                        return -1;
                    }
                }

                var phoneExist = db.patients.FirstOrDefault(x => x.phone == patient.phone && x.userId != patient.userId);

                if (phoneExist != null) {
                    return -2;
                } else {

                    var spacialistPhone = db.specialists.Any(x => x.phone == patient.phone);

                    if (spacialistPhone) {
                        return -2;
                    }
                }

                thisPaciente.name = patient.name;
                thisPaciente.age = patient.age;
                thisPaciente.mail = patient.mail;
                thisPaciente.phone = patient.phone;
                thisPaciente.password = patient.password;
                thisPaciente.modifiedDate = DateTime.Now;

                db.patients.Update(thisPaciente);
                db.SaveChanges();
                return 1;
            }
        }

        public bool VincularEspecialista(int id, string tokenEspecialista)
        {
            // Return false if the provided id is invalid or the token is empty/null
            if (id <= 0 || string.IsNullOrEmpty(tokenEspecialista)) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                .Options;

            using (var db = new DBContext(options: connectionOptions))
            {
                // Find the patient with the provided id
                var thisPaciente = db.patients.FirstOrDefault(x => x.userId == id);
                
                // If the patient doesn't exist, return false
                if (thisPaciente == null) return false;

                // Find the specialist with the given relational token
                var thisEspecialista = db.specialists.Where(x => x.relationalToken == tokenEspecialista)
                    .Include(x => x.patientsRequests)
                    .ThenInclude(x => x.patient)
                    .FirstOrDefault();
                
                // If the specialist doesn't exist, return false
                if (thisEspecialista == null) return false;

                // Link the patient to the specialist by setting the specialist on the patient's record
              

                // Update the modified date for both the patient and the specialist
                thisPaciente.modifiedDate = DateTime.Now;
                thisEspecialista.modifiedDate = DateTime.Now;

                // Add the patient to the specialist's patient list

                //request exists
                var request = thisEspecialista.patientsRequests.FirstOrDefault(x => x.patient.userId == id);

                if (request != null)
                {
                    return true;
                }

                PatientRequest patientRequest = new PatientRequest
                {
                    patient = thisPaciente,
                    
                };

                thisEspecialista.patientsRequests.Add(patientRequest);

                // Update both the patient and the specialist in the database
                db.patients.Update(thisPaciente);
                db.specialists.Update(thisEspecialista);

                // Save changes to the database
                db.SaveChanges();

                // Return true to indicate the operation was successful
                return true;
            }
        }

        public bool VincularDirecto(int id, string tokenEspecialista)
        {
            // Return false if the provided id is invalid or the token is empty/null
            if (id <= 0 || string.IsNullOrEmpty(tokenEspecialista)) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                .Options;

            using (var db = new DBContext(options: connectionOptions))
            {
                // Find the patient with the provided id
                var thisPaciente = db.patients.FirstOrDefault(x => x.userId == id);

                // If the patient doesn't exist, return false
                if (thisPaciente == null) return false;

                // Find the specialist with the given relational token
                var thisEspecialista = db.specialists.FirstOrDefault(x => x.relationalToken == tokenEspecialista);

                // If the specialist doesn't exist, return false
                if (thisEspecialista == null) return false;

                // Link the patient to the specialist by setting the specialist on the patient's record
                thisPaciente.specialist = thisEspecialista;

                // Update the modified date for both the patient and the specialist
                thisPaciente.modifiedDate = DateTime.Now;
                thisEspecialista.modifiedDate = DateTime.Now;

                // Add the patient to the specialist's patient list
                thisEspecialista.patients.Add(thisPaciente);

                // Update both the patient and the specialist in the database
                db.patients.Update(thisPaciente);
                db.specialists.Update(thisEspecialista);

                // Save changes to the database
                db.SaveChanges();

                // Return true to indicate the operation was successful
                return true;
            }
        }


        /// <summary>
        /// Retrieves the token of a patient based on the provided patient ID.
        /// </summary>
        /// <param name="idPaciente">The ID of the patient.</param>
        /// <returns>The token of the patient if found, otherwise null.</returns>
        public string? GetByToken(int idPaciente)
        {
            // Return null if the provided id is invalid (<= 0)
            if (idPaciente <= 0) return null;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                .Options;

            using (var db = new DBContext(options: connectionOptions))
            {
                // Find the patient with the provided userId (idPaciente)
                var thisPaciente = db.patients.FirstOrDefault(x => x.userId == idPaciente);

                // If the patient doesn't exist, return null
                if (thisPaciente == null) return null;

                // Return the token associated with the patient
                return thisPaciente.token;
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
                var paciente = db.patients.FirstOrDefault(x => x.mail == email);

                if (paciente != null)
                {
                    if (paciente.password == password)
                    {
                        return paciente.userId;
                    }

                    return -1;
                }

                return 0;
            }
        }

        /// <summary>
        /// Modifies the notification settings for a patient based on the provided settings.
        /// </summary>
        /// <param name="id">The ID of the patient whose settings are to be modified.</param>
        /// <param name="notificacionesActivas">Indicates if notifications should be active.</param>
        /// <param name="dirioActivado">Indicates if the diary is activated.</param>
        /// <param name="progresoActivado">Indicates if the progress questionnaire is activated.</param>
        /// <returns>True if the settings were successfully updated, otherwise false.</returns>
        public bool MoficarConfiguracionNotificaciones(int id, bool notificacionesActivas, bool dirioActivado, bool progresoActivado)
        {
            // Return false if the provided id is invalid (<= 0)
            if (id <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                .Options;

            using (var db = new DBContext(options: connectionOptions))
            {
                // Find the patient with the provided userId (id)
                var patients = db.patients;

                var thisPaciente = patients
                    .Where(x => x.userId == id).Include(x => x.settings)
                    .FirstOrDefault();

                // If the patient doesn't exist, return false
                if (thisPaciente == null) return false;

                // Update the patient's notification settings
                thisPaciente.settings.notificationsActive = notificacionesActivas;
                thisPaciente.settings.diaryActive = dirioActivado;
                thisPaciente.settings.questionnaireActive = progresoActivado;

                // Set the modified date to the current time
                thisPaciente.modifiedDate = DateTime.Now;

                // Update the patient in the database
                db.patients.Update(thisPaciente);

                // Save the changes to the database
                db.SaveChanges();

                // Return true to indicate that the settings were successfully updated
                return true;
            }
        }


        /// <summary>
        /// Adds an initial flower to a patient's user interface at a specific position.
        /// </summary>
        /// <param name="id">The ID of the patient to associate the flower with.</param>
        /// <param name="patientId">The ID of the patient to whom the flower is added.</param>
        /// <returns>The flower ID if the operation is successful, otherwise 0 if any validation fails.</returns>
        public int AgregarFlorInicial(int id, int patientId)
        {
            // Return 0 if the provided id is invalid (<= 0)
            if (id <= 0) return 0;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                .Options;

            using (var db = new DBContext(options: connectionOptions))
            {
                // Find the patient with the provided patientId
                var thisPaciente = db.patients.FirstOrDefault(x => x.userId == patientId);

                // If the patient doesn't exist, return 0
                if (thisPaciente == null) return 0;

                // Find the flower with flowerId == 1 (the initial flower)
                var thisFlor = db.flowers.FirstOrDefault(x => x.flowerId == 1);
                
                // If the flower doesn't exist, return 0
                if (thisFlor == null) return 0;

                // Add the initial flower to the patient's user interface at position 2
                thisPaciente.userInterface.userFlowers.Add(
                    new UserFlower
                    {
                        flower = thisFlor,
                        position = 2,
                        state = 0
                    }
                );

                // Save the changes to the database
                db.SaveChanges();

                // Return the flowerId of the added flower
                return thisFlor.flowerId;
            }
        }

        /// <summary>
        /// Adds a user sticker to a patient's user interface based on a given index or default behavior.
        /// </summary>
        /// <param name="index">The index of the sticker to be added (nullable). If null, the default sticker is added.</param>
        /// <param name="idUsuario">The ID of the user to which the sticker will be added.</param>
        /// <returns>The sticker ID if the operation is successful, otherwise 0 if the user is not found or an issue occurs.</returns>
        public int agregarStickerDeUsuarioModel(int? index, int idUsuario)
        {
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                .Options;

            using (var db = new DBContext(options: connectionOptions))
            {
                // Find the patient with the provided userId (idUsuario)
                var patient = db.patients.FirstOrDefault(x => x.userId == idUsuario);

                // If the patient doesn't exist, return 0
                if (patient == null) return 0;

                // Retrieve the list of stickers available in the database
                List<Sticker> stickers = db.stickers.ToList();

                UserSticker stickerDeUsuario;

                // If no index is provided, get the default sticker (based on your business logic)
                if (index == null)
                {
                    stickerDeUsuario = getStickerFromIndex(stickers, 1, 2, idUsuario); // You should implement this method based on your needs
                }
                else
                {
                    // Otherwise, find the sticker that is not the one with the provided index and has a stickerId less than 10
                    stickerDeUsuario = stickers
                        .Where(x => x.stickerId != index && x.stickerId < 10)
                        .Select(x => new UserSticker
                        {
                            sticker = x,
                            position = null // You might want to assign a value to position depending on the context
                        })
                        .FirstOrDefault()!;
                }

                // Add the chosen sticker to the patient's user interface
                patient.userInterface.userStickers.Add(stickerDeUsuario);

                // Save the changes to the database
                db.SaveChanges();

                // Return the stickerId of the added sticker
                return stickerDeUsuario.sticker.stickerId;
            }
        }



        /// <summary>
        /// Gets a random sticker from the list of stickers, within a specified index range, and creates a UserSticker.
        /// </summary>
        /// <param name="stickers">The list of available stickers to choose from.</param>
        /// <param name="index">The starting index (inclusive) from which the random selection should begin.</param>
        /// <param name="maxIndex">The maximum index (exclusive) that the random selection can go up to.</param>
        /// <param name="idUsuario">The ID of the user (not used in this function, but might be for logging or future extensions).</param>
        /// <returns>A UserSticker object containing the randomly selected sticker.</returns>
        private UserSticker getStickerFromIndex(List<Sticker> stickers, int index, int maxIndex, int idUsuario)
        {
            // Initialize the Random object to generate random numbers
            Random random = new Random();

            // Generate a random index between 'index' (inclusive) and 'maxIndex' (exclusive)
            int randomIndex = random.Next(index, maxIndex);

            // Return a new UserSticker with the randomly selected sticker from the list and no position set
            return new UserSticker
            {
                sticker = stickers[randomIndex], // Select a sticker based on the random index
                position = null, // Position is set to null (you may want to assign a meaningful value)
            };
        }


        /// <summary>
        /// Updates the registration state of a patient in the database based on the provided patient ID and new state.
        /// </summary>
        /// <param name="patientId">The unique ID of the patient whose registration state is being updated.</param>
        /// <param name="state">The new state to set for the patient's registration.</param>
        /// <returns>A string indicating the result of the operation: "success" if the update is successful, or "error" if an issue occurs.</returns>
        public string registerSet(int patientId, string state)
        {
            // Validate the patient ID to ensure it's greater than 0
            if (patientId <= 0) return "error";

            // Set up the connection to the database using the DbContext
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
            .UseSqlServer(Data.Helpers.Constants.ConnectionString)
            .Options;

            // Open the database context and perform the update inside a 'using' block to ensure resources are properly disposed of
            using (var db = new DBContext(options: connectionOptions))
            {
                // Retrieve the patient from the database using the provided patient ID
                var thisPaciente = db.patients.FirstOrDefault(x => x.userId == patientId);

                // If the patient does not exist, return an error
                if (thisPaciente == null) return "error";

                // Update the patient's registration state and set the modified date to the current date and time
                thisPaciente.registerState = state;
                thisPaciente.modifiedDate = DateTime.Now;

                // Update the patient's record in the database
                db.patients.Update(thisPaciente);

                // Save the changes to the database
                db.SaveChanges();

                // Return a success message
                return "success";
            }
        }




        /// <summary>
        /// Updates the position of a sticker for a specific patient in their user interface.
        /// If the position is valid (0 to 4), the sticker's position is updated; otherwise, it is removed from its position.
        /// If another sticker is already occupying the same position, its position is reset to null.
        /// </summary>
        /// <param name="idPatient">The unique ID of the patient whose user interface is being modified.</param>
        /// <param name="idUserSticker">The unique ID of the sticker that is being positioned.</param>
        /// <param name="position">The new position of the sticker in the user's interface (0 to 4). If invalid, the sticker's position is cleared.</param>
        /// <returns>The unique ID of the updated user sticker if successful; otherwise, 0 if an error occurs.</returns>
        public int putStickeriInInterface(int idPatient, int idUserSticker, int position)
        {
            // Validate the patient ID and user sticker ID to ensure they are greater than 0
            if (idPatient <= 0 || idUserSticker <= 0) return 0;

            // Set up the connection to the database using the DbContext
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                .Options;

            // Open the database context and perform the update inside a 'using' block to ensure resources are properly disposed of
            using (var db = new DBContext(options: connectionOptions))
            {
                // Find the patient and their user interface, including the list of stickers
                var thisPaciente = db.patients
                    .Where(x => x.userId == idPatient)
                    .Include(x => x.userInterface)
                    .ThenInclude(x => x.userStickers)
                    .ThenInclude(x => x.sticker)
                    .FirstOrDefault();

                // If the patient is not found, return 0 indicating failure
                if (thisPaciente == null) return 0;

                // Find the sticker to be updated
                var userSticker = thisPaciente.userInterface.userStickers
                    .FirstOrDefault(x => x.sticker.stickerId == idUserSticker);

                // If the sticker is not found, return 0 indicating failure
                if (userSticker == null) return 0;

                // Validate the position; if it's invalid (less than 0 or greater than 4), set the sticker's position to null
                if (position < 1 || position > 4)
                {
                    userSticker.position = null;
                }
                else
                {
                    // Check if any other sticker already occupies the specified position
                    var stickerWithSamePosition = thisPaciente.userInterface.userStickers
                        .FirstOrDefault(x => x.position == position);

                    // If a sticker occupies the position, reset its position to null
                    if (stickerWithSamePosition != null)
                    {
                        stickerWithSamePosition.position = null;
                    }

                    // Set the position of the current sticker to the new position
                    userSticker.position = position;
                }

                // Save the changes to the database
                db.SaveChanges();

                // Return the ID of the updated user sticker
                return userSticker.userStickerId;
            }
        }

        public bool removeStickerInInterface(
            int idPatient,
            int position
        )
        {
            // Validate the patient ID to ensure it's greater than 0
            if (idPatient <= 0) return false;

            // Set up the connection to the database using the DbContext
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                .Options;

            // Open the database context and perform the update inside a 'using' block to ensure resources are properly disposed of
            using (var db = new DBContext(options: connectionOptions))
            {
                // Find the patient and their user interface, including the list of stickers
                var thisPaciente = db.patients
                    .Where(x => x.userId == idPatient)
                    .Include(x => x.userInterface)
                    .ThenInclude(x => x.userStickers)
                    .ThenInclude(x => x.sticker)
                    .FirstOrDefault();

                // If the patient is not found, return false indicating failure
                if (thisPaciente == null) return false;

                // Find the sticker to be removed based on its position
                var userSticker = thisPaciente.userInterface.userStickers
                    .FirstOrDefault(x => x.position == position);

                // If the sticker is not found, return false indicating failure
                if (userSticker == null) return false;

                // Remove the sticker from the user's interface

                //fid the sticker and set the position to null
                var stickerToRemove = thisPaciente.userInterface.userStickers
                    .FirstOrDefault(x => x.sticker.stickerId == userSticker.sticker.stickerId);

                if (stickerToRemove != null)
                {
                    stickerToRemove.position = null;
                }           

                // Save the changes to the database
                db.SaveChanges();

                // Return true indicating success
                return true;
            }
        }


        public int putFlowerInInterface(int idPatient, int idFlower, int position)
        {
            // Validate the patient ID and flower ID to ensure they are greater than 0
            if (idPatient <= 0 || idFlower <= 0) return 0;

            // Set up the connection to the database using the DbContext
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                .Options;

            // Open the database context and perform the update inside a 'using' block to ensure resources are properly disposed of
            using (var db = new DBContext(options: connectionOptions))
            {
                // Find the patient and their user interface, including the list of flowers
                var thisPaciente = db.patients
                    .Where(x => x.userId == idPatient)
                    .Include(x => x.userInterface)
                    .ThenInclude(x => x.userFlowers)
                    .ThenInclude(x => x.flower)
                    .FirstOrDefault();

                // If the patient is not found, return 0 indicating failure
                if (thisPaciente == null) return 0;

                // Find the flower to be updated
                var userFlower = thisPaciente.userInterface.userFlowers
                    .FirstOrDefault(x => x.flower.flowerId == idFlower);

                // If the flower is not found, return 0 indicating failure
                if (userFlower == null) return 0;

                // Validate the position; if it's invalid (less than 0 or greater than 4), set the flower's position to null
                if (position < 1 || position > 3)
                {
                    userFlower.position = null;
                    userFlower.active = false;
                }
                else
                {
                    // Check if any other flower already occupies the specified position
                    var flowerWithSamePosition = thisPaciente.userInterface.userFlowers
                        .FirstOrDefault(x => x.position == position);

                    // If a flower occupies the position, reset its position to null
                    if (flowerWithSamePosition != null)
                    {
                        flowerWithSamePosition.position = null;
                        flowerWithSamePosition.active = false;
                    }

                    // Set the position of the current flower to the new position
                    userFlower.position = position;
                    userFlower.active = true;
                }

                // Save the changes to the database
                db.SaveChanges();

                // Return the ID of the updated user flower
                return userFlower.userFlowerId;
            }
        }


        /// <summary>
        /// A dictionary that maps specific progress conditions to their corresponding validation functions.
        /// Each function checks whether a certain stage or condition is met for a given patient.
        /// </summary>
        /// <remarks>
        /// The keys in the dictionary represent the name of the condition or stage, and the values are 
        /// the validation functions that take the patient's ID, value, and day range to determine if 
        /// the condition is satisfied.
        /// </remarks>
        /// <example>
        /// Example of usage:
        /// To validate if the patient has completed the tutorial:
        /// bool isTutorialCompleted = progressFunctionMap["tutorialCompleted"](null, null, patientId);
        /// </example>
        private readonly Dictionary<string, Func<int?, int?, int, bool>> progressFunctionMap = new Dictionary<string, Func<int?, int?, int, bool>> {
            
            /// <summary>
            /// Validates if the patient is registered in the system.
            /// </summary>
            /// <param name="value">Not used.</param>
            /// <param name="dayRange">Not used.</param>
            /// <param name="idPatient">The ID of the patient to check for registration.</param>
            /// <returns>True if the patient is registered, otherwise false.</returns>
            {"patientRegister", validateRegisterPatient},

            /// <summary>
            /// Validates if the patient has completed their first test.
            /// </summary>
            /// <param name="value">The minimum number of tests to be completed.</param>
            /// <param name="dayRange">Not used.</param>
            /// <param name="idPatient">The ID of the patient whose test completion is being validated.</param>
            /// <returns>True if the patient has completed the first test, otherwise false.</returns>
            {"firstTestComplete", validateFirstTestComplete},

            /// <summary>
            /// Validates if the patient has completed the tutorial.
            /// </summary>
            /// <param name="value">Not used.</param>
            /// <param name="dayRange">Not used.</param>
            /// <param name="idPatient">The ID of the patient whose tutorial completion is being checked.</param>
            /// <returns>True if the patient has completed the tutorial, otherwise false.</returns>
            {"tutorialCompleted", validateTutorialCompleted},

            /// <summary>
            /// Validates if the patient has created at least one diary entry.
            /// </summary>
            /// <param name="value">Not used.</param>
            /// <param name="dayRange">Not used.</param>
            /// <param name="idPatient">The ID of the patient whose diary entries are being validated.</param>
            /// <returns>True if the patient has created at least one diary entry, otherwise false.</returns>
            {"firstDiary", validateFirstDiary},

            /// <summary>
            /// Validates if the patient has completed at least one recommendation.
            /// </summary>
            /// <param name="value">The minimum number of recommendations to be completed.</param>
            /// <param name="dayRange">Not used.</param>
            /// <param name="idPatient">The ID of the patient whose recommendations are being validated.</param>
            /// <returns>True if the patient has completed the required recommendations, otherwise false.</returns>
            {"oneRecommendation", validateOneRecommendation},

            /// <summary>
            /// Validates if the patient has been assigned to a specialist.
            /// </summary>
            /// <param name="value">Not used.</param>
            /// <param name="dayRange">Not used.</param>
            /// <param name="idPatient">The ID of the patient whose specialist assignment is being validated.</param>
            /// <returns>True if the patient has been assigned to a specialist, otherwise false.</returns>
            {"relateSpecialist", validateRelateSpecialist},

            /// <summary>
            /// Validates if the patient has created a specified number of diary entries within a given time frame.
            /// </summary>
            /// <param name="value">The minimum number of diary entries per day.</param>
            /// <param name="dayRange">The time range (in days) within which the entries should have been created.</param>
            /// <param name="idPatient">The ID of the patient whose diary entries are being validated.</param>
            /// <returns>True if the patient meets the diary entry criteria, otherwise false.</returns>
            {"diary", validatediaryforDays},
        };


        private static bool validateRelateSpecialist(int? value, int? dayRange, int idPatient) {
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var patient = db.patients
                    .Where(x => x.userId == idPatient)
                    .Include(x => x.specialist)
                    .FirstOrDefault();

                return patient != null && patient.specialist != null;
            }
        }

        private static bool validateOneRecommendation(int? value, int? dayRange, int idPatient) {
        
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var patient = db.patients
                    .Where(x => x.userId == idPatient)
                    .Include(x => x.completeRecomendations)
                    .FirstOrDefault();

                return patient != null && patient.completeRecomendations.Count >= value;
            }
        }

        private static bool validateTutorialCompleted(int? value, int? dayRange, int idPatient) {
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var patient = db.patients
                    .Find(idPatient);

                if(patient == null) return false;

                
                var status = patient.registerState;

                return status == "registerSuccess";

            }
        }

        private static bool validateFirstTestComplete(int? value, int? dayRange, int idPatient) {

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var patient = db.patients
                    .Where(x => x.userId == idPatient)
                    .Include(x => x.test).ThenInclude(x => x.completeQuestionnaires);

                var completedTests = patient.SelectMany(x => x.test.completeQuestionnaires).ToList();

                return completedTests.Count >= 1;
            }
        }
    

        private static bool validateRegisterPatient(int? value, int? dayRange, int idPatient) {
    

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var patient = db.patients
                    .Where(x => x.userId == idPatient);
                    
                return patient != null;
            }
        }


       private static bool validatediaryforDays(int? value, int? dayRange, int idPatient)
        {
            // Validaciones iniciales
            if (value == null || dayRange == null || idPatient <= 0) return false;

            // Configuración de la conexión a la base de datos
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                .Options;
            
            using (var db = new DBContext(options: connectionOptions))
            {
                // Obtener el paciente y sus notas asociadas
                var patient = db.patients
                    .Where(x => x.userId == idPatient)
                    .Include(x => x.diary)
                    .ThenInclude(x => x.notes)
                    .FirstOrDefault();

                if (patient == null || patient.diary == null) return false;

                // Fecha actual y límite de días
                var currentDate = DateTime.Now;
                var startDate = currentDate.AddDays(-dayRange.Value);

                // Filtrar las notas dentro del rango de días
                var recentNotes = patient.diary.notes
                    .Where(x => x.dateCreated >= startDate && x.dateCreated <= currentDate)
                    .ToList();

                // Agrupar notas por día
                var notesGroupedByDay = recentNotes
                    .GroupBy(x => x.dateCreated.Date) // Agrupación por día (sin hora)
                    .ToDictionary(g => g.Key, g => g.Count());

                // Verificar que cada día tenga al menos la cantidad especificada de notas
                for (var date = startDate.Date; date <= currentDate.Date; date = date.AddDays(1))
                {
                    // Verifica si hay suficientes notas para el día actual
                    if (!notesGroupedByDay.TryGetValue(date, out var notesCount) || notesCount < value)
                    {
                        return false; // Si algún día no cumple, retornar false
                    }
                }

                // Si todos los días cumplen con el mínimo de notas, retornar true
                return true;
            }
        }

        //stageRequestId: 4

        private static bool validateFirstDiary(int? value, int? dayRange, int idPatient) {
          
         var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var thisPaciente = db.patients.Where(x => x.userId == idPatient).Include(x => x.diary).ThenInclude(x => x.notes).FirstOrDefault();

                if (thisPaciente == null) return false;

                var diary = thisPaciente.diary;

      

                return diary.notes.Count >= 1;
            }

           
        }

        //stageRequestId: 5
        // Una funcion que valida el numero de recomendaciones hechos por el paciente

        /*
        private static bool validateRecommendationCount(int? value, int? dayRange, int idPatient)
        {
            if (value == null || dayRange == null || idPatient <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var patient = db.patients
                    .Where(x => x.userId == idPatient)
                    .Include(x => x.recomendations)
                    .FirstOrDefault();

                return patient != null && patient.recomendations.Count >= value;
            }
        }

        //stageRequestId: 6
        // El usuario se vincula con su especialista
        private static bool validateRelateSpecialist(int? value, int? dayRange, int idPatient)
        {
            if (value == null || dayRange == null || idPatient <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var patient = db.patients
                    .Where(x => x.userId == idPatient)
                    .Include(x => x.specialist)
                    .FirstOrDefault();

                return patient != null && patient.specialist != null;
            }
        }

        //stageRequestId: 7
        // El usuario tiene al menos un objetivo

        */



        /// <summary>
        /// Validates the progress of a patient based on the specified progress name and parameters.
        /// The method uses a function map to find the appropriate validation function and then calls it.
        /// </summary>
        /// <param name="name">The name of the progress to be validated. This determines which validation function is used. This is based on the user's flower stage Request</param>
        /// <param name="value">An optional value used for the validation, depending on the progress being validated.</param>
        /// <param name="dayRange">An optional day range used for time-based validations.</param>
        /// <param name="idPatient">The unique ID of the patient whose progress is being validated.</param>
        /// <returns>
        /// Returns true if the progress is validated successfully using the corresponding validation function; otherwise, returns false.
        /// </returns>
        private bool validateProgress(string name, int? value, int? dayRange, int idPatient) {
            if (progressFunctionMap.ContainsKey(name)) {
                return progressFunctionMap[name](value, dayRange, idPatient);
            }
            return false;
        }


        /// <summary>
        /// Determines whether a specific patient can "grow" to the next stage based on their progress.
        /// This method checks the patient's progress and validates whether all the stage requests for their current stage are met.
        /// If any stage request is not satisfied, the method returns false. If all are satisfied, it returns true.
        /// </summary>
        /// <param name="idPatient">The unique ID of the patient whose ability to grow to the next stage is being evaluated.</param>
        /// <returns>
        /// Returns true if all stage requests for the current stage are satisfied and the patient can grow to the next stage;
        /// otherwise, returns false if any stage request is not met or if the patient is not found.
        /// </returns>
        public bool canGrowFlower(int idPatient) {
            if (idPatient <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
            .UseSqlServer(Data.Helpers.Constants.ConnectionString)
            .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var thisPaciente = db.patients.Where(x => x.userId == idPatient).Include(x => x.progress).FirstOrDefault();

                var stages = db.stages.Include(x => x.stageRequests).ToList();

                

                List<StageRequest> stagesRequests = stages[thisPaciente!.progress!.stage!].stageRequests;

                bool canGrow = true;

                foreach (var stageRequest in stagesRequests) {
                    
                    if (validateProgress(stageRequest.name, stageRequest.value, stageRequest.dayRange, thisPaciente.userId) == false) {
                       

                        canGrow = false;
                        return canGrow;
                    }
                }

                return canGrow;
            }
        }

    

        /// <summary>
        /// Increments the progress stage for a specific patient, moving them to the next stage in their progress tracking.
        /// The method retrieves the patient by their unique ID and updates their current progress stage.
        /// </summary>
        /// <param name="idPatient">The unique ID of the patient whose progress stage is being incremented.</param>
        /// <returns>
        /// Returns true if the stage is successfully incremented and saved to the database; otherwise, returns false if the patient is not found or if an error occurs.
        /// </returns>
        public bool growStage(int idPatient) {
            if (idPatient <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
            .UseSqlServer(Data.Helpers.Constants.ConnectionString)
            .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var thisPaciente = db.patients.Where(x => x.userId == idPatient).Include(x => x.progress).Include(x => x.userInterface).ThenInclude(x => x.userFlowers).FirstOrDefault();

                if (thisPaciente == null) return false;


                if(thisPaciente.progress!.stage > 5) {
                    thisPaciente.progress!.stage = thisPaciente.progress!.stage! + 1;
                } else {
                    thisPaciente.progress!.stage = 0;
                }

                var userFlower = thisPaciente.userInterface.userFlowers.FirstOrDefault(x => x.position == 2);

                if (userFlower != null) {
                    if(userFlower.state < 5) {
                        userFlower.state = userFlower.state + 1;
                    } 
                }
                

                db.SaveChanges();

                return true;
            }
        }

        


        /// <summary>
        /// Checks if the progress of a specific patient can be reviewed based on the last update date.
        /// If the patient's progress has not been updated in the last 7 days, it returns true, indicating the progress can be reviewed.
        /// </summary>
        /// <param name="idPatient">The unique ID of the patient whose progress is being checked.</param>
        /// <returns>
        /// Returns true if the progress can be reviewed (i.e., the last update was more than 7 days ago or if the progress has never been updated).
        /// Returns false if the progress was updated within the last 7 days.
        /// </returns>
        public bool reviewCanCheck(int idPatient) {
            if (idPatient <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
            .UseSqlServer(Data.Helpers.Constants.ConnectionString)
            .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var thisPaciente = db.patients.Where(x => x.userId == idPatient).Include(x => x.progress).Include(x => x.userInterface).ThenInclude(x => x.userFlowers).FirstOrDefault();

                // Revisa cuando fue la última vez que se actualizó el progreso (lastDate).
                if (thisPaciente == null) return false;

                var currentDate = DateTime.Now;

                var lastDate = thisPaciente.progress!.lastDate;

                var begginDate = thisPaciente.progress!.begginDate;

                if (lastDate == null) return true;

                if (begginDate == null) return true;

                // Si begginDate es igual a lastDate, entonces no se ha actualizado el progreso, por lo tanto se retorna true.
                if (begginDate == lastDate) return true;

                var days = (currentDate - lastDate!.Value).TotalDays;

                //valida que la diferencia de días sea mayor o igual a 7 y que la cuenta no tenga mas de 7 días de haberse creado

                if (days < 7) {
                    if(thisPaciente.dateCreated.AddDays(7) >= currentDate 
                    ) {
                        //encuentra la flor que esta en la posición 2 y revisa que este en el estado 0

                        var userFlower = thisPaciente.userInterface.userFlowers.Where(x => x.position == 2).FirstOrDefault();

                        if (userFlower != null) {
                            if(userFlower.state == 0) {
                                return true;
                            }
                        } else {

                        return false;
                        }

                    return false;
                    }

                    return false;
                } else {

                return true;
                }
            }
        }


        /// <summary>
        /// Updates the last progress update date for a specific patient to the current date and time.
        /// This method sets the patient's last progress update to the current date and saves the changes to the database.
        /// </summary>
        /// <param name="idPatient">The unique ID of the patient whose progress update date is being modified.</param>
        /// <returns>
        /// Returns true if the progress update date was successfully updated; false if the patient was not found or if the ID is invalid.
        /// </returns>
        public bool updateLastProgressDate(int idPatient) {
            if (idPatient <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
            .UseSqlServer(Data.Helpers.Constants.ConnectionString)
            .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var thisPaciente = db.patients.Where(x => x.userId == idPatient).Include(x => x.progress).FirstOrDefault();

                if (thisPaciente == null) return false;

                thisPaciente.progress!.lastDate = DateTime.Now;

                db.SaveChanges();

                return true;
            }
        }

        public bool actualizarThemeId(int idPatient, int themeId)
        {

        
            if (idPatient <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
            .UseSqlServer(Data.Helpers.Constants.ConnectionString)
            .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
            

                var thisPaciente = db.patients.Where(x => x.userId == idPatient).Include(x => x.userInterface).FirstOrDefault();

                if (thisPaciente == null) return false;

                thisPaciente.userInterface.themeId = themeId;

                db.SaveChanges();

                return true;
            }
        }

        public bool actualizarBackgroundId(int idPatient, int backgroundId)
        {
            if (idPatient <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
            .UseSqlServer(Data.Helpers.Constants.ConnectionString)
            .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var thisPaciente = db.patients.Where(x => x.userId == idPatient).Include(x => x.userInterface).FirstOrDefault();
                if (thisPaciente == null) return false;

                thisPaciente.userInterface.backgroundUrl = backgroundId;

                db.SaveChanges();

                return true;
            }
        }

        //SoftDelete, solo cambia el email a ""
        public bool SoftDelete(int idPatient)
        {
            if (idPatient <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
            .UseSqlServer(Data.Helpers.Constants.ConnectionString)
            .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var thisPaciente = db.patients.Where(x => x.userId == idPatient).FirstOrDefault();
                if (thisPaciente == null) return false;

                thisPaciente.mail = "";

                db.SaveChanges();

                return true;
            }

        }

        public bool ConfirmarUsuario(int id)
        {
            if (id <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
            .UseSqlServer(Data.Helpers.Constants.ConnectionString)
            .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var thisPaciente = db.patients.Where(x => x.userId == id).FirstOrDefault();
                if (thisPaciente == null) return false;

                thisPaciente.confirmed = true;

                db.SaveChanges();

                return true;
            }
        }

        public Patient? GetByEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) return null;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
            .UseSqlServer(Data.Helpers.Constants.ConnectionString)
            .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var thisPaciente = db.patients.Where(x => x.mail == email).FirstOrDefault();
                if (thisPaciente == null) return null;

                return thisPaciente;
            }
        }

        public string GetForgotPassword(int idPatient)
        {
            if (idPatient <= 0) return "";

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
            .UseSqlServer(Data.Helpers.Constants.ConnectionString)
            .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
              //primero encuentras el paciente, despues crear un codigo aleatorio de 6 digitos
                var thisPaciente = db.patients.Where(x => x.userId == idPatient).FirstOrDefault();
                if (thisPaciente == null) return "";

                //crea un codigo aleatorio de 6 digitos
                Random random = new Random();
                int code = random.Next(100000, 999999);

                //actualiza el paciente con el nuevo codigo
                thisPaciente.codeHelper = code.ToString();

                db.SaveChanges();

                //retorna el codigo
                return code.ToString();
            }
        }

        public bool ValidarCodigo(int idPatient, string code)
        {
            if (idPatient <= 0 || string.IsNullOrEmpty(code)) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
            .UseSqlServer(Data.Helpers.Constants.ConnectionString)
            .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var thisPaciente = db.patients.Where(x => x.userId == idPatient).FirstOrDefault();
                if (thisPaciente == null) return false;

                //compara el codigo del paciente con el codigo que le pasas
                if (thisPaciente.codeHelper != code) return false;

                //si son iguales, actualiza el paciente con el nuevo codigo
                thisPaciente.codeHelper = "";

                db.SaveChanges();

                return true;
            }
        }

        public bool ModificarContraseña(int idPatient, string password)
        {
            if (idPatient <= 0 || string.IsNullOrEmpty(password)) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
            .UseSqlServer(Data.Helpers.Constants.ConnectionString)
            .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var thisPaciente = db.patients.Where(x => x.userId == idPatient).FirstOrDefault();
                if (thisPaciente == null) return false;

                //actualiza el paciente con el nuevo codigo
                thisPaciente.password = password;

                db.SaveChanges();

                return true;
            }
        }
    }
   
}


//crea un mapa que retorne funciones, me explico, si el name es firstDiary, lo que retornas es la funcion validateFirstDiary, y asi con todos los nombres de los stageRequest
//despues, en el metodo canGrowFlower, vas a iterar sobre todos los stageRequest, y vas a llamar a la funcion que corresponde al nombre del stageRequest, si alguna de las funciones retorna false, entonces retornas false, si todas las funciones retornan true, entonces retornas true




 