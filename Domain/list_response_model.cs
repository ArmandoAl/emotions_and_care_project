using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ListResponseModel
    {
        public List<TestQuestionForComplete> questions { get; set; } = new List<TestQuestionForComplete>();
    }
}
