using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Contracts
{
    
    public interface ITextService
    {
        int Add(InAppText text);
        InAppText? Get(int id);
        bool Delete(int id);
        bool Update(int id, InAppText text);

        List<InAppText> GetAll();
    }
}