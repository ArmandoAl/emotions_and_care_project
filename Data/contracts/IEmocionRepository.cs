using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Contracts
{
    public interface IEmocionRepository
    {
        int AddEmocion(Emocion emocion);

        bool UpdateEmocion(Emocion emocion);

        bool DeleteEmocion(int idEmocion);

        Emocion? GetEmocion(int idEmocion);

        List<Emocion>? GetEmociones();
    }
}
