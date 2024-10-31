using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain
{

    public class Stage 
    {
        [Key]
        public int stageId { get; set; }

        public List<StageRequest> stageRequests { get; set; } = new List<StageRequest>();
    }

    public class StageRequest
    {
        [Key]
        public int stageRequestId { get; set; }
        public int stageId { get; set; }
        public string name { get; set; } = "";

        public int? value { get; set; } = 1;

        public int? dayRange { get; set; } = 7;
    }
}
