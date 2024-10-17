using Data.Contracts;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Implementations
{
    public class NoteRepository : INoteRepository
    {
        
        public int AddNote(Note note, int idPatient)
        {
            if (note == null) return 0;
            note.dateCreated = DateTime.Now;
            note.modifiedDate = DateTime.Now;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                note.emotion = db.emotions!.FirstOrDefault(x => x.emotionId! == note.emotion.emotionId)!;

                Console.WriteLine(note.emotion.emotionId);

                var patient = db.patients.Where(x => x.userId == idPatient).Include(x => x.diary).FirstOrDefault();

                if (patient == null) return 0;

                Console.WriteLine("Paciente encontrado");

                var dairy = patient.diary;

                if (dairy == null) return 0;

                Console.WriteLine("Diario encontrado:" + dairy.diaryId);

                dairy.notes.Add(note);
                db.SaveChanges();
                return note.noteId;
            }
        }

        public bool DeleteNote(int idNote, int idPatient)
        {
            if (idNote <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var patient = db.patients.FirstOrDefault(x => x.userId == idPatient);

                if (patient == null) return false;

                var dairy = patient.diary;

                if (dairy == null) return false;

                var note = dairy.notes.FirstOrDefault(x => x.noteId == idNote);

                if (note == null) return false;

                dairy.notes.Remove(note);
                db.SaveChanges();
                return true;
            }
        }

        public Note? GetNote(int idNote, int idPatient)
        {
            if (idNote <= 0) return null;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var patient = db.patients.FirstOrDefault(x => x.userId == idPatient);

                if (patient == null) return null;

                var dairy = patient.diary;

                if (dairy == null) return null;

                return dairy.notes.FirstOrDefault(x => x.noteId == idNote);
            }
        }

        public List<Note>? GetNotesByPaciente(int idPaciente)
        {
            if(idPaciente <= 0) return null;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;


            List<Note> notes = new List<Note>();
            using (var db = new DBContext(options: connectionOptions))
            {
                var patient = db.patients.FirstOrDefault(x => x.userId == idPaciente);

                if (patient == null) return null;

                notes = patient.diary.notes.ToList();

                //ordenar de mas reciente a mas antigua
                notes = notes.OrderByDescending(x => x.dateCreated).ToList();

                return notes;
            }
        }

        public int getTimeWithoutNotes(int idPaciente)
        {
            if (idPaciente <= 0) return 0;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;

            using (var db = new DBContext(options: connectionOptions))
            {

                var patient = db.patients.FirstOrDefault(x => x.userId == idPaciente);

                if (patient == null) return 0;

                var notes = patient.diary.notes;

                if (notes.Count == 0) return 0;

                var lastNote = notes.OrderByDescending(x => x.dateCreated).FirstOrDefault();

                var days = (DateTime.Now - lastNote!.dateCreated).Days;

                return days;
            }
        }

        public bool UpdateNote(Note note, int idPatient)
        {
            if (note == null) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var patient = db.patients.FirstOrDefault(x => x.userId == idPatient);

                if (patient == null) return false;

                var dairy = patient.diary;

                if (dairy == null) return false;

                var thisNote = dairy.notes.FirstOrDefault(x => x.noteId == note.noteId);   
                if (thisNote == null) return false;

                thisNote.title = note.title;
                thisNote.content = note.content;
                thisNote.modifiedDate = DateTime.Now;
                thisNote.emotion = db.emotions!.FirstOrDefault(x => x.emotionId! == note.emotion.emotionId)!;

                dairy.notes.Remove(thisNote);

                dairy.notes.Add(thisNote);

                db.SaveChanges();
                return true;
            }
        }

        public bool vincularNoteConEmocion(int idNote, int idEmocion, int idPaciente)
        {
            if (idNote <= 0 || idEmocion <= 0) return false;
            
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;

            using (var db = new DBContext(options: connectionOptions))
            {

                var patient = db.patients.FirstOrDefault(x => x.userId == idPaciente);

                if (patient == null) return false;

                var dairy = patient.diary;


                var note = dairy.notes.FirstOrDefault(x => x.noteId == idNote);
                if (note == null) return false;

                var emocion = db.emotions.FirstOrDefault(x => x.emotionId == idEmocion);
                if (emocion == null) return false;

                note.emotion = emocion;
                
                dairy.notes.Remove(note);

                dairy.notes.Add(note);

                db.SaveChanges();
                return true;
            }

        }

        public bool vincularNoteConPaciente(int idNote, int idPaciente)
        {
            if (idNote <= 0 || idPaciente <= 0) return false;
            
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;

            using (var db = new DBContext(options: connectionOptions))
            {

                var patient = db.patients.FirstOrDefault(x => x.userId == idPaciente);

                if (patient == null) return false;

                var dairy = patient.diary;


                var note = dairy.notes.FirstOrDefault(x => x.noteId == idNote);
                if (note == null) return false;

               
                dairy.notes.Remove(note);
                dairy.notes.Add(note);
                
                db.SaveChanges();
                return true;
            }
          }
        }
    }
        
        

