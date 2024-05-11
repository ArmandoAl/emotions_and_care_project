using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class AgregarPaciente
    {
        public string Nombre { get; set; } = "";

        public string Correo { get; set; } = "";

        public string Contraseña { get; set; } = "";

        public string Telefono { get; set; } = "";

        public DateTime FechaNacimiento { get; set; } = DateTime.Now;

        public string Sexo { get; set; } = "";

        public string Token { get; set; } = "";

        public int TerminosycondicionesId { get; set; }
    }
}
