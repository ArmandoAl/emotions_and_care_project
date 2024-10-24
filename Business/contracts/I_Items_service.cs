using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace Business
{
    public interface IItemsService
    {
        int AddSticker(Sticker sticker);

        int AddFlor(Flower flor);

        Sticker GetSticker(int id);

        Flower GetFlor(int id);

        List<Sticker> GetAllStickers();

        List<Flower> GetAllFlores();

        bool putFlowerInInterface(int id, int flowerId);
    }
}