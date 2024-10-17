using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Contracts
{
    public interface IEmotionRepository
    {
        int AddEmotion(Emotion emotion);

        bool UpdateEmotion(Emotion emotion);

        bool DeleteEmotion(int idEmotion);

        Emotion? GetEmotion(int idEmotion);

        List<Emotion>? GetEmocions();
    }
}
