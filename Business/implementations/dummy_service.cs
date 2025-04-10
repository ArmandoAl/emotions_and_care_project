using Business.Contracts;
using Data.contracts;
using Data.Contracts;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Implementations
{
    
    public class DummyService : IDummyService
    {
        private readonly IDummyRepository _dummyRepository;

        public DummyService(IDummyRepository dummyRepository)
        {
            _dummyRepository = dummyRepository;
        }

        public int Add(DummyUser dummyUser)
        {
            if (dummyUser == null) return 0;
            return _dummyRepository.Add(dummyUser);
        }
        public DummyUser? Get(int id)
        {
            if (id <= 0) return null;
            return _dummyRepository.Get(id);
        }
        public bool Delete(int id)
        {
            if (id <= 0) return false;
            return _dummyRepository.Delete(id);
        }
    }
}