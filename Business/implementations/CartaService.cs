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
    public class CartaService : ICartaService
    {
        private readonly ICartaRepository _cartaRepository;

        public CartaService(ICartaRepository cartaRepository)
        {
            _cartaRepository = cartaRepository;
        }

        public int Add(Carta carta, int idUsuario, bool isPatient)
        {
            if(carta == null || idUsuario <= 0) return 0;

            int idCarta = _cartaRepository.Add(carta);

            if (idCarta > 0)
            {
               bool vinculacion = _cartaRepository.
                    vincularCartConUsuario(idCarta, idUsuario, isPatient);

                if (vinculacion == false)
                {
                     _cartaRepository.Delete(idCarta);
                     return 0;
                }
                return idCarta;
            }
            return 0;
        }

        public bool AddRespuesta(RespuestaCarta respuesta, int idCarta)
        {
            if (respuesta == null || idCarta <= 0) return false;
            return _cartaRepository.AddRespuesta(respuesta, idCarta);
        }

        public bool Delete(int idCarta)
        {
            if (idCarta <= 0) return false;
            return _cartaRepository.Delete(idCarta);
        }

        public Carta? Get(int idCarta)
        {
            if (idCarta <= 0) return null;
            return _cartaRepository.Get(idCarta);
        }

        public List<Carta>? GetAllByUser(int idUsuario, bool isPatient)
        {
            if (idUsuario <= 0) return null;
            return _cartaRepository.GetAllByUser(idUsuario, isPatient);
        }
        public bool Update(Carta carta)
        {
            if (carta == null) return false;
            return _cartaRepository.Update(carta);
        }

        public List<Carta>? initCarts(int idUsuario)
        {
            bool init = _cartaRepository.initCarts();
            
            if (init == false) return null;

            return _cartaRepository.GetNotExpiredCarts(idUsuario);
        }
    }
}
