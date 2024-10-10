using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Contracts
{
    public interface ITermsAndConditionsRepository
    {
        int AddTerminosYCondiciones(TermsAndConditions terminosYCondiciones);

        bool UpdateTerminosYCondiciones(TermsAndConditions terminosYCondiciones);

        bool DeleteTerminosYCondiciones(int idTerminosYCondiciones);

        TermsAndConditions? GetTerminosYCondiciones(int idTerminosYCondiciones);
    }
}
