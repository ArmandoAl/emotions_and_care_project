using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Contracts
{
    public interface IPublicacionService
    {
        int Add(Publicacion publicacion);

        Publicacion? Get(int idPublicacion);

        List<Publicacion>? GetPublicaciones();

        bool Update(Publicacion publicacion);

        bool Delete(int idPublicacion);

        bool AddLike(int idPublicacion);

        bool RemoveLike(int idPublicacion);

        bool AddComent(int idPublicacion, Comentario comentario);

        bool RemoveComent(int idPublicacion, int idComentario);
    }
}
