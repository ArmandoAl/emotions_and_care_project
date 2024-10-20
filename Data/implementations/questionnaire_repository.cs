using Data.Contracts;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Data.Implementations
{
     public class CuestionarioRepository : IQuestionnaireRepository
     {
        public int AddQuestionnaire(Questionnaire cuestionario)
        {
            if (cuestionario == null) return 0;

            cuestionario.dateCreated = DateTime.Now;
            cuestionario.modifiedDate = DateTime.Now;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;
            using (var db = new DBContext(options: connectionOptions))
            {

                
                db.questionnaires.Add(cuestionario);

                db.SaveChanges();
                return  cuestionario.questionnaireId;
            }
        }

        public bool AgregarQuestionnaireAPacientes(int idCuestionario)
        {
            if(idCuestionario <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;

            using (var db = new DBContext(options: connectionOptions))
            {
                var pacientes = db.patients.ToList();
                var cuestionario = db.questionnaires.FirstOrDefault(x => x.questionnaireId == idCuestionario);
                if (cuestionario == null) return false;

                foreach (var paciente in pacientes)
                {
                    paciente.test.questionnaires.Add(new QuestionnaireForUser
                    { questionnaireId = idCuestionario });
                }
                db.SaveChanges();
                return true;
            }   
        }

        public int AgregarHistorialQuestionnaire(int idCuestionario, List<TestQuestionForComplete> respuestas, int pacienteId)
        {
            if (idCuestionario <= 0) return 0;
            if (respuestas == null || respuestas.Count == 0) return 0;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                .Options;

            using (var db = new DBContext(options: connectionOptions))
            {

                var patient = db.patients.Where(x => x.userId == pacienteId).Include(x => x.test.questionnaires).Include(x => x.test.questionnairesHistory).Include(x => x.test.completeQuestionnaires).FirstOrDefault();

                if (patient == null) return 0;

                //obtener el cuestionario con sus preguntas y respuestas
              var cuestionario = db.questionnaires
                .Where(x => x.questionnaireId == idCuestionario)
                .Include(x => x.questions)
                .ThenInclude(q => q.answers)  // Usa ThenInclude para cargar respuestas
                .Include(x => x.questionnaireResults)
                .FirstOrDefault();


                if (cuestionario == null) return 0;

                var listQuestions = new List<TestQuestionWithAnswer>();
                foreach (var respuesta in respuestas)
                {
                   
                    //obtener la pregunta con sus respuestas
                    Question? pregunta = cuestionario.questions.FirstOrDefault(x => x.questionId == respuesta.questionId);

                    if (pregunta == null) return 0;

                    List<Answer>? listaRespuestas = pregunta.answers;

                    if(listaRespuestas == null) return 0;

                   var respuestaSeleccionada = listaRespuestas[respuesta.answerId];

                    if (respuestaSeleccionada == null) return 0;

                    listQuestions.Add(new TestQuestionWithAnswer
                    {
                        question = pregunta.statement,
                        answer = respuestaSeleccionada.answerText
                    });
                }

                var historialesCuestionariosList = patient.test.questionnairesHistory;
                 

                var historialCuestionario = historialesCuestionariosList!.FirstOrDefault(x => x.questionnaireId == idCuestionario);

                if (historialCuestionario == null)
                {
               
                    historialCuestionario = new QuestionnairesHistory
                    {
                        questionnaireId = idCuestionario,
                        name = cuestionario.questionnaireName,
                        testInfoModels = new List<TestInfoModel>
                        {
                            new TestInfoModel
                            {
                                result = getQuestionnaireResult(respuestas.Select(x => x.answerId).ToList(), 
                                cuestionario.questionnaireResults
                                ),
                                date = DateTime.Now,
                                testQuestionWithAnswers = listQuestions
                            }
                        }
                    };

                    patient.test.questionnairesHistory.Add(historialCuestionario);


                    db.SaveChanges();
                    return historialCuestionario.questionnairesHistoryId;

                }
                else
                {

                    historialCuestionario.testInfoModels.Add(new TestInfoModel
                    {
                        result = getQuestionnaireResult(respuestas.Select(x => x.answerId).ToList(), 
                        cuestionario.questionnaireResults
                        ),
                        date = DateTime.Now,
                        testQuestionWithAnswers = listQuestions
                    });
                    db.SaveChanges();
               
                    return historialCuestionario.questionnairesHistoryId;
                }
              
            }
        }

        private string  getQuestionnaireResult(List<int> respuestas,
            List<QuestionnaireResult> cuestionarioResults
         ) {

            var sum = respuestas.Sum();

            var result = cuestionarioResults.OrderBy(x => Math.Abs(x.value - sum)).FirstOrDefault();

            if (result == null) return "No se encontro un resultado";

            return result.result;

        }



        public bool completarQuestionnaire(int idCuestionario, int idPaciente)
        {
            if (idCuestionario <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;
            using (var db = new DBContext(options: connectionOptions))
            {

                var patient = db.patients.Where(x => x.userId == idPaciente).Include(x => x.test.completeQuestionnaires).Include(x => x.test.questionnaires).Include(x => x.test.questionnairesHistory).FirstOrDefault();

                if (patient == null) return false;

                var cuestionarioCompletado =  patient.test.completeQuestionnaires.FirstOrDefault(x => x.questionnaireId == idCuestionario);

              //  if (cuestionarioCompletado != null) return true;


                if (cuestionarioCompletado != null)
                {
                    cuestionarioCompletado.dateCompleted = DateTime.Now;
                }
                else
                {
                    var newcuestionarioCompletado = new CompleteQuestionnaires
                    {
                        patientId = idPaciente,
                        questionnaireId = idCuestionario,
                        dateCompleted = DateTime.Now
                    };
                    
                    patient.test.completeQuestionnaires.Add(newcuestionarioCompletado);
                }
                
                db.SaveChanges();
                return true;
            }
        }

        public bool DeleteQuestionnaire(int idCuestionario, int idPaciente)
        {
            if (idCuestionario <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;
            using (var db = new DBContext(options: connectionOptions))
            {

                var patient = db.patients.FirstOrDefault(x => x.userId == idPaciente);

                if (patient == null) return false;

                var cuestionario =  patient.test.questionnaires.FirstOrDefault(x => x.questionnaireId == idCuestionario);

                if (cuestionario == null) return false;

                
                patient.test.questionnaires.Remove(cuestionario);

                db.SaveChanges();
                return true;
            }
        }   

        public Questionnaire? GetQuestionnaire(int idCuestionario)
        {
            if (idCuestionario <= 0) return null;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                return db.questionnaires.FirstOrDefault(x => x.questionnaireId == idCuestionario);
            }
        }   

        public bool UpdateQuestionnaire(Questionnaire cuestionario)
        {
            if (cuestionario == null) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;
            using (var db = new DBContext(options: connectionOptions))
            {

                var thisCuestionario = db.questionnaires.FirstOrDefault(x => x.questionnaireId == cuestionario.questionnaireId);
                if (thisCuestionario == null) return false;

                thisCuestionario.questionnaireName = cuestionario.questionnaireName;
                thisCuestionario.description = cuestionario.description;
                thisCuestionario.instructions = cuestionario.instructions;
                thisCuestionario.modifiedDate = DateTime.Now;

                db.questionnaires.Update(thisCuestionario);
                db.SaveChanges();
                return true;
            }
        }

        // public bool relacionarHistorialCuestionarioConPaciente(int idCuestionario, int idPaciente)
        // {
        //     if (idCuestionario <= 0 || idPaciente <= 0) return false;

        //     var connectionOptions = new DbContextOptionsBuilder<DBContext>()
        //      .UseSqlServer(Data.Helpers.Constants.ConnectionString)
        //      .Options;
        //     using (var db = new DBContext(options: connectionOptions))
        //     {




        //         var historialCuestionario = db.HistorialesCuestionariosCompletados.FirstOrDefault(x => x.Id == idCuestionario);
        //         if (historialCuestionario == null) return false;

        //         var paciente = db.Pacientes.FirstOrDefault(x => x.IdUsuario == idPaciente);
        //         if (paciente == null) return false;

        //         paciente.HistorialCuestionarios.Add(historialCuestionario);
        //         db.SaveChanges();

        //         return true;
        //     }
        // }

        public QuestionnairesInfo? GetQuestionnairesInfo(int pacienteId)
        {
            if(pacienteId <= 0) return null;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var paciente = db.patients.Where(x => x.userId == pacienteId).Include(x => x.test.completeQuestionnaires).Include(x => x.test.questionnairesHistory).ThenInclude(x => x.testInfoModels).Include(x => x.test.questionnairesHistory).ThenInclude(x => x.testInfoModels).ThenInclude(x => x.testQuestionWithAnswers).FirstOrDefault();
                if (paciente == null) return null;

                 var cuestionarios = db.questionnaires
                .Include(x => x.questions)
                .ThenInclude(q => q.answers)  // Usa ThenInclude para cargar respuestas
                .Include(x => x.questionnaireResults)
                .ToList();

                var cuestionariosCompletados = paciente.test.completeQuestionnaires;

                var historialCuestionarios = paciente.test.questionnairesHistory;

                historialCuestionarios.ForEach(x => x.testInfoModels = x.testInfoModels.OrderByDescending(x => x.testInfoModelId).ToList());


                return new QuestionnairesInfo
                {
                    questionnaires = cuestionarios,
                    completeQuestionnaires = cuestionariosCompletados,
                    questionnairesHistory = historialCuestionarios
                };
            }
        }

        public bool CanMakeTest(int pacienteId)
        {
            // Este metodo revisa si alguno de los cuestionarios fue realizado hace mas de 15 dias, si es asi, elimina ese cuestionario de la lista de
            // cuestionarios completados y retorna true, si no, retorna false
            if (pacienteId <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;

            using (var db = new DBContext(options: connectionOptions))
            {
                var patient = db.patients.Include(x => x.test.completeQuestionnaires).FirstOrDefault(x => x.userId == pacienteId);

                if (patient == null) return false;

                var cuestionariosCompletados = patient.test.completeQuestionnaires;



                if (cuestionariosCompletados == null || cuestionariosCompletados.Count == 0) return false;

                var cuestionariosCompletadosToDelete = new List<CompleteQuestionnaires>();
                foreach (var cuestionarioCompletado in cuestionariosCompletados)
                {
                    if (DateTime.Now.Subtract(cuestionarioCompletado.dateCompleted).TotalDays > 15)
                    {
                        cuestionariosCompletadosToDelete.Add(cuestionarioCompletado);
                    }
                }

                if (cuestionariosCompletadosToDelete.Count > 0)
                {
                    foreach (var cuestionarioCompletado in cuestionariosCompletadosToDelete)
                    {
                        patient.test.completeQuestionnaires.Remove(cuestionarioCompletado);
                    }
                    db.SaveChanges();
                    return true;
                }
                return false;
            }

        }

        public TestInfoModel? GetTestInfoModel(int patientId,
            int idCuestionario, int idTestInfoModel)
        {
            if (idCuestionario <= 0) return null;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;
            using (var db = new DBContext(options: connectionOptions))
            {

                var patient = db.patients.Include(x => x.test.questionnairesHistory).ThenInclude(x => x.testInfoModels).ThenInclude(x => x.testQuestionWithAnswers).FirstOrDefault(x => x.userId == patientId);

                if (patient == null) return null;


                QuestionnairesHistory? historialCuestionario = patient.test.questionnairesHistory.FirstOrDefault(x => x.questionnaireId == idCuestionario);

                if (historialCuestionario == null) return null;

                //return the last testInfoModel
                var histoy = historialCuestionario.testInfoModels.OrderByDescending(x => x.testInfoModelId).FirstOrDefault();

          
                return histoy;

            }
        }


        public bool changeVisibility(
            int patientId,
            int idCuestionario, int idTestInfoModel, bool visible)
        {
            if (idCuestionario <= 0 || idTestInfoModel <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;
            using (var db = new DBContext(options: connectionOptions))
            {

                var patient = db.patients.Include(x => x.test.questionnairesHistory).ThenInclude(x => x.testInfoModels).FirstOrDefault(x => x.userId == patientId);

                if (patient == null) return false;


                var historialCuestionario = patient.test.questionnairesHistory.FirstOrDefault(x => x.questionnaireId == idCuestionario);


                if (historialCuestionario == null) return false;

                var testInfoModel = historialCuestionario.testInfoModels.FirstOrDefault(x => x.testInfoModelId == idTestInfoModel);


                if (testInfoModel == null) return false;

                testInfoModel.visible = visible;
                db.SaveChanges();
                return true;
            }
        }
    }
}








            