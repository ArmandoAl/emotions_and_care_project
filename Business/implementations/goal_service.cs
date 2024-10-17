using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.contracts;
using Data.contracts;
using Domain;

namespace Business.implementations
{
    public class LogroService : IGoalService
    {

        private readonly IGoalRepository _logroRepository;

        public LogroService(IGoalRepository logroRepository)
        {
            _logroRepository = logroRepository;
        }

        public int AddLogro(Goal logro)
        {
            return _logroRepository.AddGoal(logro);
        }

        public List<Goal> GetAllLogros()
        {
            return _logroRepository.GetAllGoals();
        }

        public bool UpdateLogro(Goal logro)
        {
            return _logroRepository.UpdateGoal(logro);
        }

        public bool DeleteLogro(int id)
        {
            return _logroRepository.DeleteGoal(id);
        }

        public int AgregarLogroAPaciente(int idPaciente, int id)
        {
            return _logroRepository.AddGoalPatient(idPaciente, id);
        }

        public Goal GetLogro(int id)
        {
            return _logroRepository.GetGoal(id);
        }
    }
}