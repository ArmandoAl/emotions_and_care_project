using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ConfiguracionP
    {
        public int Id { get; set; }
        public bool NotificacionesActivas { get; set; } = true;

        public bool DirioActivado { get; set; } = true;

    }
}
