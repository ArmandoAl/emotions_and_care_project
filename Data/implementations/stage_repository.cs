using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.contracts;
using Data.Contracts;
using Data.Implementations;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Data.implementations
{
    public class StageRepository : IStagesRepository
    {

        public int Add(Stage stage)
        {
            if(stage == null) return 0;
            
             var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                db.stages.Add(stage);
                db.SaveChanges();
                return stage.stageId;
            }
        }

        public int AddRequest(Stage stageRequest)
        {
            if(stageRequest == null) return 0;
            
             var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                db.stages.Add(stageRequest);
                db.SaveChanges();
                return stageRequest.stageId;
            }
        }

        public bool Delete(int idStage)
        {
            if(idStage == 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var stage = db.stages.Find(idStage);
                if(stage == null) return false;
                db.stages.Remove(stage);
                db.SaveChanges();
                return true;
            }
        }

        public bool DeleteRequest(int idStageRequest)
        {
            if(idStageRequest == 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var stageRequest = db.stages.Find(idStageRequest);

                if(stageRequest == null) return false;

                db.stages.Remove(stageRequest);

                db.SaveChanges();

                return true;
            }
        }

        public Stage? Get(int idStage)
        {
            if(idStage == 0) return null;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                return db.stages.Find(idStage);
            }
        }
        

        public StageRequest? GetRequest(int idStageRequest)
        {
            if(idStageRequest == 0) return null;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var stageRequest = db.stages.Find(idStageRequest);

                if(stageRequest == null) return null;

                return stageRequest.stageRequests.FirstOrDefault();
            }
        }

        public List<Stage>? GetAll()
        {
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                return db.stages.ToList();
            }
        }

        public List<StageRequest>? GetAllRequests(int idStage)
        {
            if(idStage == 0) return null;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var stage = db.stages.Find(idStage);

                if(stage == null) return null;

                return stage.stageRequests;
            }
        }

        public bool Update(Stage stage)
        {
            if(stage == null) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                db.stages.Update(stage);
                db.SaveChanges();
                return true;
            }
        }

        public bool UpdateRequest(StageRequest stageRequest)
        {
            if(stageRequest == null) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var stage = db.stages.Find(stageRequest.stageId);

                if(stage == null) return false;

                var stageRequestToUpdate = stage.stageRequests.Where(l => l.stageRequestId == stageRequest.stageRequestId).FirstOrDefault();


                if(stageRequestToUpdate == null) return false;

                stageRequestToUpdate.name = stageRequest.name;

                stageRequestToUpdate.value = stageRequest.value;

                stageRequestToUpdate.dayRange = stageRequest.dayRange;

                db.stages.Update(stage);

                db.SaveChanges();

                return true;
            }
        }

        public int AddRequest(StageRequest stageRequest)
        {
            if(stageRequest == null) return 0;
            
             var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var stage = db.stages.Find(stageRequest.stageId);

                if(stage == null) return 0;

                stage.stageRequests.Add(stageRequest);

                db.stages.Update(stage);

                db.SaveChanges();

                return stageRequest.stageRequestId;

                
            }
        }
    


    }


}


