using Data.Helpers;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Data.Implementations
{       
        public class DBContext : DbContext
        {
            public DbSet<Patient> patients { get; set; } = null!;
            
            public DbSet<Specialist> specialists { get; set; } = null!;

            public DbSet<Diary> dairy { get; set; } = null!;

            public DbSet<DateRequest> dateRequests { get; set; } = null!;

            public DbSet<PatientRequest> patientRequest { get; set; } = null!;
  
            public DbSet<Emotion> emotions { get; set; } = null!;

            public DbSet<Test> test { get; set; } = null!;


            public DbSet<SettingsP> ConfuguracionesPaciente { get; set; } = null!;

            public DbSet<Cart> carts { get; set; } = null!;

            public DbSet<Notification> notifications { get; set; } = null!;

            public DbSet<Schedule> schedule { get; set; } = null!;            

            public DbSet<TermsAndConditions> terms { get; set; } = null!;

            public DbSet<Goal> goals { get; set; } = null!;
            
            public DbSet<Sticker> stickers { get; set; } = null!;

            public DbSet<Flower> flowers { get; set; } = null!;



            public DbSet<Recomandation> recomendation { get; set; } = null!;


            public DBContext(DbContextOptions<DBContext> options) : base(options) { }
        }
}