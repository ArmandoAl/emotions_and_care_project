using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class CuestionarioCompletado
    {
        [Key]
        public int IdCuestionarioCompletado { get; set; }

        public int PacienteId { get; set; }

        public int CuestionarioId { get; set; }

        public DateTime FechaCompletado { get; set; }
    }
}
