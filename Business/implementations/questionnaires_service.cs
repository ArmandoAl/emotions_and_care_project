using Business.contracts;
using Business.Contracts;
using Data.contracts;
using Data.Contracts;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Implementations
{
    public class CuestionarioService : IQuestionnaireService
    {
        private readonly IQuestionnaireRepository _cuestionarioService;
        private readonly INotificationRepository _notificacionService;
        private readonly IGoalRepository _logroService;

        private readonly IItemsRepository _itemsRepository;

        public CuestionarioService(IQuestionnaireRepository cuestionarioService, INotificationRepository notificacionService, IGoalRepository logroService, IItemsRepository itemsRepository)
        {
            _cuestionarioService = cuestionarioService;
            _notificacionService = notificacionService;
            _logroService = logroService;
            _itemsRepository = itemsRepository;
        }
        public int AddCuestionario(Questionnaire cuestionario)
        {
            if (cuestionario == null) return 0;

            var idCuestionario = _cuestionarioService.AddQuestionnaire(cuestionario);

            if (idCuestionario > 0)
            {
               var result = _cuestionarioService.AgregarQuestionnaireAPacientes(idCuestionario);
               if (result) return idCuestionario;
            }

            return 0;
        }

        public GoalWithTestInfoModel? completarCuestionario(int idCuestionario, int idPaciente, List<TestQuestionForComplete> respuestas, bool isFirstTime)
        {
           if (idCuestionario <= 0 || idPaciente <= 0) return null;

            var result = _cuestionarioService.completarQuestionnaire(idCuestionario, idPaciente);

            if (result)
            {
                var idHistoralCuestionario = _cuestionarioService.AgregarHistorialQuestionnaire(idCuestionario, respuestas, idPaciente);

                if (idHistoralCuestionario >= 0)
                {
                
                    if(isFirstTime) {
                        var idLogro = _logroService.AddGoalPatient(idPaciente, 5);

                        bool addStickerResult = _itemsRepository.addStickerToPatient(6, idPaciente);


                        if (addStickerResult == false) return null;

                        if (idLogro <= 0) return null;

                        return new GoalWithTestInfoModel
                        {
                        TestInfoModel = _cuestionarioService.GetTestInfoModel(
                            idPaciente,
                            idCuestionario,
                            idHistoralCuestionario)!,

                        Logro = _logroService.GetGoal(idLogro)
                        };
                    } else {

                        return new GoalWithTestInfoModel
                        {
                            TestInfoModel = _cuestionarioService.GetTestInfoModel(
                                idPaciente,
                                idCuestionario,
                                idHistoralCuestionario
                                    
                                )!,

                            Logro = null
                        };

                    }   
                }
            }

            return null;
        }

        public bool DeleteCuestionario(int idCuestionario, 
            int idPaciente
        )
        {
            if (idCuestionario <= 0) return false;

            return _cuestionarioService.DeleteQuestionnaire(idCuestionario, idPaciente);
        }

        public Questionnaire? GetCuestionario(int idCuestionario)
        {
            if (idCuestionario <= 0) return null;

            return _cuestionarioService.GetQuestionnaire(idCuestionario);
        }

        public QuestionnairesInfo? GetCuestionariosInfo(int pacienteId)
        {
            if(pacienteId <= 0) return null;
               
            QuestionnairesInfo cuestionariosInfo = _cuestionarioService.GetQuestionnairesInfo(pacienteId)!;

            if (cuestionariosInfo != null)
            {
                bool canMakeTest = _cuestionarioService.CanMakeTest(pacienteId);

                if (canMakeTest)
                {
                  int id =  _notificacionService.AddNotification(new NotificationModel
                    {
                        Titulo = "Cuestionarios",
                        Descripcion = "Tienes un cuestionario disponible para completar",
                        notificationType = NotificationType.ReminderNotification,
                    });


                    if (id > 0)
                    {
                        _notificacionService.vincularNotificationConPaciente(id, pacienteId);

                    }
                    
                    
                }

                return cuestionariosInfo;
            }

            return null;
        }

        public bool UpdateCuestionario(Questionnaire cuestionario)
        {
            if (cuestionario == null) return false;

            return _cuestionarioService.UpdateQuestionnaire(cuestionario);
        }

        public bool changeVisibility(
            int patientId,
            int idCuestionario,
      
         int idTestInfoModel, bool visible)
        {
            if (idCuestionario <= 0 || idTestInfoModel <= 0) return false;

            return _cuestionarioService.changeVisibility(
                patientId,
                idCuestionario, idTestInfoModel, visible);

        }
    }
}
