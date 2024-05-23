using Business.Contracts;
using Data.contracts;
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
        private readonly ILogroRepository _logroRepository;

        public CartaService(ICartaRepository cartaRepository, ILogroRepository logroRepository)
        {
            _cartaRepository = cartaRepository;
            _logroRepository = logroRepository;
        }

        public LogroWithCarta? Add(Carta carta, int idUsuario, bool isPatient, bool isFirtTime)
        {
            if(carta == null || idUsuario <= 0) return null;

            int idCarta = _cartaRepository.Add(carta);

            if (idCarta > 0)
            {
               bool vinculacion = _cartaRepository.
                    vincularCartConUsuario(idCarta, idUsuario, isPatient);

                if (vinculacion == false)
                {
                     _cartaRepository.Delete(idCarta);
                     return null;
                }

                if(isFirtTime)
                {
                    var idLogro = _logroRepository.AgregarLogroAPaciente(idUsuario, 5);
                    
                    return new LogroWithCarta
                    {
                        Logro = _logroRepository.GetLogro(idLogro),
                        IdCarta = idCarta
                    };
                } else {
                    
                    return new LogroWithCarta
                    {
                        Logro = null,
                        IdCarta = idCarta
                    };
                }
            }
            return null;
        }

        public LogroWithRespuestaCarta? AddRespuesta(RespuestaCarta respuesta, int idCarta, bool isFirtTime)
        {
            if (respuesta == null || idCarta <= 0) return null;
            bool res = _cartaRepository.AddRespuesta(respuesta, idCarta);

            if (res)
            {

                if(isFirtTime) {
                var idLogro = _logroRepository.AgregarLogroAPaciente(respuesta.IdReceptor, 6);

                if (idLogro <= 0)
                {
                    return null;
                }

                return new LogroWithRespuestaCarta
                {
                    Logro = _logroRepository.GetLogro(idLogro),
                    IdRespuestaCarta = respuesta.IdCarta
                };

                } else {
                    return new LogroWithRespuestaCarta
                    {
                        Logro = null,
                        IdRespuestaCarta = respuesta.IdCarta
                    };
                }
            }
            return null;
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
