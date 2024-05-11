using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace Data.contracts
{
    public interface ILogroRepository
    {
        int AddLogro(Logro logro);

        List<Logro> GetAllLogros();

        Logro GetLogro(int id);

        Logro GetLogro(string nombre);

        bool UpdateLogro(Logro logro);

        bool DeleteLogro(int id);
        int AgregarLogroAPaciente(int idPaciente, int id);
    }
}