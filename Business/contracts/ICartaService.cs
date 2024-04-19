using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Contracts
{
    public interface ICartaService
    {
        int Add(Carta carta, int idUsuario, bool isPatient);

        Carta? Get(int idCarta);

        List<Carta>? GetAllByUser(int idUsuario, bool isPatient);

        bool Update(Carta carta);

        bool Delete(int idCarta);

        bool AddRespuesta(RespuestaCarta respuesta, int idCarta);

        List<Carta>? initCarts(int idUsuario);
    }
}
