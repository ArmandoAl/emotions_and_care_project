using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Contracts
{
    public interface IRecomendationService
    {
        int Add(Recomendation recomendacion);

        Recomendation? Get(int idRecomendacion);

        List<Recomendation>? GetRecomentaciones();

        bool Update(Recomendation recomendacion);

        bool Delete(int idRecomendacion);

        bool recomendationCompleted(int idRecomendation, int idUsuario);

        // bool AddRecomendacionCompletada(int idRecomendacion, int idUsuario);
    }
}
