using Data.Contracts;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Data.Implementations
{
     public class CuestionarioRepository : ICuestionarioRepository
     {
        public int AddCuestionario(Cuestionario cuestionario)
        {
            if (cuestionario == null) return 0;

            cuestionario.FechaCreacion = DateTime.Now;
            cuestionario.FechaModificacion = DateTime.Now;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                db.Cuestionarios.Add(cuestionario);
                db.SaveChanges();
                return  cuestionario.IdCuestionario;
            }
        }

        public bool AgregarCuestionarioAPacientes(int idCuestionario)
        {
            if(idCuestionario <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;

            using (var db = new DBContext(options: connectionOptions))
            {
                var pacientes = db.Pacientes.ToList();
                var cuestionario = db.Cuestionarios.FirstOrDefault(x => x.IdCuestionario == idCuestionario);
                if (cuestionario == null) return false;

                foreach (var paciente in pacientes)
                {
                    paciente.Cuestionarios.Add(cuestionario);
                }
                db.SaveChanges();
                return true;
            }   
        }

        public int AgregarHistorialCuestionario(int idCuestionario, List<TestQuestionForComplete> respuestas, int pacienteId)
        {
            if (idCuestionario <= 0) return 0;
            if (respuestas == null || respuestas.Count == 0) return 0;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                .Options;

            using (var db = new DBContext(options: connectionOptions))
            {
                var cuestionario = db.Cuestionarios
                    .Include(c => c.Preguntas)
                    .ThenInclude(p => p.Respuestas)
                    .FirstOrDefault(x => x.IdCuestionario == idCuestionario);

                if (cuestionario == null) return 0;

                var listQuestions = new List<TestQuestionWithAnswer>();
                foreach (var respuesta in respuestas)
                {
                    var pregunta = cuestionario.Preguntas.FirstOrDefault(p => p.IdPregunta == respuesta.idPregunta);
                    if (pregunta == null) return 0;

                    var respuestaSeleccionada = pregunta.Respuestas![respuesta.posicionRespuesta];
                    if (respuestaSeleccionada == null) return 0;

                    listQuestions.Add(new TestQuestionWithAnswer
                    {
                        Question = pregunta.Enunciado,
                        Answer = respuestaSeleccionada.TextoRespuesta
                    });
                }

                var historialesCuestionariosList = db.Pacientes.Where(x => x.IdUsuario == pacienteId).Include(x => x.HistorialCuestionarios).
                    ThenInclude(x => x.testInfoModels).ThenInclude(x => x.TestQuestionWithAnswers).FirstOrDefault();
                 

                var historialCuestionario = historialesCuestionariosList!.HistorialCuestionarios.FirstOrDefault(x => x.IdCuestionario == idCuestionario);

                if (historialCuestionario == null)
                {
               
                    historialCuestionario = new HistoryTestModel
                    {
                        IdCuestionario = idCuestionario,
                        Name = cuestionario.NombreCuestionario,
                        testInfoModels = new List<TestInfoModel>
                        {
                            new TestInfoModel
                            {
                                Result = getBDI2result(respuestas.Select(x => x.posicionRespuesta).ToList()),
                                Date = DateTime.Now,
                                TestQuestionWithAnswers = listQuestions
                            }
                        }
                    };
                    db.HistorialesCuestionariosCompletados.Add(historialCuestionario);
                    db.SaveChanges();
                    Console.WriteLine(historialCuestionario.Id);
                    return historialCuestionario.Id;

                }
                else
                {
         
                    historialCuestionario.testInfoModels.Add(new TestInfoModel
                    {
                        Result = getBDI2result(respuestas.Select(x => x.posicionRespuesta).ToList()),
                        Date = DateTime.Now,
                        TestQuestionWithAnswers = listQuestions
                    });
                    db.SaveChanges();
                    return historialCuestionario.Id;
                }

               
              
            }
        }

        private string getBDI2result(List<int> respuestas)
        {
            int result = 0;
            foreach (var item in respuestas)
            {
                result += item;
            }
            if (result <= 9) return "Depresión mínima";
            if (result <= 16) return "Depresión leve";
            if (result <= 23) return "Depresión moderada";
            if (result <= 30) return "Depresión grave";
            return "Depresión muy grave";
        }

        public bool completarCuestionario(int idCuestionario, int idPaciente)
        {
            if (idCuestionario <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var cuestionarioCompletado = db.CuestionarioCompletados.FirstOrDefault(x => x.CuestionarioId == idCuestionario && x.PacienteId == idPaciente);

                if (cuestionarioCompletado != null)
                {
                    cuestionarioCompletado.FechaCompletado = DateTime.Now;
                }
                else
                {
                    var newcuestionarioCompletado = new CuestionarioCompletado
                    {
                        PacienteId = idPaciente,
                        CuestionarioId = idCuestionario,
                        FechaCompletado = DateTime.Now
                    };
                    db.CuestionarioCompletados.Add(newcuestionarioCompletado);
                }
                
                db.SaveChanges();
                return true;
            }
        }

        public bool DeleteCuestionario(int idCuestionario)
        {
            if (idCuestionario <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var cuestionario = db.Cuestionarios.FirstOrDefault(x => x.IdCuestionario == idCuestionario);
                if (cuestionario == null) return false;

                db.Cuestionarios.Remove(cuestionario);
                db.SaveChanges();
                return true;
            }
        }   

        public Cuestionario? GetCuestionario(int idCuestionario)
        {
            if (idCuestionario <= 0) return null;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                return db.Cuestionarios.FirstOrDefault(x => x.IdCuestionario == idCuestionario);
            }
        }   

        public bool UpdateCuestionario(Cuestionario cuestionario)
        {
            if (cuestionario == null) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var thisCuestionario = db.Cuestionarios.FirstOrDefault(x => x.IdCuestionario == cuestionario.IdCuestionario);
                if (thisCuestionario == null) return false;

                thisCuestionario.NombreCuestionario = cuestionario.NombreCuestionario;
                thisCuestionario.Descripcion = cuestionario.Descripcion;
                thisCuestionario.Instrucciones = cuestionario.Instrucciones;
                thisCuestionario.FechaModificacion = DateTime.Now;

                db.Cuestionarios.Update(thisCuestionario);
                db.SaveChanges();
                return true;
            }
        }

        public bool relacionarHistorialCuestionarioConPaciente(int idCuestionario, int idPaciente)
        {
            if (idCuestionario <= 0 || idPaciente <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                Console.WriteLine("Relacionando historial con paciente");
                var historialCuestionario = db.HistorialesCuestionariosCompletados.FirstOrDefault(x => x.Id == idCuestionario);
                if (historialCuestionario == null) return false;

                var paciente = db.Pacientes.FirstOrDefault(x => x.IdUsuario == idPaciente);
                if (paciente == null) return false;

                paciente.HistorialCuestionarios.Add(historialCuestionario);
                db.SaveChanges();

                return true;
            }
        }

        public CuestionariosInfo? GetCuestionariosInfo(int pacienteId)
        {
            if(pacienteId <= 0) return null;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var paciente = db.Pacientes.Where(x => x.IdUsuario == pacienteId).Include(x => x.HistorialCuestionarios).ThenInclude(x => x.testInfoModels).
                    ThenInclude(x => x.TestQuestionWithAnswers).FirstOrDefault();
                  
                if (paciente == null) return null;

                var cuestionarios = db.Cuestionarios.Include(x => x.Preguntas).ThenInclude(x => x.Respuestas).ToList();
                var cuestionariosCompletados = db.CuestionarioCompletados.Where(x => x.PacienteId == pacienteId).ToList();
                var historialCuestionarios  = paciente.HistorialCuestionarios.ToList();


                historialCuestionarios.ForEach(x => x.testInfoModels = x.testInfoModels.OrderByDescending(x => x.Id).ToList());


                return new CuestionariosInfo
                {
                    Cuestionarios = cuestionarios,
                    cuestionarioCompletados = cuestionariosCompletados,
                    historialCuestionarios = historialCuestionarios
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
                var cuestionariosCompletados = db.CuestionarioCompletados.Where(x => x.PacienteId == pacienteId).ToList();
                if (cuestionariosCompletados == null || cuestionariosCompletados.Count == 0) return true;

                var cuestionariosCompletadosToDelete = new List<CuestionarioCompletado>();
                foreach (var cuestionarioCompletado in cuestionariosCompletados)
                {
                    if (DateTime.Now.Subtract(cuestionarioCompletado.FechaCompletado).TotalDays > 15)
                    {
                        cuestionariosCompletadosToDelete.Add(cuestionarioCompletado);
                    }
                }

                if (cuestionariosCompletadosToDelete.Count > 0)
                {
                    foreach (var cuestionarioCompletado in cuestionariosCompletadosToDelete)
                    {
                        db.CuestionarioCompletados.Remove(cuestionarioCompletado);
                    }
                    db.SaveChanges();
                    return true;
                }
                return false;
            }

        }

        public TestInfoModel? GetTestInfoModel(int idHistoralCuestionario)
        {
            if (idHistoralCuestionario <= 0) return null;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                return db.HistorialesCuestionariosCompletados!.Include(x => x.testInfoModels)!.ThenInclude(x => x.TestQuestionWithAnswers)!.
                    FirstOrDefault(x => x.Id == idHistoralCuestionario)!.testInfoModels.LastOrDefault();
            }
        }


        public bool changeVisibility(int idCuestionario, int idTestInfoModel, bool visible)
        {
            if (idCuestionario <= 0 || idTestInfoModel <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var historialCuestionario = db.HistorialesCuestionariosCompletados.FirstOrDefault(x => x.Id == idCuestionario);
                if (historialCuestionario == null) return false;

                var testInfoModel = historialCuestionario.testInfoModels.FirstOrDefault(x => x.Id == idTestInfoModel);
                if (testInfoModel == null) return false;

                testInfoModel.Visible = visible;
                db.SaveChanges();
                return true;
            }
        }
    }
}








            