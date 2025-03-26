using Data.Helpers;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Data.Implementations
{       
        public class DBContext : DbContext
        {
            public DbSet<Patient> patients { get; set; } = null!;
            
            public DbSet<Specialist> specialists { get; set; } = null!;


             public DbSet<Questionnaire> questionnaires { get; set; } = null!;


            public DbSet<DateRequest> dateRequests { get; set; } = null!;

            public DbSet<PatientRequest> patientRequest { get; set; } = null!;
  
            public DbSet<Emotion> emotions { get; set; } = null!;


            public DbSet<Cart> carts { get; set; } = null!;

            public DbSet<NotificationModel> notifications { get; set; } = null!;
        

            public DbSet<Date> dates { get; set; } = null!;

            public DbSet<Recomendation> recomendations { get; set; } = null!;                


            public DbSet<TermsAndConditions> terms { get; set; } = null!;

            public DbSet<Goal> goals { get; set; } = null!;
            
            public DbSet<Sticker> stickers { get; set; } = null!;

            public DbSet<Flower> flowers { get; set; } = null!;


            public DbSet<Recomendation> recomendation { get; set; } = null!;

            public DbSet<Stage> stages { get; set; } = null!;

            public DbSet<Opps> opps { get; set; } = null!;

            public DbSet<RecomendationComplete> recomendationComplete { get; set; } = null!;


            public DBContext(DbContextOptions<DBContext> options) : base(options) { }
        }
}