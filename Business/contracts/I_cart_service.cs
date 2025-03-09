using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Contracts
{
    public interface ICartService
    {
        GoalWithCart? Add(Cart carta, int idUsuario, bool isPatient);

        Cart? Get(int idCarta);

        List<Cart>? GetAllByUser(int idUsuario, bool isPatient);

        bool Update(Cart carta);

        bool Delete(int idCarta);

        GoalWithCartAnswer? AddRespuesta(CartAnswer respuesta, int idCarta);

        List<Cart>? initCarts(int idUsuario);

        GoalWithSticker? getGoalWithSticker(int idUsuario, int idCarta);
    }
}
