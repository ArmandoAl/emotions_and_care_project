using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace Business.contracts
{
    public interface IGoalService
    {
        
        int AddLogro(Goal logro);

        List<Goal> GetAllLogros();
        Goal GetLogro(int id);

        bool UpdateLogro(Goal logro);

        bool DeleteLogro(int id);
        int AgregarLogroAPaciente(int idPaciente, int id);
    }
}