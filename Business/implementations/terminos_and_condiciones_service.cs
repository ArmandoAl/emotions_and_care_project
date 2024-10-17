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
    public class TermsService : ITermsService
    {
        private readonly ITermsAndConditionsRepository _service;

        public TermsService(ITermsAndConditionsRepository service)
        {
            _service = service;
        }

        public int AddTerminosYCondiciones(TermsAndConditions terminosYCondiciones)
        {
            if (terminosYCondiciones == null) { return 0; }
            return _service.AddTerminosYCondiciones(terminosYCondiciones);
        }

        public bool DeleteTerminosYCondiciones(int idTerminosYCondiciones)
        {
            if (idTerminosYCondiciones < 1) { return false; }
            return _service.DeleteTerminosYCondiciones(idTerminosYCondiciones);
        }

        public TermsAndConditions? GetTerminosYCondiciones(int idTerminosYCondiciones)
        {
            if (idTerminosYCondiciones < 1) return null;

            return _service.GetTerminosYCondiciones(idTerminosYCondiciones);
        }

        public bool UpdateTerminosYCondiciones(TermsAndConditions terminosYCondiciones)
        {
            if (terminosYCondiciones == null) { return false; };
            return _service.UpdateTerminosYCondiciones(terminosYCondiciones);
        }

        }

    }