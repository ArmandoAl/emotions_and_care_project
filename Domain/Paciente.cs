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

        public List<Logro> logros { get; set; } = new List<Logro>();
        public List<StickerDeUsuarioModel> Stickersds { get; set; } = new List<StickerDeUsuarioModel>();
        public List<FloresDelUsuarioModel> FloresDelUsuario { get; set; } = new List<FloresDelUsuarioModel>();

        public bool registerSet { get; set; } = false;
    }
    
    public class FloresDelUsuarioModel
    {
        [Key]
        public int StickerIdSticker { get; set; }
        public Flor Flor { get; set; } = new Flor();
        public bool Active { get; set; }
        public EtapaFlor Etapa { get; set; } = EtapaFlor.initialFlowet;
        public int idUsuario { get; set; }
    }

    public class StickerDeUsuarioModel
    {
        [Key]
        public int IdStickerDeUsuarioModel { get; set; }
        public Sticker Sticker { get; set; } = new Sticker();
        public int? Posicion { get; set; }

         public int idUsuario { get; set; }
    }

}
