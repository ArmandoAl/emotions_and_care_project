using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Nota
    {
        [Key]
        public int IdNota { get; set; }

        public string Titulo { get; set; } = "";
 
        public string Contenido { get; set; } = "";

        public Emocion Emocion { get; set; } = new Emocion();

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaModificacion { get; set;} 

        public bool Visible { get; set; } = true;
    }
}
