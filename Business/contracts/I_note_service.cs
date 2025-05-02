using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Contracts
{
    public interface INoteService
    {
        AchievementWithNote? AddNota(Note nota, int idPaciente, bool isFirtTime);

        bool UpdateNota(Note nota, int idPaciente);

        bool DeleteNota(int idNota, int idPaciente);

        Note? GetNota(int idNota, int idPaciente);

        List<Note>? GetNotas(int idPaciente);
    }
}
