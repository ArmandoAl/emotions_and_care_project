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
        public int AddFlor(Flor flor)
        {
            if(flor == null) return 0;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                        .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                        .Options;

             using (var db = new DBContext(options: connectionOptions))
            {
                db.Flores.Add(flor);
                db.SaveChanges();
                return flor.IdFlor;
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
                db.Sticker.Add(sticker);
                db.SaveChanges();
                return sticker.IdSticker;
            }
        }

        public List<Flor> GetAllFlores()
        {
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                        .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                        .Options;

            using (var db = new DBContext(options: connectionOptions))
            {
                return db.Flores.ToList();
            }
        }

        public List<Sticker> GetAllStickers()
        {
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                        .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                        .Options;

            using (var db = new DBContext(options: connectionOptions))
            {
                return db.Sticker.ToList();
            }
        }

        public Flor GetFlor(int id)
        {
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                        .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                        .Options;

            using (var db = new DBContext(options: connectionOptions))
            {
                return db.Flores.Find(id)!;
            }
        }

        public Sticker GetSticker(int id)
        {
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                        .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                        .Options;

            using (var db = new DBContext(options: connectionOptions))
            {
                return db.Sticker.Find(id)!;
            }

        }
    }

}