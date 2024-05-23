using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class RespuestaCarta
    {
        [Key]
        public int IdCarta { get; set; }

        public int IdReceptor { get; set; }

        public char LetraReceptor { get; set; }

        public string Contenido { get; set; } = "";

        public int? IdSticker { get; set; }

        public bool Leida { get; set; } = false;

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaModificacion { get; set; }
    }

    public class LogroWithRespuestaCarta
    {
        public Logro? Logro { get; set; } = new Logro();
        public int IdRespuestaCarta { get; set; }
    }
}
