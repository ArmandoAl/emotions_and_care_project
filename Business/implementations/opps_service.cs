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
    public class OppsService : IOppsService
    {
        private readonly IOppsRepository _service;

        public OppsService(IOppsRepository service)
        {
            _service = service;
        }

        public int Add(OppsAdd opps)
        {
            if(opps == null) { return 0; }
            return _service.Add(opps);
        }

        public bool Delete(int id)
        {
            if(id < 1) { return false; }
            return _service.Delete(id);
        }

        public Opps? Get(int id)
        {
            if(id < 1) { return null; }
            return _service.Get(id);
        }

        public int AddSakaNote(SakaNotes note, int userId)
        {
            if(note == null || userId < 1) { return 0; }
            return _service.AddSakaNote(note, userId);
        }
    }
}