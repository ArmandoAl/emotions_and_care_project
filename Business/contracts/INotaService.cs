using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Contracts
{
    public interface INotaService
    {
        LogroWithNota? AddNota(Nota nota, int idPaciente, bool isFirtTime);

        bool UpdateNota(Nota nota);

        bool DeleteNota(int idNota);

        Nota? GetNota(int idNota);

        List<Nota>? GetNotas(int idPaciente);
    }
}
