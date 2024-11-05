using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Contracts
{
    public interface IRecomendationRepository
    {
        int Add(Recomendation recomendation);

        Recomendation? Get(int idRecomendation);

        List<Recomendation>? GetRecomentaciones();

        bool Update(Recomendation recomendation);

        bool Delete(int idRecomendation);

        bool recomendationCompleted(int idRecomendation, int idUsuario);

        // bool AddRecomendationCompletada(int idRecomendation, int idUsuario);
    }
}
