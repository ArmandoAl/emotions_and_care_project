using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Contracts
{
    public interface IQuestionnaireService
    {
        int AddCuestionario(Questionnaire cuestionario);

        bool UpdateCuestionario(Questionnaire cuestionario);

        bool DeleteCuestionario(int idCuestionario, 
            int idPaciente
        );

        Questionnaire? GetCuestionario(int idCuestionario);

        AchievementWithTestInfoModel? completarCuestionario(int idCuestionario, int idPaciente,
                List<TestQuestionForComplete> respuestas, bool isFirstTime);
            

        QuestionnairesInfo? GetCuestionariosInfo(int pacienteId);

       bool changeVisibility(
            int patientId,
            int idCuestionario,
      
         int idTestInfoModel, bool visible);
    }
}
