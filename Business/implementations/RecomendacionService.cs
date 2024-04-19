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
    public class RecomendacionService : IRecomendacionService
    {
        private readonly IRecomendacionRepository _recomendacionRepository;

        public RecomendacionService(IRecomendacionRepository recomendacionRepository)
        {
            _recomendacionRepository = recomendacionRepository;
        }
        public int Add(Recomendacion recomendacion)
        {
            if (recomendacion == null) return 0;
            return _recomendacionRepository.Add(recomendacion);
        }

        public bool AddRecomendacionCompletada(int idRecomendacion, int idUsuario)
        {
            if (idRecomendacion <= 0 || idUsuario <= 0) return false;
            return _recomendacionRepository.AddRecomendacionCompletada(idRecomendacion, idUsuario);
        }

        public bool Delete(int idRecomendacion)
        {
            if (idRecomendacion <= 0) return false;
            return _recomendacionRepository.Delete(idRecomendacion);
        }

        public Recomendacion? Get(int idRecomendacion)
        {
            if (idRecomendacion <= 0) return null;
            return _recomendacionRepository.Get(idRecomendacion);
        }

        public List<Recomendacion> GetRecomentaciones()
        {
            return _recomendacionRepository.GetRecomentaciones()!;
        }

        public bool Update(Recomendacion recomendacion)
        {
            if (recomendacion == null) return false;
            return _recomendacionRepository.Update(recomendacion);
        }
    }
}
