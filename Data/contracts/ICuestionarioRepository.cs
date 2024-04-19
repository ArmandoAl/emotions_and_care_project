using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Contracts
{
    public interface ICuestionarioRepository
    {
        int AddCuestionario(Cuestionario cuestionario);

        bool UpdateCuestionario(Cuestionario cuestionario);

        bool DeleteCuestionario(int idCuestionario);

        Cuestionario? GetCuestionario(int idCuestionario);

        bool completarCuestionario(int idCuestionario, int idPaciente);

        bool AgregarCuestionarioAPacientes(int idCuestionario);

        int AgregarHistorialCuestionario(int idCuestionario, List<TestQuestionForComplete> respuestas, int pacienteId);

        bool relacionarHistorialCuestionarioConPaciente(int idCuestionario, int idPaciente);

        CuestionariosInfo? GetCuestionariosInfo(int pacienteId);
        bool CanMakeTest(int pacienteId);
        TestInfoModel? GetTestInfoModel(int idHistoralCuestionario);
    }
}
