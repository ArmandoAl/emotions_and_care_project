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
    public class PublicacionService : IPublicacionService
    {
        private readonly IPublicacionRepository _publicacionRepository;

        public PublicacionService(IPublicacionRepository publicacionRepository)
        {
            _publicacionRepository = publicacionRepository;
        }
        public int Add(Publicacion publicacion)
        {
            if(publicacion == null) return 0;
            return _publicacionRepository.Add(publicacion);
        }

        public bool AddComent(int idPublicacion, Comentario comentario)
        {
            if (comentario == null) return false;
            return _publicacionRepository.AddComent(idPublicacion, comentario);
        }

        public bool AddLike(int idPublicacion)
        {
            if (idPublicacion == 0) return false;
            return _publicacionRepository.AddLike(idPublicacion);
        }

        public bool Delete(int idPublicacion)
        {
            if (idPublicacion == 0) return false;
            return _publicacionRepository.Delete(idPublicacion);
        }

        public Publicacion? Get(int idPublicacion)
        {
            if (idPublicacion == 0) return null;
            return _publicacionRepository.Get(idPublicacion);
        }

        public List<Publicacion>? GetPublicaciones()
        {
            return _publicacionRepository.GetPublicaciones();
        }

        public bool RemoveComent(int idPublicacion, int idComentario)
        {
           if (idPublicacion == 0 || idComentario == 0) return false;
            return _publicacionRepository.RemoveComent(idPublicacion, idComentario);
        }

        public bool RemoveLike(int idPublicacion)
        {
            if (idPublicacion == 0) return false;
            return _publicacionRepository.RemoveLike(idPublicacion);
        }

        public bool Update(Publicacion publicacion)
        {
            if (publicacion == null) return false;
            return _publicacionRepository.Update(publicacion);
        }
    }
}
