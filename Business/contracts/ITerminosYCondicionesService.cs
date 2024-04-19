using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Contracts
{
    public interface ITerminosYCondicionesService
    {
        int AddTerminosYCondiciones(TerminosYCondiciones terminosYCondiciones);

        bool UpdateTerminosYCondiciones(TerminosYCondiciones terminosYCondiciones);

        bool DeleteTerminosYCondiciones(int idTerminosYCondiciones);

        TerminosYCondiciones? GetTerminosYCondiciones(int idTerminosYCondiciones);
    }
}
