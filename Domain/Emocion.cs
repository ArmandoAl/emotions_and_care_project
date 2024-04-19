using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain
{
    public class Emocion
    {
        [Key]
        public int IdEmocion { get; set; }
 
        public string Nombre { get; set; } = "";

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaModificacion { get; set; }

        [JsonIgnore]
        public List<Nota> Notas { get; set; } = new List<Nota>();
    }
}
