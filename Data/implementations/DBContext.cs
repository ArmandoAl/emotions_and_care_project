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

            public DbSet<SakaNotes> sakaNotes { get; set; } = null!;

            public DbSet<Bukayo> bukayos { get; set; } = null!;

            public DbSet<RecomendationComplete> recomendationComplete { get; set; } = null!;

            public DbSet<Diary> diaries { get; set; } = null!;

            public DbSet<Note> notes { get; set; } = null!;

            public DbSet<Progress> progresses { get; set; } = null!;

            public DbSet<Test> tests { get; set; } = null!;

            public DbSet<SettingsP> settings { get; set; } = null!;

            public DbSet<UserInterface> userInterfaces { get; set; } = null!;

            public DbSet<QuestionnaireForUser> questionnairesForUsers { get; set; } = null!;

            public DbSet<CompleteQuestionnaires> completeQuestionnaires { get; set; } = null!;

            public DbSet<QuestionnairesHistory> questionnairesHistories { get; set; } = null!;

            public DbSet<PatientRequest> patientRequests { get; set; } = null!;

            public DbSet<CartAnswer> cartAnswers { get; set; } = null!;

            public DbSet<InAppText> inAppTexts { get; set; } = null!;

            public DbSet<Badge> badges { get; set; } = null!;

            public DbSet<DummyUser> dummyUsers { get; set; } = null!;

            public DbSet<BadgeCollection> badgeCollections { get; set; } = null!;

            public DbSet<UserBadge> userBadges { get; set; } = null!;

            public DbSet<Achievement> achievements { get; set; } = null!;
            public DbSet<UserAchievement> userAchievements { get; set; } = null!;
            public DbSet<AchievementCollection> achievementCollections { get; set; } = null!;

            public DBContext(DbContextOptions<DBContext> options) : base(options) { }

    }
}