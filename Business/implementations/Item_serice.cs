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

        public int AddFlor(Flower flor)
        {
            return _itemsRepository.AddFlor(flor);
        }

        public int AddSticker(Sticker sticker)
        {
            return _itemsRepository.AddSticker(sticker);
        }

        public List<Flower> GetAllFlores()
        {
            return _itemsRepository.GetAllFlores();
        }

        public List<Sticker> GetAllStickers()
        {
            return _itemsRepository.GetAllStickers();
        }

        public Flower GetFlor(int id)
        {
            return _itemsRepository.GetFlor(id);
        }

        public Sticker GetSticker(int id)
        {
            return _itemsRepository.GetSticker(id);
        }

        List<Flower> IItemsService.GetAllFlores()
        {
            return _itemsRepository.GetAllFlores();
        }

        List<Sticker> IItemsService.GetAllStickers()
        {
            return _itemsRepository.GetAllStickers();
        }

        Flower IItemsService.GetFlor(int id)
        {
            return _itemsRepository.GetFlor(id);
        }

        Sticker IItemsService.GetSticker(int id)
        {
            return _itemsRepository.GetSticker(id);
        }

        public bool putFlowerInInterface(int id, int flowerId)
        {
            return _itemsRepository.putFlowerInInterface(id, flowerId);
        }

        public int AddFlowersToPatient(int idPatient, int indexStart, int indexEnd)
        {
            return _itemsRepository.AddFlowersToPatient(idPatient, indexStart, indexEnd);
        }
    }
}