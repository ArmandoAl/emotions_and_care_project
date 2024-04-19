using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{

    public class Respuesta
    {
        [Key]
        public int IdRespuesta { get; set; }

        public string TextoRespuesta { get; set; } = "";

        // public string tipoRespuesta { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaModificacion { get; set; }
    }
}
