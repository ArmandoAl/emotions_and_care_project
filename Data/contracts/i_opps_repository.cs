using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Contracts
{
    public interface IOppsRepository
    {
        int Add(OppsAdd opps);
        Opps? Get(int id);
        bool Delete(int id);

        int AddSakaNote(SakaNotes note, int userId);
    }
}