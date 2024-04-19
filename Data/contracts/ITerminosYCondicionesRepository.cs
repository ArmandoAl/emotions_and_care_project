using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Contracts
{
    public interface ITerminosYCondicionesRepository
    {
        int AddTerminosYCondiciones(TerminosYCondiciones terminosYCondiciones);

        bool UpdateTerminosYCondiciones(TerminosYCondiciones terminosYCondiciones);

        bool DeleteTerminosYCondiciones(int idTerminosYCondiciones);

        TerminosYCondiciones? GetTerminosYCondiciones(int idTerminosYCondiciones);
    }
}
