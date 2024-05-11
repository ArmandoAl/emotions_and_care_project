using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.contracts;
using Data.contracts;
using Domain;

namespace Business.implementations
{
    public class LogroService : ILogroService
    {

        private readonly ILogroRepository _logroRepository;

        public LogroService(ILogroRepository logroRepository)
        {
            _logroRepository = logroRepository;
        }

        public int AddLogro(Logro logro)
        {
            return _logroRepository.AddLogro(logro);
        }

        public List<Logro> GetAllLogros()
        {
            return _logroRepository.GetAllLogros();
        }

        public bool UpdateLogro(Logro logro)
        {
            return _logroRepository.UpdateLogro(logro);
        }

        public bool DeleteLogro(int id)
        {
            return _logroRepository.DeleteLogro(id);
        }

        public int AgregarLogroAPaciente(int idPaciente, int id)
        {
            return _logroRepository.AgregarLogroAPaciente(idPaciente, id);
        }

        public Logro GetLogro(int id)
        {
            return _logroRepository.GetLogro(id);
        }
    }
}