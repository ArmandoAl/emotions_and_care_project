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
    public class NotaService : INoteService
    {
      private readonly INoteRepository _notaRepository;
      private readonly IGoalRepository _logroRepository;

      private readonly IItemsRepository _itemsRepository;

      public NotaService(INoteRepository notaRepository, IGoalRepository logroRepository, IItemsRepository itemsRepository)
        {
            _notaRepository = notaRepository;
            _logroRepository = logroRepository;
            _itemsRepository = itemsRepository;
        }
       
        public GoalWithNote? AddNota(Note nota, int idPaciente, bool isFirtTime)
        {
            if (nota == null) return null;
    
            var idNota = _notaRepository.AddNote(nota, idPaciente);

            if (idNota > 0)
            {
                if (isFirtTime)
                {
                    var idLogro = _logroRepository.AddGoalPatient(idPaciente, 3);
                    if (idLogro <= 0)
                    {
                        
                        _notaRepository.DeleteNote(idNota, idPaciente);
                        return null;
                    }


                    bool addStickerResult = _itemsRepository.addStickerToPatient(5, idPaciente);

                    if (!addStickerResult)
                    {
                        _notaRepository.DeleteNote(idNota, idPaciente);

                        return null;

                    }


                    return new GoalWithNote 
                    {
                        goal = _logroRepository.GetGoal(idLogro),
                        noteId = idNota
                    };

                } else {
                    return new GoalWithNote
                    {
                        goal = null,
                        noteId = idNota
                    };
                }
            }

            return null;
        }

        public bool DeleteNota(int idNota, int idPaciente)
        {
            if (idNota <= 0) return false;

            return _notaRepository.DeleteNote(idNota, idPaciente);
        }

        public Note? GetNota(int idNota, int idPaciente)
        {
            if (idNota <= 0) return null;

            return _notaRepository.GetNote(idNota, idPaciente);
        }

        public List<Note>? GetNotas(int idPaciente)
        {
            if(idPaciente <= 0) return null;
            return _notaRepository.GetNotesByPaciente(idPaciente);
        }

        public bool UpdateNota(Note nota, int idPaciente)
        {
            if (nota == null) return false;

            return _notaRepository.UpdateNote(nota, idPaciente);
        }

    }

 }