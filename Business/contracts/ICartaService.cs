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
        LogroWithCarta? Add(Carta carta, int idUsuario, bool isPatient, bool isFirtTime);

        Carta? Get(int idCarta);

        List<Carta>? GetAllByUser(int idUsuario, bool isPatient);

        bool Update(Carta carta);

        bool Delete(int idCarta);

        LogroWithRespuestaCarta? AddRespuesta(RespuestaCarta respuesta, int idCarta, bool isFirtTime);

        List<Carta>? initCarts(int idUsuario);
    }
}
