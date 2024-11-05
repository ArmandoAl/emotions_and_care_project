using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Patient : User
    {
        public Specialist? specialist { get; set; }

        //Caja de Notificaciones
        public List<NotificationModel> notifications { get; set; } = new List<NotificationModel>();  

        public List<RecomendationComplete> completeRecomendations { get; set; } = new List<RecomendationComplete>();

        public List<Date> dates { get; set; } = new List<Date>();

        public Diary diary { get; set; } = new Diary();


       public List<Cart> carts { get; set; } = new List<Cart>();

        public Test test { get; set; } = new Test();
        //Configuracion
        public SettingsP settings { get; set; } = new SettingsP();

        public List<Goal> goals { get; set; } = new List<Goal>();

        public string registerState { get; set; } = "register";

       public Progress? progress { get; set; } = new Progress {
            stage = 0,
            lastDate = DateTime.Now,
            begginDate = DateTime.Now 
         };

        public UserInterface userInterface { get; set; } = new UserInterface();
    }

    public class  RecomendationComplete {
        [Key]
        public int recomendationCompleteId { get; set; }
        public int recomendationId { get; set; }
        public DateTime dateCompleted { get; set; }
    }


    public class Progress
    {
        [Key]
        public int progressId { get; set; }

        public int stage { get; set; } = 0;

        public DateTime? lastDate { get; set; }

        public DateTime? begginDate { get; set; }
        
    }


    public class Diary
    {
        [Key]
        public int diaryId { get; set; }
        public List<Note> notes { get; set; } = new List<Note>();
    }

    public class Test 
    {
        [Key]
        public int testId { get; set; }
        public List<QuestionnaireForUser> questionnaires { get; set; } = new List<QuestionnaireForUser>();

        public List<CompleteQuestionnaires> completeQuestionnaires { get; set; } = new List<CompleteQuestionnaires>();

        public List<QuestionnairesHistory> questionnairesHistory { get; set; } = new List<QuestionnairesHistory>();
    }

    public class QuestionnaireForUser
    {
        [Key]
        public int questionnaireForUserId { get; set; }
        public int questionnaireId { get; set; }

    }

    //Historial de cuestionarios completados


    public class UserInterface
    {
        [Key]
        public int userInterfaceId { get; set; }

        public List<UserFlower> userFlowers { get; set; } = new List<UserFlower>();

        public List<UserSticker> userStickers { get; set; } = new List<UserSticker>();

        public int backgroundUrl { get; set; }

        public int themeId { get; set; } 
        
    }

    public class UserFlower
    {
        [Key]
        public int userFlowerId { get; set; }
        public Flower flower { get; set; } = new Flower();
        public int state { get; set; }

        public bool active { get; set; } = false;

        public int? position { get; set; }
    }

    public class UserSticker
    {
        [Key]
        public int userStickerId { get; set; }
        public Sticker sticker { get; set; } = new Sticker();

        public int? position { get; set; }
    }





}
    