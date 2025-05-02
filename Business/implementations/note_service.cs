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

        private readonly IPatientRepository _patientRepository;

        // Constructor

      public NotaService(INoteRepository notaRepository, IGoalRepository logroRepository, IItemsRepository itemsRepository, 
            IPatientRepository patientRepository)
        {
            _notaRepository = notaRepository;
            _logroRepository = logroRepository;
            _itemsRepository = itemsRepository;
            _patientRepository = patientRepository;
        }
       
        public AchievementWithNote? AddNota(Note nota, int idPaciente, bool isFirtTime)
        {
            if (nota == null) return null;
    
            var idNota = _notaRepository.AddNote(nota, idPaciente);

            if (idNota > 0)
            {
                var idLogro = _patientRepository.checkAchievement_Community(idPaciente);

                if (idLogro > 0)
                {
                    return new AchievementWithNote
                    {
                        achievementId = idLogro,
                        noteId = idNota
                    };
                }
                else
                {
                    return new AchievementWithNote
                    {
                        achievementId = 0,
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