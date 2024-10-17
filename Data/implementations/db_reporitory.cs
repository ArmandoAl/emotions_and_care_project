using Data.Contracts;
using Data.Implementations;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Implementations
{
    public class DBReporitory : IDBRepository
    {

        bool IDBRepository.deleteDatabase()
        {
             var connectionOptions = new DbContextOptionsBuilder<DBContext>()
             .UseSqlServer(Data.Helpers.Constants.ConnectionString)
             .Options;
            using (var db = new DBContext(options: connectionOptions))
            {

                db.Database.EnsureDeleted();

                return true;
            }
        }
    }

}