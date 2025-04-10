using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Contracts
{
    
    public interface IDummyRepository
    {
        int Add(DummyUser dummy);
        DummyUser? Get(int id);
        //bool Update(DummyUser dummy);
        bool Delete(int id);
    }
}