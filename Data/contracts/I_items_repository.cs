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

        int AddFlor(Flower flor);

        Sticker GetSticker(int id);

        Flower GetFlor(int id);

        List<Sticker> GetAllStickers();

        List<Flower> GetAllFlores();

        bool addStickerToPatient(int stickerId, int patientId);

        bool addFlowerToPatient(int flowerId, int patientId);


        bool setFlowerStage(
            int patientId,
            int flowerId, EtapaFlor etapaFlor);

        bool setFlowerPosition(
            int patientId,
            int flowerId, int? position);

        bool setStickerPosition(
             int patientId,
            int stickerId, int? position);

    }
}