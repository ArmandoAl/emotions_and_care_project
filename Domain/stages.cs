using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain
{
    // Represents a stage in a process or workflow.
    public class Stage
    {
        // Unique identifier for each stage (Primary Key)
        [Key]
        public int stageId { get; set; }

        // List of stage requests associated with this stage
        public List<StageRequest> stageRequests { get; set; } = new List<StageRequest>();
    }

    // Represents a request related to a specific stage.
    public class StageRequest
    {
        // Unique identifier for each stage request (Primary Key)
        [Key]
        public int stageRequestId { get; set; }

        // The identifier of the stage to which this request belongs
        public int stageId { get; set; }

        // Name of the stage request (default is an empty string if not provided)
        public string name { get; set; } = "";

        // Optional value associated with the stage request (nullable, default is 1)
        public int? value { get; set; } = 1;

        // Optional day range associated with the stage request (nullable, default is 7)
        public int? dayRange { get; set; } = 7;
    }
}
