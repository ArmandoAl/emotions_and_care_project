using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Contracts
{
    public interface INoteRepository
    {
        int AddNote(Note note, int idPatient);

        bool UpdateNote(Note note, int idPatient);

        bool DeleteNote(int idNote, int idPatient);

        Note? GetNote(int idNote, int idPatient);

        bool vincularNoteConPaciente(int idNote, int idPaciente);

        bool vincularNoteConEmocion(int idNote, int idEmocion, int idPaciente);

        List<Note>? GetNotesByPaciente(int idPaciente);
        int getTimeWithoutNotes(int idPaciente);
    }
}
