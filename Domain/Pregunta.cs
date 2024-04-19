using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{

    public enum PreguntaTipo
    {
        Numerica,
        Booleana,
        OpcionMultiple,
        Abierta
    }
    public class Pregunta
    {
        [Key]
        public int IdPregunta { get; set; }

        public string Enunciado { get; set; } = "";

        public PreguntaTipo Tipo { get; set; }

        public List<Respuesta>? Respuestas { get; set; } = new List<Respuesta>();

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaModificacion { get; set; }
    }
}
