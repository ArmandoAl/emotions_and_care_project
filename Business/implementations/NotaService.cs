using Business.Contracts;
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

      public NotaService(INotaRepository notaRepository)
        {
        _notaRepository = notaRepository;
      }

        public int AddNota(Nota nota, int idPaciente)
        {
            if (nota == null) return 0;
    
            var idNota = _notaRepository.AddNota(nota);

            if (idNota > 0)
            {
                var vinculacion = _notaRepository.vincularNotaConPaciente(idNota, idPaciente);
                if (!vinculacion)
                {
                    _notaRepository.DeleteNota(idNota);
                    return 0;
                }

                return idNota;
            }

            return 0;
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