using Data.Helpers;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Data.Implementations
{       
        public class DBContext : DbContext
        {
            public DbSet<Paciente> Pacientes { get; set; } = null!;
            
            public DbSet<Especialista> Especialistas { get; set; } = null!;

            public DbSet<Nota> Notas { get; set; } = null!;

            public DbSet<SolicitudCita> SolicitudesCita { get; set; } = null!;

            public DbSet<SolicitudPaciente> SolicitudesPaciente { get; set; } = null!;
  
            public DbSet<Emocion> Emociones { get; set; } = null!;

            public DbSet<Cuestionario> Cuestionarios { get; set; } = null!;

            public DbSet<CuestionarioCompletado> CuestionarioCompletados { get; set; } = null!;
            
            public DbSet<HistoryTestModel> HistorialesCuestionariosCompletados { get; set; }  = null!; 

            public DbSet<ConfiguracionP> ConfuguracionesPaciente { get; set; } = null!;

            public DbSet<Carta> Cartas { get; set; } = null!;

            public DbSet<Notificacion> Notificaciones { get; set; } = null!;

            public DbSet<Cita> Citas { get; set; } = null!;            

            public DbSet<TerminosYCondiciones> TerminosYCondiciones { get; set; } = null!;

            public DbSet<Logro> Logros { get; set; } = null!;
            
            public DbSet<Sticker> Sticker { get; set; } = null!;

            public DbSet<Flor> Flores { get; set; } = null!;

            public DbSet<Publicacion> Publicaciones { get; set; } = null!;

            public DbSet<Recomendacion> Recomendaciones { get; set; } = null!;

            public DbSet<StickerDeUsuarioModel> StickersDeUsuario { get; set; } = null!;

            public DbSet<FloresDelUsuarioModel> FloresDelUsuario { get; set; } = null!;

            public DBContext(DbContextOptions<DBContext> options) : base(options) { }
        }
}