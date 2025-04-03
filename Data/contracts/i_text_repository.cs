using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Contracts
{
    
    public interface ITextRepository
    {
        int Add(InAppText text);
        InAppText? Get(int id);
        bool Delete(int id);
        bool Update(int id, InAppText text);

        List<InAppText> GetAll();
    }
}