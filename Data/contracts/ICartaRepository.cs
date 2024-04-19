using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Contracts
{
    public interface ICartaRepository
    {
        int Add(Carta carta);

        Carta? Get(int idCarta);

        List<Carta>? GetAllByUser(int idUsuario, bool isPatient);

        List<Carta> GetNotExpiredCarts(int idUsuario);

        bool Update(Carta carta);

        bool Delete(int idCarta);

        bool AddRespuesta(RespuestaCarta respuesta, int idCarta);

        bool vincularCartConUsuario(int idCarta, int idUsuario, bool isPatient);

        bool initCarts();
        bool thereCartsForOpen(int idPaciente);
    }
}
