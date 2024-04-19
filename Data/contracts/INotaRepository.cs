using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Contracts
{
    public interface INotaRepository
    {
        int AddNota(Nota nota);

        bool UpdateNota(Nota nota);

        bool DeleteNota(int idNota);

        Nota? GetNota(int idNota);

        bool vincularNotaConPaciente(int idNota, int idPaciente);

        bool vincularNotaConEmocion(int idNota, int idEmocion);

        List<Nota>? GetNotasByPaciente(int idPaciente);
        int getTimeWithoutNotes(int idPaciente);
    }
}
