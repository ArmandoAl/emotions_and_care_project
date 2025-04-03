using Business.Contracts;
using Data.Contracts;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Implementations
{
    
    public class TextService : ITextService
    {
        private readonly ITextRepository _textRepository;

        public TextService(ITextRepository textRepository)
        {
            _textRepository = textRepository;
        }

        public int Add(InAppText text)
        {
            if (text == null) { return 0; }
            return _textRepository.Add(text);
        }
        public bool Delete(int id)
        {
            if (id < 1) { return false; }
            return _textRepository.Delete(id);
        }
        public InAppText? Get(int id)
        {
            if (id < 1) { return null; }
            return _textRepository.Get(id);
        }
        public bool Update(int id, InAppText text)
        {
            if (id < 1) { return false; }
            if (text == null) { return false; }
            text.textId = id;
            return _textRepository.Update(id, text);
        }
        public List<InAppText> GetAll()
        {
            return _textRepository.GetAll();
        }
    }
}