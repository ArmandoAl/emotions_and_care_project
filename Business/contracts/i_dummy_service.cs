using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Contracts
{
    public interface IDummyService
    {
        int Add(DummyUser dummyUser);
        DummyUser? Get(int id);
        bool Delete(int id);
        //bool Update(DummyUser dummyUser);
        //List<DummyUser>? GetAll();
    }
}