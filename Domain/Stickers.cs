using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain
{
    public class Sticker
    {
        [Key]
        public int IdSticker { get; set; }
        public string Imagen { get; set; } = "";

        [JsonIgnore]
        public List<Paciente> Pacintes { get; set; } = new List<Paciente>();
    }
}
