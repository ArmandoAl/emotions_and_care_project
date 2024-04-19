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
    public class EmocionService : IEmocionSerrvice
    {
        private readonly IEmocionRepository _emocionService;

        public EmocionService(IEmocionRepository emocionService)
        {
            _emocionService = emocionService;
        }

        public int AddEmocion(Emocion emocion)
        {
            if (emocion == null) return 0;

            return _emocionService.AddEmocion(emocion);

        }

        public bool DeleteEmocion(int idEmocion)
        {
            if (idEmocion <= 0) return false;

            return _emocionService.DeleteEmocion(idEmocion);
        }

        public Emocion? GetEmocion(int idEmocion)
        {
            if (idEmocion <= 0) return null;

            return _emocionService.GetEmocion(idEmocion);
        }

        public List<Emocion>? GetEmociones()
        {
            return _emocionService.GetEmociones();
        }

        public bool UpdateEmocion(Emocion emocion)
        {
            if (emocion == null) return false;

            return _emocionService.UpdateEmocion(emocion);
        }
    }
 }
  