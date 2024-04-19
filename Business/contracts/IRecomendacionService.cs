using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Contracts
{
    public interface IRecomendacionService
    {
        int Add(Recomendacion recomendacion);

        Recomendacion? Get(int idRecomendacion);

        List<Recomendacion>? GetRecomentaciones();

        bool Update(Recomendacion recomendacion);

        bool Delete(int idRecomendacion);

        bool AddRecomendacionCompletada(int idRecomendacion, int idUsuario);
    }
}
