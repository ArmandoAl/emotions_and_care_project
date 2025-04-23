using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    /// <summary>
    /// Represents a patient in the system, inheriting from the User class.
    /// </summary>
    public class Patient : User
    {
        /// <summary>
        /// Represents the specialist assigned to the patient (nullable).
        /// </summary>
        public Specialist? specialist { get; set; }

        /// <summary>
        /// The date of the last sync operation.
        /// </summary>
        public DateTime? syncDate { get; set; } = null;

        /// <summary>
        /// A list of notifications for the patient.
        /// </summary>
        public List<NotificationModel> notifications { get; set; } = new List<NotificationModel>();

        /// <summary>
        /// A list of completed recommendations for the patient.
        /// </summary>
        public List<RecomendationComplete> completeRecomendations { get; set; } = new List<RecomendationComplete>();

        /// <summary>
        /// A list of dates associated with the patient (appointments, visits, etc.).
        /// </summary>
        public List<Date> dates { get; set; } = new List<Date>();

        /// <summary>
        /// The patient's diary, which contains notes.
        /// </summary>
        public Diary diary { get; set; } = new Diary();

        /// <summary>
        /// A list of carts associated with the patient (e.g., shopping carts, action items).
        /// </summary>
        public List<Cart> carts { get; set; } = new List<Cart>();

        /// <summary>
        /// Represents the patient's test information.
        /// </summary>
        public Test test { get; set; } = new Test();

        /// <summary>
        /// Configuration settings for the patient.
        /// </summary>
        public SettingsP settings { get; set; } = new SettingsP();

        /// <summary>
        /// A list of goals set by or for the patient.
        /// </summary>
        public List<Goal> goals { get; set; } = new List<Goal>();

        /// <summary>
        /// The registration state of the patient (default is "register").
        /// </summary>
        public string registerState { get; set; } = "register";

        public bool confirmed { get; set; } = false;

        public string codeHelper { get; set; } = "";

        /// <summary>
        /// Represents the patient's progress in their care or treatment.
        /// </summary>
        public Progress? progress { get; set; } = new Progress
        { 
            stage = 0,
            lastDate = DateTime.Now,
            begginDate = DateTime.Now 
        };

        /// <summary>
        /// User interface settings specific to the patient (theme, flowers, stickers).
        /// </summary>
        public UserInterface userInterface { get; set; } = new UserInterface();
    }

    /// <summary>
    /// Represents a completed recommendation for the patient.
    /// </summary>
    public class RecomendationComplete
    {
        [Key]
        public int recomendationCompleteId { get; set; }

        /// <summary>
        /// The ID of the recommendation.
        /// </summary>
        public int recomendationId { get; set; }

        /// <summary>
        /// The date when the recommendation was completed.
        /// </summary>
        public DateTime dateCompleted { get; set; }
    }

    /// <summary>
    /// Represents the progress of a patient through a care or treatment process.
    /// </summary>
    public class Progress
    {
        [Key]
        public int progressId { get; set; }

        /// <summary>
        /// The stage of the patient's progress (e.g., 0 = initial, 1 = halfway, etc.).
        /// </summary>
        public int stage { get; set; } = 0;

        /// <summary>
        /// The last date the patient's progress was updated.
        /// </summary>
        public DateTime? lastDate { get; set; }

        /// <summary>
        /// The start date of the patient's progress.
        /// </summary>
        public DateTime? begginDate { get; set; }
    }

    /// <summary>
    /// Represents a patient's diary containing a list of notes.
    /// </summary>
    public class Diary
    {
        [Key]
        public int diaryId { get; set; }

        /// <summary>
        /// A list of notes in the patient's diary.
        /// </summary>
        public List<Note> notes { get; set; } = new List<Note>();
    }

    /// <summary>
    /// Represents a patient's test information.
    /// </summary>
    public class Test 
    {
        [Key]
        public int testId { get; set; }

        /// <summary>
        /// A list of questionnaires for the patient to complete.
        /// </summary>
        public List<QuestionnaireForUser> questionnaires { get; set; } = new List<QuestionnaireForUser>();

        /// <summary>
        /// A list of completed questionnaires for the patient.
        /// </summary>
        public List<CompleteQuestionnaires> completeQuestionnaires { get; set; } = new List<CompleteQuestionnaires>();

        /// <summary>
        /// A history of completed questionnaires for the patient.
        /// </summary>
        public List<QuestionnairesHistory> questionnairesHistory { get; set; } = new List<QuestionnairesHistory>();
    }

    /// <summary>
    /// Represents a questionnaire assigned to a patient.
    /// </summary>
    public class QuestionnaireForUser
    {
        [Key]
        public int questionnaireForUserId { get; set; }

        /// <summary>
        /// The ID of the questionnaire.
        /// </summary>
        public int questionnaireId { get; set; }
    }

    /// <summary>
    /// Represents the user interface settings for a patient.
    /// </summary>
    public class UserInterface
    {
        [Key]
        public int userInterfaceId { get; set; }

        /// <summary>
        /// A list of flowers assigned to the user.
        /// </summary>
        public List<UserFlower> userFlowers { get; set; } = new List<UserFlower>();

        /// <summary>
        /// A list of stickers assigned to the user.
        /// </summary>
        public List<UserSticker> userStickers { get; set; } = new List<UserSticker>();

        /// <summary>
        /// Background image URL or ID for the user interface.
        /// </summary>
        public int backgroundUrl { get; set; }

        /// <summary>
        /// The ID of the theme used by the patient.
        /// </summary>
        public int themeId { get; set; }
    }

    /// <summary>
    /// Represents a flower assigned to the user in their interface.
    /// </summary>
    public class UserFlower
    {
        [Key]
        public int userFlowerId { get; set; }

        /// <summary>
        /// The flower object assigned to the user.
        /// </summary>
        public Flower flower { get; set; } = new Flower();

        /// <summary>
        /// The state of the flower (e.g., active, blooming, etc.).
        /// </summary>
        public int state { get; set; }

        /// <summary>
        /// Whether the flower is active (true/false).
        /// </summary>
        public bool active { get; set; } = false;

        /// <summary>
        /// The position of the flower (nullable, for UI arrangement).
        /// </summary>
        public int? position { get; set; }
    }

    /// <summary>
    /// Represents a sticker assigned to the user in their interface.
    /// </summary>
    public class UserSticker
    {
        [Key]
        public int userStickerId { get; set; }

        /// <summary>
        /// The sticker object assigned to the user.
        /// </summary>
        public Sticker sticker { get; set; } = new Sticker();

        /// <summary>
        /// The position of the sticker (nullable, for UI arrangement).
        /// </summary>
        public int? position { get; set; }
    }
}
