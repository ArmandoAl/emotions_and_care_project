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
    
    public class TextRepository : ITextRepository
    {
        public int Add(InAppText text)
        {
            if (text == null) return 0;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
          .UseSqlServer(Data.Helpers.Constants.ConnectionString)
          .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                db.inAppTexts.Add(text);
                db.SaveChanges();
                return text.textId;
            }
        }

        public InAppText? Get(int id)
        {
            if (id <= 0) return null;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
          .UseSqlServer(Data.Helpers.Constants.ConnectionString)
          .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                return db.inAppTexts.FirstOrDefault(x => x.textId == id);
            }
        }

        public bool Delete(int id)
        {
            if (id <= 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
          .UseSqlServer(Data.Helpers.Constants.ConnectionString)
          .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var text = db.inAppTexts.FirstOrDefault(x => x.textId == id);
                if (text == null) return false;

                db.inAppTexts.Remove(text);
                db.SaveChanges();
                return true;
            }
        }

        public bool Update(int id, InAppText text)
        {
            if (text == null) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                .Options;

            using (var db = new DBContext(options: connectionOptions))
            {
                var existingText = db.inAppTexts.Find(id);
                if (existingText == null) return false;

                existingText.text = text.text;
                existingText.textType = text.textType;
                existingText.modifiedDate = DateTime.Now;

                db.SaveChanges();
                return true;
            }
        }


        public List<InAppText> GetAll()
        {
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
          .UseSqlServer(Data.Helpers.Constants.ConnectionString)
          .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                return db.inAppTexts.ToList();
            }
        }
    }
}