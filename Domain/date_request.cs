using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class DateRequest
    {
        [Key]
        public int dateRequestId { get; set; }

        public Date? Cita { get; set; }
    }
}
