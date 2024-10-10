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
        int Add(Recomandation recomendacion);

        Recomandation? Get(int idRecomendacion);

        List<Recomandation>? GetRecomentaciones();

        bool Update(Recomandation recomendacion);

        bool Delete(int idRecomendacion);

        bool AddRecomendacionCompletada(int idRecomendacion, int idUsuario);
    }
}
