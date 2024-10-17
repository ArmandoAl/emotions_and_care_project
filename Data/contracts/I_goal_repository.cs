using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace Data.contracts
{
    public interface IGoalRepository
    {
        int AddGoal(Goal goal);

        List<Goal> GetAllGoals();

        Goal GetGoal(int id);

        Goal GetGoal(string nombre);

        bool UpdateGoal(Goal logro);

        bool DeleteGoal(int id);
        int AddGoalPatient(int idPaciente, int id);
    }
}