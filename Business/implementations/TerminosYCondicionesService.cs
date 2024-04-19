using Business.Contracts;
using Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Implementations
{
    public class TerminosYCondicionesService : ITerminosYCondicionesService
    {
        private readonly ITerminosYCondicionesRepository _service;

        public TerminosYCondicionesService(ITerminosYCondicionesRepository service)
        {
            _service = service;
        }

        public int AddTerminosYCondiciones(Domain.TerminosYCondiciones terminosYCondiciones)
        {
            if (terminosYCondiciones == null) { return 0; }
            return _service.AddTerminosYCondiciones(terminosYCondiciones);
        }

        public bool DeleteTerminosYCondiciones(int idTerminosYCondiciones)
        {
            if (idTerminosYCondiciones < 1) { return false; }
            return _service.DeleteTerminosYCondiciones(idTerminosYCondiciones);
        }

        public Domain.TerminosYCondiciones? GetTerminosYCondiciones(int idTerminosYCondiciones)
        {
            if (idTerminosYCondiciones < 1) return null;

            return _service.GetTerminosYCondiciones(idTerminosYCondiciones);
        }

        public bool UpdateTerminosYCondiciones(Domain.TerminosYCondiciones terminosYCondiciones)
        {
            if (terminosYCondiciones == null) { return false; };
            return _service.UpdateTerminosYCondiciones(terminosYCondiciones);
        }

        }

    }