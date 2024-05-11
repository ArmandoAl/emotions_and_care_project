using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class HistoryTestModel
    {
        [Key]
        public int Id { get; set; }

        public int IdCuestionario { get; set; }
        
        public string Name { get; set; } = "";

        public List<TestInfoModel> testInfoModels { get; set; } = new List<TestInfoModel>();
    }
}
