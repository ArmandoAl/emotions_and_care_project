using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Paciente : Usuario
    {
        public Especialista? Especialista { get; set; }

        //Caja de Notificaciones
        public List<Notificacion> Notificaciones { get; set; } = new List<Notificacion>();  

        //Agenda
        public List<Cita> Citas { get; set; } = new List<Cita>();

        //Diario
        public List<Nota> Notas { get; set; } = new List<Nota>();

        //Comunidad
        public List<Carta> Cartas { get; set; } = new List<Carta>();


        //Cuestionarios
        public List<Cuestionario> Cuestionarios { get; set; } = new List<Cuestionario>();

        public List<CuestionarioCompletado> cuestionarioCompletados { get; set; } = new List<CuestionarioCompletado>();

        public List<HistoryTestModel> HistorialCuestionarios { get; set; } = new List<HistoryTestModel>();

        
        //Configuracion
        public ConfiguracionP Configuracion { get; set; } = new ConfiguracionP();


        //Inventario
        public List<Logro> Logros { get; set; } = new List<Logro>();
        public List<Sticker> Stickers { get; set; } = new List<Sticker>();
    }
}
