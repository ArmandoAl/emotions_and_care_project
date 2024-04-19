using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain
{
    public class Cuestionario
    {
        [Key]
        public int IdCuestionario { get; set; }

        //agregar la descripcion y el objetivo
        public string NombreCuestionario { get; set; } = "";

        public string Descripcion { get; set; } = "";

        public string Objetivo { get; set; } = "";

        public string Instrucciones { get; set; } = "";

        public List<Pregunta> Preguntas { get; set; } = new List<Pregunta>();

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaModificacion { get; set; }

        [JsonIgnore]
        public List<Paciente> Paciente { get; set; } = new List<Paciente>();
    }
}
