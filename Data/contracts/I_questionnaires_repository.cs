using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Contracts
{
    public interface IQuestionnaireRepository
    {
        int AddQuestionnaire(Questionnaire Questionnaire);

        bool UpdateQuestionnaire(Questionnaire Questionnaire);

        bool DeleteQuestionnaire(int idCuestionario, int idPaciente);

        Questionnaire? GetQuestionnaire(int idQuestionnaire);

        bool completarQuestionnaire(int idQuestionnaire, int idPaciente);

        bool AgregarQuestionnaireAPacientes(int idQuestionnaire);

       int AgregarHistorialQuestionnaire(int idCuestionario, List<TestQuestionForComplete> respuestas, int pacienteId);

        // bool relacionarHistorialQuestionnaireConPaciente(int idQuestionnaire, int idPaciente);

        QuestionnairesInfo? GetQuestionnairesInfo(int pacienteId);
        bool CanMakeTest(int pacienteId);
       TestInfoModel? GetTestInfoModel(int patientId,
            int idHistoralCuestionario, int idTestInfoModel);

        bool changeVisibility(
            int patientId,
            int idCuestionario, int idTestInfoModel, bool visible);
    }
}
