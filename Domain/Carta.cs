    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{

    public enum EstadoCarta
    {
        Enviada,
        Expirada   
    }
    public class Carta
    {
        [Key]
        public int IdCarta { get; set; }
        public int IdEmisor { get; set; }
        public char inicialEmisor { get; set; }
        public string Contenido { get; set; } = "";

        public EstadoCarta Estado { get; set; } = EstadoCarta.Enviada;

        public List<RespuestaCarta> Respuestas { get; set; } = new List<RespuestaCarta>();

        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
