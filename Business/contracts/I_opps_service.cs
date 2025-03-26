using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Contracts
{
    public interface IOppsService
    {
        int Add(OppsAdd opps);
        Opps? Get(int id);
        bool Delete(int id);

        int AddSakaNote(SakaNotes note, int userId);
    }
}