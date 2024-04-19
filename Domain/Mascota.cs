using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Mascota
    {
        [Key]
        public int IdMascota { get; set; }
        public string Nombre { get; set; } = "";
        public string Imagen { get; set; } = "";

    }
}
