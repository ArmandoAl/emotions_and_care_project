using Data.contracts;
using Data.Contracts;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Implementations
{
    public class ItemsRepository : IItemsRepository
    {
        public int AddFlor(Flower flor)
        {
            if(flor == null) return 0;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                        .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                        .Options;

             using (var db = new DBContext(options: connectionOptions))
            {
                db.flowers.Add(flor);
                db.SaveChanges();
                return flor.flowerId;
            }
        }

        public int AddSticker(Sticker sticker)
        {
            if(sticker == null) return 0;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                        .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                        .Options;

             using (var db = new DBContext(options: connectionOptions))
            {
                db.stickers.Add(sticker);
                db.SaveChanges();
                return sticker.stickerId;
            }
        }

        public List<Flower> GetAllFlores()
        {
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                        .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                        .Options;

            using (var db = new DBContext(options: connectionOptions))
            {
                return db.flowers.ToList();
            }
        }

        public List<Sticker> GetAllStickers()
        {
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                        .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                        .Options;

            using (var db = new DBContext(options: connectionOptions))
            {
                return db.stickers.ToList();
            }
        }

        public Flower GetFlor(int id)
        {
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                        .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                        .Options;

            using (var db = new DBContext(options: connectionOptions))
            {
                return db.flowers.Find(id)!;
            }
        }

        public Sticker GetSticker(int id)
        {
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                        .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                        .Options;

            using (var db = new DBContext(options: connectionOptions))
            {
                return db.stickers.Find(id)!;
            }

        }


        public bool addFlowerToPatient(int flowerId, int patientId) {
            if(flowerId <= 0 || patientId <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                        .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                        .Options;

            using (var db = new DBContext(options: connectionOptions))
            {
                var patient = db.patients.FirstOrDefault(x => x.userId == patientId);

                if(patient == null) return false;

                var flower = db.flowers.FirstOrDefault(x => x.flowerId == flowerId);

                if(flower == null) return false;

                patient.userInterface.userFlowers.Add(new UserFlower{
                    flower = flower,
                    position = null,
                    state = 0
                });

                db.SaveChanges();

                return true;

                }
            }



        public bool addStickerToPatient(int stickerId, int patientId) {
            if(stickerId <= 0 || patientId <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()

                        .UseSqlServer(Data.Helpers.Constants.ConnectionString)

                        .Options;

            using (var db = new DBContext(options: connectionOptions))
            {
                var patient = db.patients
                    .Where(x => x.userId == patientId)
                    .Include(x => x.userInterface)
                    .ThenInclude(ui => ui.userStickers)
                    .FirstOrDefault();

                if (patient == null) return false;

                var sticker = db.stickers.FirstOrDefault(x => x.stickerId == stickerId);

                if (sticker == null) return false;

                // Check if the user already has this sticker
                var hasSticker = patient.userInterface.userStickers
                    .Any(us => us.sticker.stickerId == stickerId);

                if (hasSticker) {
                    // Increment the timesEarned if the sticker is already present
                    var userSticker = patient.userInterface.userStickers
                        .FirstOrDefault(us => us.sticker.stickerId == stickerId);
                    if (userSticker != null) {
                        userSticker.timesEarned++;
                    }
                    db.SaveChanges();
                    return true;
                }

                // Add the sticker if not already present
                patient.userInterface.userStickers.Add(new UserSticker
                {
                    sticker = sticker,
                    position = null,
                    timesEarned = 1 // Set to 1 when the sticker is given for the first time
                });

                db.SaveChanges();

                return true;
            }
    
        }


        
        public bool setFlowerPosition(
             int patientId,
            int flowerId, int? position) {
            if(flowerId <= 0 || position <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                        .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                        .Options;

            using (var db = new DBContext(options: connectionOptions))
            {

                var patient = db.patients.FirstOrDefault(x => x.userId == patientId);

                if(patient == null) return false;

                var userFlower = patient.userInterface.userFlowers.FirstOrDefault(x => x.flower.flowerId == flowerId);

                if(userFlower == null) return false;

                userFlower.position = position;


                db.SaveChanges();

                return true;
            }

            }


        public bool setFlowerStage(
             int patientId,
            int flowerId, EtapaFlor etapaFlor) {

            if(flowerId <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                        .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                        .Options;

            using (var db = new DBContext(options: connectionOptions))
            {

                var patient = db.patients.FirstOrDefault(x => x.userId == patientId);

                if(patient == null) return false;

                var userFlower = patient.userInterface.userFlowers.FirstOrDefault(x => x.flower.flowerId == flowerId);

                if(userFlower == null) return false;

                userFlower.state = (int)etapaFlor;

                db.SaveChanges();

                return true;

            }

            }

        public bool setStickerPosition(
                int patientId,
                int stickerId, int? position) {
            if(stickerId <= 0 || position <= 0) return false;   

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                        .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                        .Options;

            using (var db = new DBContext(options: connectionOptions))
            {

                var patient = db.patients.FirstOrDefault(x => x.userId == patientId);

                if(patient == null) return false;


                var userSticker = patient.userInterface.userStickers.FirstOrDefault(x => x.sticker.stickerId == stickerId);

                if(userSticker == null) return false;

                userSticker.position = position;

                db.SaveChanges();

                return true;
            }

        }


        public bool putFlowerInInterface(int id, int flowerId) {


            if(flowerId <= 0 || id <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                        .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                        .Options;

            using (var db = new DBContext(options: connectionOptions))
            {

                var patient = db.patients.Where(x => x.userId == id).Include(x => x.userInterface).ThenInclude(x => x.userFlowers).FirstOrDefault();

                if(patient == null) return false;

                var flower = db.flowers.FirstOrDefault(x => x.flowerId == flowerId);

                if(flower == null) return false;

                patient.userInterface.userFlowers.Add(new UserFlower{
                    flower = flower,
                    position = null,
                    state = 0,
                });

                db.SaveChanges();

                return true;
            }
        }


        public int AddFlowersToPatient(int idPatient, int indexStart, int indexEnd)
{
    if (idPatient <= 0 || indexStart < 0 || indexEnd < 0 || indexStart > indexEnd) 
        return 0;

    var connectionOptions = new DbContextOptionsBuilder<DBContext>()
        .UseSqlServer(Data.Helpers.Constants.ConnectionString)
        .Options;

    using (var db = new DBContext(options: connectionOptions))
    {
        var patient = db.patients.FirstOrDefault(x => x.userId == idPatient);

        if (patient == null) 
            return 0;

        var flowers = db.flowers.ToList();

        if (flowers == null || flowers.Count == 0) 
            return 0;

        // Ensure indexEnd does not exceed the bounds of the flowers list
        indexEnd = Math.Min(indexEnd, flowers.Count - 1);

        for (int i = indexStart; i <= indexEnd; i++)
        {
            var flower = flowers[i];

            if (flower == null) 
                return 0;

            patient.userInterface.userFlowers.Add(new UserFlower
            {
                flower = flower,
                position = null,
                state = 0
            });
        }

        db.SaveChanges();

        return 1;
    }
    }

      public bool UpdateFlower(Flower flower)
{
    if (flower == null) return false;

    var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                .Options;

    using (var db = new DBContext(options: connectionOptions))
    {
        var existingFlower = db.flowers
            .Include(f => f.images)  // Incluir imágenes en la consulta
            .FirstOrDefault(x => x.flowerId == flower.flowerId);

        if (existingFlower == null) return false;

        existingFlower.name = flower.name;

        // Eliminar imágenes antiguas
        existingFlower.images.Clear();

        // Agregar nuevas imágenes
        existingFlower.images = flower.images;

        db.SaveChanges();
        return true;
    }
}

        public bool UpdateSticker(Sticker sticker)
        {
            if(sticker == null) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                        .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                        .Options;

            using (var db = new DBContext(options: connectionOptions))
            {
                var newSticker = db.stickers.FirstOrDefault(x => x.stickerId == sticker.stickerId);

                if(newSticker == null) return false;

                newSticker.url = sticker.url;
                db.SaveChanges();
                return true;
            }
        }   

        //check if a user already has a sticker in userStickers
        public Sticker? HasSticker(int stickerId, int idUsuario)
        {
            if (stickerId <= 0 || idUsuario <= 0) return null;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                        .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                        .Options;

            using (var db = new DBContext(options: connectionOptions))
            {
                var sticker = db.stickers.FirstOrDefault(x => x.stickerId == stickerId);

                if (sticker == null) return null;

                var userSticker = db.patients
                    .Where(x => x.userId == idUsuario)
                    .Include(x => x.userInterface)
                    .ThenInclude(ui => ui.userStickers)
                    .FirstOrDefault()?
                    .userInterface
                    .userStickers
                    .FirstOrDefault(us => us.sticker.stickerId == stickerId);

                return userSticker?.sticker;
            }
        }







        
    }

}