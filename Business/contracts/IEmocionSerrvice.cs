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
        int AddEmocion(Emotion emocion);

        bool UpdateEmocion(Emotion emocion);

        bool DeleteEmocion(int idEmocion);

        Emotion? GetEmocion(int idEmocion);

        List<Emotion>? GetEmociones();
    }
}
