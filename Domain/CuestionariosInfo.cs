using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class CuestionariosInfo
    {
        public List<Cuestionario> Cuestionarios { get; set; } = new List<Cuestionario>();
        
        public List<CuestionarioCompletado> cuestionarioCompletados { get; set; } = new List<CuestionarioCompletado>();

        public List<HistoryTestModel> historialCuestionarios { get; set; } = new List<HistoryTestModel>();
    }
}
