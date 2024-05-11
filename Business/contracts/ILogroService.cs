using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace Business.contracts
{
    public interface ILogroService
    {
        

        int AddLogro(Logro logro);

        List<Logro> GetAllLogros();
        Logro GetLogro(int id);

        bool UpdateLogro(Logro logro);

        bool DeleteLogro(int id);
        int AgregarLogroAPaciente(int idPaciente, int id);
    }
}