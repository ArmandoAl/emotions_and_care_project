using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Publicacion
    {
        [Key]
        public int IdPublicacion { get; set; }

        public int IdUsuario { get; set; }

        public string NombreUsuario { get; set; } = "";

        public string Contenido { get; set; } = "";

        public DateTime FechaPublicacion { get; set; } = DateTime.Now;

        public List<Comentario> Comentarios { get; set; } = new List<Comentario>(); 

        public int Likes { get; set; } = 0;
    }

    public class Comentario
    {
        [Key]
        public int IdComentario { get; set; }

        public int IdUsuario { get; set; }

        public string NombreUsuario { get; set; } = "";

        public string Contenido { get; set; } = "";

        public DateTime FechaComentario { get; set; } = DateTime.Now;
    }

    
}
