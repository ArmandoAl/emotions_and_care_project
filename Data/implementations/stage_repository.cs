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

        /// <summary>
        /// Adds a new stage to the database and saves it.
        /// This method accepts a `Stage` object, adds it to the database, and saves the changes. 
        /// If the stage is successfully added, it returns the ID of the newly added stage.
        /// </summary>
        /// <param name="stage">The `Stage` object to be added to the database.</param>
        /// <returns>
        /// Returns the unique ID of the newly added stage if successful; otherwise, returns 0 if the stage is null or an error occurs.
        /// </returns>
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


        /// <summary>
        /// Adds a new stage request to the database and saves it.
        /// This method accepts a `Stage` object representing a stage request, adds it to the database, 
        /// and saves the changes. If the stage request is successfully added, it returns the ID of the newly added stage request.
        /// </summary>
        /// <param name="stageRequest">The `Stage` object representing the stage request to be added to the database.</param>
        /// <returns>
        /// Returns the unique ID of the newly added stage request if successful; otherwise, returns 0 if the stage request is null or an error occurs.
        /// </returns>
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

        /// <summary>
        /// Retrieves a stage from the database based on the provided stage ID.
        /// This method queries the database for a stage that matches the given ID and returns the corresponding `Stage` object.
        /// If no stage is found or the ID is invalid, it returns null.
        /// </summary>
        /// <param name="idStage">The unique ID of the stage to be retrieved.</param>
        /// <returns>
        /// Returns the `Stage` object corresponding to the provided ID if found; otherwise, returns null if the stage is not found or the ID is invalid.
        /// </returns>
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

        

        /// <summary>
        /// Retrieves the first stage request associated with the provided stage request ID from the database.
        /// This method queries the database for a stage request that matches the given ID and returns the first associated `StageRequest` object.
        /// If no stage request is found or the ID is invalid, it returns null.
        /// </summary>
        /// <param name="idStageRequest">The unique ID of the stage request to be retrieved.</param>
        /// <returns>
        /// Returns the first `StageRequest` object associated with the provided ID if found; otherwise, returns null if the stage request is not found or the ID is invalid.
        /// </returns>
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


