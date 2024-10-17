using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Contracts
{
    public interface IEmotionSerrvice
    {
        int AddEmotion(Emotion emocion);

        bool UpdateEmotion(Emotion emocion);

        bool DeleteEmotion(int idEmocion);

        Emotion? GetEmotion(int idEmocion);

        List<Emotion>? GetEmotions();
    }
}
