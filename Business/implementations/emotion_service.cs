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
    public class EmotionService : IEmotionSerrvice
    {
        private readonly IEmotionRepository _emotionService;

        public EmotionService(IEmotionRepository emotionService)
        {
            _emotionService = emotionService;
        }

        public int AddEmotion(Emotion emotion)
        {
            if (emotion == null) return 0;

            return _emotionService.AddEmotion(emotion);

        }

        public bool DeleteEmotion(int idEmotion)
        {
            if (idEmotion <= 0) return false;

            return _emotionService.DeleteEmotion(idEmotion);
        }

        public Emotion? GetEmotion(int idEmotion)
        {
            if (idEmotion <= 0) return null;

            return _emotionService.GetEmotion(idEmotion);
        }

        public List<Emotion>? GetEmotions()
        {
            return _emotionService.GetEmocions();
        }

        public bool UpdateEmotion(Emotion emotion)
        {
            if (emotion == null) return false;

            return _emotionService.UpdateEmotion(emotion);
        }
    }
 }
  