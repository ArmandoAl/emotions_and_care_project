using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain
{

    public enum TipoLogro
    {
        SeguimientoCuestionario,
        SeguimientoConespecialista,
        Recomendaciones,
        Comunidad,
    }   
    

    public class Logro
    {
        [Key]
        public int IdLogro { get; set; }
        public string Nombre { get; set; } = "";
        public string Descripcion { get; set; } = "";
        public TipoLogro Tipo { get; set; }

        public int? idSticker { get; set; }
        public int? idFlor { get; set; }

        [JsonIgnore]
        public List<Paciente> Pacientes { get; set; } = new List<Paciente>();
    }
}
