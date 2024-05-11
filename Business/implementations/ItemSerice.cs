using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.contracts;
using Domain;

namespace Business.implementations
{
    public class ItemsSerice : IItemsService
    {
        private readonly IItemsRepository _itemsRepository;

        public ItemsSerice(IItemsRepository itemsRepository)
        {
            _itemsRepository = itemsRepository;
        }

        public int AddFlor(Flor flor)
        {
            return _itemsRepository.AddFlor(flor);
        }

        public int AddSticker(Sticker sticker)
        {
            return _itemsRepository.AddSticker(sticker);
        }

        public List<Flor> GetAllFlores()
        {
            return _itemsRepository.GetAllFlores();
        }

        public List<Sticker> GetAllStickers()
        {
            return _itemsRepository.GetAllStickers();
        }

        public Flor GetFlor(int id)
        {
            return _itemsRepository.GetFlor(id);
        }

        public Sticker GetSticker(int id)
        {
            return _itemsRepository.GetSticker(id);
        }

        List<Flor> IItemsService.GetAllFlores()
        {
            throw new NotImplementedException();
        }

        List<Sticker> IItemsService.GetAllStickers()
        {
            throw new NotImplementedException();
        }

        Flor IItemsService.GetFlor(int id)
        {
            throw new NotImplementedException();
        }

        Sticker IItemsService.GetSticker(int id)
        {
            throw new NotImplementedException();
        }
    }
}