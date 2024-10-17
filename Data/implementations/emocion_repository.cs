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
    public class EmotionRepository : IEmotionRepository
    {
        public int AddEmotion(Emotion emotion)
        {
            if (emotion == null) return 0;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                db.emotions.Add(emotion);
                db.SaveChanges();
                return emotion.emotionId;
            }
        }

        
        public bool DeleteEmotion(int idEmotion)
        {
            if (idEmotion <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var emocion = db.emotions.FirstOrDefault(x => x.emotionId == idEmotion);
                if (emocion == null) return false;

                db.emotions.Remove(emocion);
                db.SaveChanges();
                return true;
            }
        }

        public Emotion? GetEmotion(int idEmotion)
        {
            if (idEmotion <= 0) return null;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                return db.emotions.FirstOrDefault(x => x.emotionId == idEmotion);
            }
        }

        public List<Emotion> GetEmocions()
        {
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                return db.emotions.ToList();
            }
        }

        public bool UpdateEmotion(Emotion emotion)
        {
            if (emotion == null) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var thisEmocion = db.emotions.FirstOrDefault(x => x.emotionId == emotion.emotionId);
                if (thisEmocion == null) return false;

                db.emotions.Update(emotion);
                db.SaveChanges();
                return true;
            }
        }

        public List<Emotion>? GetEmocionesByPaciente(int idPaciente)
        {
            if (idPaciente <= 0) return null;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                return db.emotions.ToList();
            }
        }

        }
    }


        