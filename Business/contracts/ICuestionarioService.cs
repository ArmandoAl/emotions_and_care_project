using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Contracts
{
    public interface ICuestionarioService
    {
        int AddCuestionario(Cuestionario cuestionario);

        bool UpdateCuestionario(Cuestionario cuestionario);

        bool DeleteCuestionario(int idCuestionario);

        Cuestionario? GetCuestionario(int idCuestionario);

        LogroWithTestInfoModel? completarCuestionario(int idCuestionario, int idPaciente,
                List<TestQuestionForComplete> respuestas, bool isFirstTime);
            

        CuestionariosInfo? GetCuestionariosInfo(int pacienteId);

        bool changeVisibility(int idCuestionario, int idTestInfoModel, bool visible);
    }
}
