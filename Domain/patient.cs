using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    // Represents a patient in the system, inheriting from the User class.
    public class Patient : User
    {
        // Represents the specialist assigned to the patient (nullable)
        public Specialist? specialist { get; set; }

        
        public DateTime? syncDate { get; set; } = null;

        // A list of notifications for the patient
        public List<NotificationModel> notifications { get; set; } = new List<NotificationModel>();

        // A list of completed recommendations for the patient
        public List<RecomendationComplete> completeRecomendations { get; set; } = new List<RecomendationComplete>();

        // A list of dates associated with the patient (appointments, visits, etc.)
        public List<Date> dates { get; set; } = new List<Date>();

        // The patient's diary, which contains notes
        public Diary diary { get; set; } = new Diary();

        // A list of carts associated with the patient (e.g., shopping carts, action items)
        public List<Cart> carts { get; set; } = new List<Cart>();

        // Represents the patient's test information
        public Test test { get; set; } = new Test();

        // Configuration settings for the patient
        public SettingsP settings { get; set; } = new SettingsP();

        // A list of goals set by or for the patient
        public List<Goal> goals { get; set; } = new List<Goal>();

        // The registration state of the patient (default is "register")
        public string registerState { get; set; } = "register";

        // Represents the patient's progress in their care or treatment
        public Progress? progress { get; set; } = new Progress 
        { 
            stage = 0,
            lastDate = DateTime.Now,
            begginDate = DateTime.Now 
        };

        // User interface settings specific to the patient (theme, flowers, stickers)
        public UserInterface userInterface { get; set; } = new UserInterface();
    }

    // Represents a completed recommendation for the patient
    public class RecomendationComplete
    {
        [Key]
        public int recomendationCompleteId { get; set; }

        // The ID of the recommendation
        public int recomendationId { get; set; }

        // The date when the recommendation was completed
        public DateTime dateCompleted { get; set; }
    }

    // Represents the progress of a patient through a care or treatment process
    public class Progress
    {
        [Key]
        public int progressId { get; set; }

        // The stage of the patient's progress (e.g., 0 = initial, 1 = halfway, etc.)
        public int stage { get; set; } = 0;

        // The last date the patient's progress was updated
        public DateTime? lastDate { get; set; }

        // The start date of the patient's progress
        public DateTime? begginDate { get; set; }
    }

    // Represents a patient's diary containing a list of notes
    public class Diary
    {
        [Key]
        public int diaryId { get; set; }

        // A list of notes in the patient's diary
        public List<Note> notes { get; set; } = new List<Note>();
    }

    // Represents a patient's test information
    public class Test 
    {
        [Key]
        public int testId { get; set; }

        // A list of questionnaires for the patient to complete
        public List<QuestionnaireForUser> questionnaires { get; set; } = new List<QuestionnaireForUser>();

        // A list of completed questionnaires for the patient
        public List<CompleteQuestionnaires> completeQuestionnaires { get; set; } = new List<CompleteQuestionnaires>();

        // A history of completed questionnaires for the patient
        public List<QuestionnairesHistory> questionnairesHistory { get; set; } = new List<QuestionnairesHistory>();
    }

    // Represents a questionnaire assigned to a patient
    public class QuestionnaireForUser
    {
        [Key]
        public int questionnaireForUserId { get; set; }

        // The ID of the questionnaire
        public int questionnaireId { get; set; }
    }

    // Represents the user interface settings for a patient
    public class UserInterface
    {
        [Key]
        public int userInterfaceId { get; set; }

        // A list of flowers assigned to the user
        public List<UserFlower> userFlowers { get; set; } = new List<UserFlower>();

        // A list of stickers assigned to the user
        public List<UserSticker> userStickers { get; set; } = new List<UserSticker>();

        // Background image URL or ID for the user interface
        public int backgroundUrl { get; set; }

        // The ID of the theme used by the patient
        public int themeId { get; set; }
    }

    // Represents a flower assigned to the user in their interface
    public class UserFlower
    {
        [Key]
        public int userFlowerId { get; set; }

        // The flower object assigned to the user
        public Flower flower { get; set; } = new Flower();

        // The state of the flower (e.g., active, blooming, etc.)
        public int state { get; set; }

        // Whether the flower is active (true/false)
        public bool active { get; set; } = false;

        // The position of the flower (nullable, for UI arrangement)
        public int? position { get; set; }
    }

    // Represents a sticker assigned to the user in their interface
    public class UserSticker
    {
        [Key]
        public int userStickerId { get; set; }

        // The sticker object assigned to the user
        public Sticker sticker { get; set; } = new Sticker();

        // The position of the sticker (nullable, for UI arrangement)
        public int? position { get; set; }
    }
}
