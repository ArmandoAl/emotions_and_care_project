using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace Data.contracts
{
    public interface IItemsRepository
    {
        int AddSticker(Sticker sticker);

        int AddFlor(Flor flor);

        Sticker GetSticker(int id);

        Flor GetFlor(int id);

        List<Sticker> GetAllStickers();

        List<Flor> GetAllFlores();
    }
}