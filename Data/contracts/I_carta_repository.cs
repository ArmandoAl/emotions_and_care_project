using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Contracts
{
    public interface ICartRepository
    {
        int Add(Cart carta);

        Cart? Get(int idCarta);

        List<Cart>? GetAllByUser(int idUsuario, bool isPatient);

        List<Cart> GetNotExpiredCarts(int idUsuario);

        bool Update(Cart carta);

        bool Delete(int idCarta);

        bool AddRespuesta(CartAnswer respuesta, int idCarta);

        bool vincularCartConUsuario(int idCarta, int idUsuario, bool isPatient);

        bool initCarts();
        bool thereCartsForOpen(int idPaciente);
    }
}
