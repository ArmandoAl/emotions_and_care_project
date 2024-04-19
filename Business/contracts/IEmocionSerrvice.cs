using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Contracts
{
    public interface IEmocionSerrvice
    {
        int AddEmocion(Emocion emocion);

        bool UpdateEmocion(Emocion emocion);

        bool DeleteEmocion(int idEmocion);

        Emocion? GetEmocion(int idEmocion);

        List<Emocion>? GetEmociones();
    }
}
