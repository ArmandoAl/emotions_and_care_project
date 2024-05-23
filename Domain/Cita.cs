using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Cita
    {
        [Key]
        public int IdCita { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now;

        public string? Hora { get; set; }

        public string? Lugar { get; set; }

        public string? Descripcion { get; set; }

        public bool ConfirmadaPorPaciente { get; set; } = false;

        public bool ConfirmadaPorEspecialista { get; set; } = false;

        public bool Realizada { get; set; } = false;

        public Paciente? Paciente { get; set; }
    }

    public class LogroWithCita {
        public Logro? Logro { get; set; } = new Logro();
        public int IdCita { get; set; }
    }
}
