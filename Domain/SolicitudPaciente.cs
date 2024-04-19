using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class SolicitudPaciente
    {
        [Key]
        public int IdSolicitudPaciente { get; set; }

        public Paciente Paciente { get; set; } = new Paciente();
    }
}
