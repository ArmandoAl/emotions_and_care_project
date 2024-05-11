using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }

        public string Nombre { get; set; } = "";

        public string Correo { get; set; } = "";

        public string Contraseña { get; set; } = "";

        public string Telefono { get; set; } = "";

        public DateTime FechaNacimiento { get; set; }

        public int Edad { get; set; }

        public string Sexo { get; set; } = "";

        public string Token { get; set; } = "";

        public string TokenRelacional { get; set; } = "000000";

        public TerminosYCondiciones Terminosycondiciones { get; set; } = new TerminosYCondiciones();

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaModificacion { get; set; }

    }
}
