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
        GoalWithNote? AddNota(Note nota, int idPaciente, bool isFirtTime);

        bool UpdateNota(Note nota);

        bool DeleteNota(int idNota);

        Note? GetNota(int idNota);

        List<Note>? GetNotas(int idPaciente);
    }
}
