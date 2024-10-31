using Business.Contracts;
using Data.contracts;
using Data.Contracts;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Implementations
{
    public class StageService : IStageService
    {
        private readonly IStagesRepository _stagesRepository;

        public StageService(IStagesRepository stagesRepository)
        {
            _stagesRepository = stagesRepository;
        }

        public int Add(Stage stage)
        {
            return _stagesRepository.Add(stage);
        }

        public bool Delete(int idStage)
        {
            return _stagesRepository.Delete(idStage);
        }

        public bool DeleteRequest(int idStageRequest)
        {
            return _stagesRepository.DeleteRequest(idStageRequest);
        }

        public Stage? Get(int idStage)
        {
            return _stagesRepository.Get(idStage);
        }

        public StageRequest? GetRequest(int idStageRequest)
        {
            return _stagesRepository.GetRequest(idStageRequest);
        }

        public List<Stage>? GetAll()
        {
            return _stagesRepository.GetAll();
        }

        public List<StageRequest>? GetAllRequests(int idStage)
        {
            return _stagesRepository.GetAllRequests(idStage);
        }

        public int AddRequest(StageRequest stageRequest)
        {
            return _stagesRepository.AddRequest(stageRequest);
        }

        public bool Update(Stage stage)
        {
            return _stagesRepository.Update(stage);
        }

        public bool UpdateRequest(StageRequest stageRequest)
        {
            return _stagesRepository.UpdateRequest(stageRequest);
        }

    }


}