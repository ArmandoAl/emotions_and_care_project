using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Contracts
{
    public interface IStagesRepository
    {

        int Add(Stage stage);

        Stage? Get(int idStage);

        List<Stage>? GetAll();

        bool Update(Stage stage);

        bool Delete(int idStage);

        int AddRequest(StageRequest stageRequest);

        StageRequest? GetRequest(int idStageRequest);

        List<StageRequest>? GetAllRequests(int idStage);

        bool UpdateRequest(StageRequest stageRequest);

        bool DeleteRequest(int idStageRequest);

    }


}