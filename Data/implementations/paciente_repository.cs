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
        public int Add(AddPatient patient)
        {
            if (patient == null) return 0;

            // Email validation regex pattern
            var emailRegex = new System.Text.RegularExpressions.Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");

            if (!emailRegex.IsMatch(patient.mail))
                throw new ArgumentException("Must enter a valid email address.");
            
            if (!emailRegex.IsMatch(patient.mail)) return -3; // Invalid email format

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                .Options;

            using (var db = new DBContext(options: connectionOptions))
            {
                var thisEmailExist = db.patients.FirstOrDefault(x => x.mail == patient.mail);
                var thisNumberExist = db.patients.FirstOrDefault(x => x.phone == patient.phone);    

                if (thisNumberExist != null) return -2;

                if (thisEmailExist == null)
                {
                    var isaEspacialistaEmail = db.specialists.FirstOrDefault(x => x.mail == patient.mail);
                    if (isaEspacialistaEmail != null) return -1;

                    var newpaciente = new Patient
                    {
                        name = patient.name,
                        mail = patient.mail,
                        password = patient.password,
                        phone = patient.phone,
                        bornDate = patient.bornDate,
                        age = getEdadFromBirthDate(patient.bornDate),
                        sex = patient.sex,
                        token = patient.token,
                        relationalToken = getTheFirstSixDigits(patient.token),
                        termsAndConditions = db.terms!.FirstOrDefault(x => x.termsAndConditionsId == patient.termsiD)!,
                    };

                    db.patients.Add(newpaciente);
                    db.SaveChanges();
                    return newpaciente.userId;
                }
                else
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

                var thisTokenExist = db.patients.FirstOrDefault(x => x.relationalToken == posibleRelationalToken);
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


        public Patient? Get(int id)
        {
            if (id <= 0) return null;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var stickers = db.stickers.ToList();
                var flowers = db.flowers.ToList();

                return db.patients.Where(x => x.userId == id).Include(x => x.specialist).Include(x => x.goals).Include(x => x.userInterface
                ).Include(x => x.userInterface.userFlowers).Include(x => x.userInterface.userStickers).
                Include(x => x.settings).
                Include(x => x.termsAndConditions).Select
                    (x => new Patient
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
                    goals = x.goals,
                    dateCreated = DateTime.Now,
                    modifiedDate = DateTime.Now,
                    termsAndConditions = x.termsAndConditions,
                    settings = x.settings,
                    progress = x.progress,
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
                        termsAndConditions = x.specialist.termsAndConditions,
                        relationalToken = x.specialist.relationalToken,
                        dateCreated = x.specialist.dateCreated,
                        modifiedDate = x.specialist.modifiedDate
                        
                    },
                    registerState = x.registerState
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
                var patient = db.patients.FirstOrDefault(x => x.userId == id);
                if (patient == null) return false;

                db.patients.Remove(patient);
                db.SaveChanges();
                return true;
            }
        }

        public bool Update(Patient patient)
        {
            if (patient == null) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var thisPaciente = db.patients.FirstOrDefault(x => x.userId == patient.userId);
                if (thisPaciente == null) return false;

                thisPaciente.name = patient.name;
                thisPaciente.age = patient.age;
                thisPaciente.mail = patient.mail;
                thisPaciente.phone = patient.phone;
                thisPaciente.password = patient.password;
                thisPaciente.modifiedDate = DateTime.Now;

                db.patients.Update(thisPaciente);
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
                var thisPaciente = db.patients.FirstOrDefault(x => x.userId == id);
                if (thisPaciente == null) return false;

                var thisEspecialista = db.specialists.FirstOrDefault(x => x.relationalToken == tokenEspecialista);
                if (thisEspecialista == null) return false;

                thisPaciente.specialist = thisEspecialista;
                thisPaciente.modifiedDate = DateTime.Now;
                thisEspecialista.modifiedDate = DateTime.Now;
                thisEspecialista.
                    patients.Add(thisPaciente);

                db.patients.Update(thisPaciente);
                db.specialists.Update(thisEspecialista);
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
                var thisPaciente = db.patients.FirstOrDefault(x => x.userId == idPaciente);
                if (thisPaciente == null) return null;

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

        public bool MoficarConfiguracionNotificaciones(int id, bool notificacionesActivas, bool dirioActivado, bool progresoActivado)
        {
            if (id <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var thisPaciente = db.patients.FirstOrDefault(x => x.userId == id);
                if (thisPaciente == null) return false;

                thisPaciente.settings.notificationsActive = notificacionesActivas;
                thisPaciente.settings.diaryActive = dirioActivado;
                thisPaciente.settings.questionnaireActive = progresoActivado;
                thisPaciente.modifiedDate = DateTime.Now;

                db.patients.Update(thisPaciente);
                db.SaveChanges();
                return true;
            }
        }

        public int AgregarFlorInicial(int id, 
            int patientId
        )
        {
            if (id <= 0) return 0;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;
            using (var db = new DBContext(options: connectionOptions))
            {

                var thisPaciente = db.patients.FirstOrDefault(x => x.userId == patientId);

                if(thisPaciente == null) return 0;
               

                var thisFlor = db.flowers.FirstOrDefault(x => x.flowerId == 1);
                if (thisFlor == null) return 0;

                thisPaciente.userInterface.userFlowers.Add(
                    new UserFlower
                    {
                        flower = thisFlor,
                        position = 2,
                        state = 0
                    }
                );

                
                db.SaveChanges();
                return  thisFlor.flowerId;
            }
        }

        public int agregarStickerDeUsuarioModel(int? index, int idUsuario) {
              var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;
            using (var db = new DBContext(options: connectionOptions))
            {

                var patient = db.patients.FirstOrDefault(x => x.userId == idUsuario);

                if(patient == null) return 0;


              List<Sticker> stickers = db.stickers.ToList();

                UserSticker stickerDeUsuario; 

              if(index == null) {

               stickerDeUsuario = getStickerFromIndex(stickers, 1, 2, idUsuario);

              } else {
                stickerDeUsuario = stickers.Where(x => x.stickerId != index && x.stickerId < 10)
                    .Select(x => new UserSticker
                    {
                        sticker = x,
                        position = null,

                    }).FirstOrDefault()!;
              }

            patient.userInterface.userStickers.Add(stickerDeUsuario);


                db.SaveChanges();
                return stickerDeUsuario.sticker.stickerId;
            }
        }


        private UserSticker getStickerFromIndex(List<Sticker> stickers, int index, int maxIndex, int idUsuario)
        {
          //get a random sticker from the list of stickers and return it
            Random random = new Random();
            int randomIndex = random.Next(index, maxIndex);
            return new UserSticker
            {
                sticker = stickers[randomIndex],
                position = null,

            };       
        }

        public string registerSet(
            int patientId,
            string state)
        {
            if (patientId <= 0) return "error";

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var thisPaciente = db.patients.FirstOrDefault(x => x.userId == patientId);
                if (thisPaciente == null) return "error";

                thisPaciente.registerState = state;
                thisPaciente.modifiedDate = DateTime.Now;

                db.patients.Update(thisPaciente);
                db.SaveChanges();
                return "success";
            }
        }



        public int putStickeriInInterface(int idPatient, int idUserSticker, int position) {


            if (idPatient <= 0 || idUserSticker <= 0) return 0;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                //primero vamos a encontrar al paciente y vamos a importar dede el paciente sus stickers, despues vamos a encontrar el sticker que queremos agregar y si esta en la lista de stickers del paciente, vamos a modificar la posicion del sticker, pe tenemos que tener cuidado al hacer eso, ya que primero tenemos que revisar si hay otro de sus sticker que tienen la misma posicion, si es asi, vamos a cambiar la posicion de ese sticker a null, y despues vamos a cambiar la posicion del sticker que queremos agregar

                var thisPaciente = db.patients.Where(x => x.userId == idPatient).Include(x => x.userInterface).ThenInclude(x => x.userStickers).ThenInclude(x => x.sticker).FirstOrDefault();

                Console.WriteLine(thisPaciente);

                if (thisPaciente == null) return 0;

                var userSticker = thisPaciente.userInterface.userStickers.FirstOrDefault(x => x.sticker.stickerId == idUserSticker);

                if (userSticker == null) return 0;


                if (position < 0 || position > 4) {
                    userSticker.position = null;
                } else {
                    var stickerWithSamePosition = thisPaciente.userInterface.userStickers.FirstOrDefault(x => x.position == position);
                    if (stickerWithSamePosition != null) {
                        stickerWithSamePosition.position = null;
                    }
                    userSticker.position = position;
                }

                db.SaveChanges();

                return userSticker.userStickerId;
            }


        }

        private readonly Dictionary<string, Func<int?, int?, int, bool>> progressFunctionMap = new Dictionary<string, Func<int?, int?, int, bool>> {
            {"patientRegister" , validateRegisterPatient},
            {"firstTestComplete", validateFirstTestComplete},
            {"tutorialCompleted", validateTutorialCompleted},
            { "firstDiary", validateFirstDiary },
             {"oneRecommendation", validateOneRecommendation},
            {"relateSpecialist", validateRelateSpecialist},
            { "diary", validatediaryforDays},
           
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
                    .Where(x => x.userId == idPatient)
                    ;

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



        private bool validateProgress(string name, int? value, int? dayRange, int idPatient) {

            if (progressFunctionMap.ContainsKey(name)) {
      
                return progressFunctionMap[name](value, dayRange, idPatient);
            }
            return false;
        }

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
    

    public bool growStage(int idPatient) {
        if (idPatient <= 0) return false;

        var connectionOptions = new DbContextOptionsBuilder<DBContext>()
          .UseSqlServer(Data.Helpers.Constants.ConnectionString)
          .Options;
        using (var db = new DBContext(options: connectionOptions))
        {
            var thisPaciente = db.patients.Where(x => x.userId == idPatient).Include(x => x.progress).FirstOrDefault();

            if (thisPaciente == null) return false;

            thisPaciente.progress!.stage = thisPaciente.progress!.stage! + 1;

            db.SaveChanges();

            return true;
        }
    }


}
    
    
}


//crea un mapa que retorne funciones, me explico, si el name es firstDiary, lo que retornas es la funcion validateFirstDiary, y asi con todos los nombres de los stageRequest
//despues, en el metodo canGrowFlower, vas a iterar sobre todos los stageRequest, y vas a llamar a la funcion que corresponde al nombre del stageRequest, si alguna de las funciones retorna false, entonces retornas false, si todas las funciones retornan true, entonces retornas true




 