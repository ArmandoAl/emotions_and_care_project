using Business.Contracts;
using Data.contracts;
using Data.Contracts;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Implementations
{
    public class NotaService : INotaService
    {
      private readonly INotaRepository _notaRepository;
      private readonly ILogroRepository _logroRepository;

      public NotaService(INotaRepository notaRepository, ILogroRepository logroRepository)
        {
            _notaRepository = notaRepository;
            _logroRepository = logroRepository;
        }
       
        public LogroWithNota? AddNota(Nota nota, int idPaciente, bool isFirtTime)
        {
            if (nota == null) return null;
    
            var idNota = _notaRepository.AddNota(nota);

            if (idNota > 0)
            {
                var vinculacion = _notaRepository.vincularNotaConPaciente(idNota, idPaciente);
                if (!vinculacion)
                {
                    _notaRepository.DeleteNota(idNota);
                    return null;
                }

                if (isFirtTime)
                {
                    var idLogro = _logroRepository.AgregarLogroAPaciente(idPaciente, 3);
                    if (idLogro <= 0)
                    {
                        _notaRepository.DeleteNota(idNota);
                        return null;
                    }

                    return new LogroWithNota
                    {
                        Logro = _logroRepository.GetLogro(idLogro),
                        IdNota = idNota
                    };

                } else {
                    return new LogroWithNota
                    {
                        Logro = null,
                        IdNota = idNota
                    };
                }
            }

            return null;
        }

        public bool DeleteNota(int idNota)
        {
            if (idNota <= 0) return false;

            return _notaRepository.DeleteNota(idNota);
        }

        public Nota? GetNota(int idNota)
        {
            if (idNota <= 0) return null;

            return _notaRepository.GetNota(idNota);
        }

        public List<Nota>? GetNotas(int idPaciente)
        {
            if(idPaciente <= 0) return null;
            return _notaRepository.GetNotasByPaciente(idPaciente);
        }

        public bool UpdateNota(Domain.Nota nota)
        {
            if (nota == null) return false;

            return _notaRepository.UpdateNota(nota);
        }

    }

 }