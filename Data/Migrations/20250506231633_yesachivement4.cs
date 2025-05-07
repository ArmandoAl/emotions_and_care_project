using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class yesachivement4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "achievementCollections",
                columns: table => new
                {
                    achievementCollectionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_achievementCollections", x => x.achievementCollectionId);
                });

            migrationBuilder.CreateTable(
                name: "achievements",
                columns: table => new
                {
                    achievementId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    category = table.Column<int>(type: "int", nullable: false),
                    imageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    progressMap = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_achievements", x => x.achievementId);
                });

            migrationBuilder.CreateTable(
                name: "badgeCollections",
                columns: table => new
                {
                    badgeCollectionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: false),
                    dateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_badgeCollections", x => x.badgeCollectionId);
                });

            migrationBuilder.CreateTable(
                name: "badges",
                columns: table => new
                {
                    badgeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    category = table.Column<int>(type: "int", nullable: false),
                    imageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    progressMap = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_badges", x => x.badgeId);
                });

            migrationBuilder.CreateTable(
                name: "bukayos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bukayos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "diaries",
                columns: table => new
                {
                    diaryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_diaries", x => x.diaryId);
                });

            migrationBuilder.CreateTable(
                name: "emotions",
                columns: table => new
                {
                    emotionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_emotions", x => x.emotionId);
                });

            migrationBuilder.CreateTable(
                name: "flowers",
                columns: table => new
                {
                    flowerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flowers", x => x.flowerId);
                });

            migrationBuilder.CreateTable(
                name: "inAppTexts",
                columns: table => new
                {
                    textId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    textType = table.Column<int>(type: "int", nullable: false),
                    dateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inAppTexts", x => x.textId);
                });

            migrationBuilder.CreateTable(
                name: "progresses",
                columns: table => new
                {
                    progressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    stage = table.Column<int>(type: "int", nullable: false),
                    lastDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    begginDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_progresses", x => x.progressId);
                });

            migrationBuilder.CreateTable(
                name: "questionnaires",
                columns: table => new
                {
                    questionnaireId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    questionnaireName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    objective = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    instructions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_questionnaires", x => x.questionnaireId);
                });

            migrationBuilder.CreateTable(
                name: "Recomendation",
                columns: table => new
                {
                    recomendationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    type = table.Column<int>(type: "int", nullable: false),
                    subTitle = table.Column<int>(type: "int", nullable: false),
                    reference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createdDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recomendation", x => x.recomendationId);
                });

            migrationBuilder.CreateTable(
                name: "settings",
                columns: table => new
                {
                    settingsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    notificationsActive = table.Column<bool>(type: "bit", nullable: false),
                    diaryActive = table.Column<bool>(type: "bit", nullable: false),
                    questionnaireActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_settings", x => x.settingsId);
                });

            migrationBuilder.CreateTable(
                name: "stages",
                columns: table => new
                {
                    stageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stages", x => x.stageId);
                });

            migrationBuilder.CreateTable(
                name: "stickers",
                columns: table => new
                {
                    stickerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    url = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stickers", x => x.stickerId);
                });

            migrationBuilder.CreateTable(
                name: "terms",
                columns: table => new
                {
                    termsAndConditionsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    termsAndConditions = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_terms", x => x.termsAndConditionsId);
                });

            migrationBuilder.CreateTable(
                name: "tests",
                columns: table => new
                {
                    testId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tests", x => x.testId);
                });

            migrationBuilder.CreateTable(
                name: "userInterfaces",
                columns: table => new
                {
                    userInterfaceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    backgroundUrl = table.Column<int>(type: "int", nullable: false),
                    themeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userInterfaces", x => x.userInterfaceId);
                });

            migrationBuilder.CreateTable(
                name: "userAchievements",
                columns: table => new
                {
                    userAchievementId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    achievementId = table.Column<int>(type: "int", nullable: false),
                    progress = table.Column<int>(type: "int", nullable: false),
                    dateEarned = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    achievementCollectionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userAchievements", x => x.userAchievementId);
                    table.ForeignKey(
                        name: "FK_userAchievements_achievementCollections_achievementCollectionId",
                        column: x => x.achievementCollectionId,
                        principalTable: "achievementCollections",
                        principalColumn: "achievementCollectionId");
                    table.ForeignKey(
                        name: "FK_userAchievements_achievements_achievementId",
                        column: x => x.achievementId,
                        principalTable: "achievements",
                        principalColumn: "achievementId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "dummyUsers",
                columns: table => new
                {
                    BuserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    favoritePlayer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    patientId = table.Column<int>(type: "int", nullable: false),
                    favoriteTeam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    favoriteStadium = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    badgeCollectionId = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    bornDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    age = table.Column<int>(type: "int", nullable: false),
                    sex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    relationalToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dummyUsers", x => x.BuserId);
                    table.ForeignKey(
                        name: "FK_dummyUsers_badgeCollections_badgeCollectionId",
                        column: x => x.badgeCollectionId,
                        principalTable: "badgeCollections",
                        principalColumn: "badgeCollectionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "userBadges",
                columns: table => new
                {
                    userBadgeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    badgeId = table.Column<int>(type: "int", nullable: false),
                    progress = table.Column<int>(type: "int", nullable: false),
                    dateEarned = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    badgeCollectionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userBadges", x => x.userBadgeId);
                    table.ForeignKey(
                        name: "FK_userBadges_badgeCollections_badgeCollectionId",
                        column: x => x.badgeCollectionId,
                        principalTable: "badgeCollections",
                        principalColumn: "badgeCollectionId");
                    table.ForeignKey(
                        name: "FK_userBadges_badges_badgeId",
                        column: x => x.badgeId,
                        principalTable: "badges",
                        principalColumn: "badgeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "opps",
                columns: table => new
                {
                    BuserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    bukayoSakaDiaryId = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    bornDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    age = table.Column<int>(type: "int", nullable: false),
                    sex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    relationalToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_opps", x => x.BuserId);
                    table.ForeignKey(
                        name: "FK_opps_bukayos_bukayoSakaDiaryId",
                        column: x => x.bukayoSakaDiaryId,
                        principalTable: "bukayos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sakaNotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BukayoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sakaNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sakaNotes_bukayos_BukayoId",
                        column: x => x.BukayoId,
                        principalTable: "bukayos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "notes",
                columns: table => new
                {
                    noteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    emotionId = table.Column<int>(type: "int", nullable: false),
                    dateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    visible = table.Column<bool>(type: "bit", nullable: false),
                    diaryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notes", x => x.noteId);
                    table.ForeignKey(
                        name: "FK_notes_diaries_diaryId",
                        column: x => x.diaryId,
                        principalTable: "diaries",
                        principalColumn: "diaryId");
                    table.ForeignKey(
                        name: "FK_notes_emotions_emotionId",
                        column: x => x.emotionId,
                        principalTable: "emotions",
                        principalColumn: "emotionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImageModel",
                columns: table => new
                {
                    imageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    flowerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageModel", x => x.imageId);
                    table.ForeignKey(
                        name: "FK_ImageModel_flowers_flowerId",
                        column: x => x.flowerId,
                        principalTable: "flowers",
                        principalColumn: "flowerId");
                });

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    questionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    statement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    type = table.Column<int>(type: "int", nullable: false),
                    dateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    questionnaireId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.questionId);
                    table.ForeignKey(
                        name: "FK_Question_questionnaires_questionnaireId",
                        column: x => x.questionnaireId,
                        principalTable: "questionnaires",
                        principalColumn: "questionnaireId");
                });

            migrationBuilder.CreateTable(
                name: "QuestionnaireResult",
                columns: table => new
                {
                    questionnaireResultId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    value = table.Column<int>(type: "int", nullable: false),
                    result = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    questionnaireId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionnaireResult", x => x.questionnaireResultId);
                    table.ForeignKey(
                        name: "FK_QuestionnaireResult_questionnaires_questionnaireId",
                        column: x => x.questionnaireId,
                        principalTable: "questionnaires",
                        principalColumn: "questionnaireId");
                });

            migrationBuilder.CreateTable(
                name: "StageRequest",
                columns: table => new
                {
                    stageRequestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    stageId = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    value = table.Column<int>(type: "int", nullable: true),
                    dayRange = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StageRequest", x => x.stageRequestId);
                    table.ForeignKey(
                        name: "FK_StageRequest_stages_stageId",
                        column: x => x.stageId,
                        principalTable: "stages",
                        principalColumn: "stageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "specialists",
                columns: table => new
                {
                    userId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    license = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    focus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    institution = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    presentation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    adress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    bornDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    age = table.Column<int>(type: "int", nullable: false),
                    sex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    relationalToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    termsAndConditionsId = table.Column<int>(type: "int", nullable: false),
                    dateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_specialists", x => x.userId);
                    table.ForeignKey(
                        name: "FK_specialists_terms_termsAndConditionsId",
                        column: x => x.termsAndConditionsId,
                        principalTable: "terms",
                        principalColumn: "termsAndConditionsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "completeQuestionnaires",
                columns: table => new
                {
                    completeQuestionnaireId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    patientId = table.Column<int>(type: "int", nullable: false),
                    questionnaireId = table.Column<int>(type: "int", nullable: false),
                    dateCompleted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    testId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_completeQuestionnaires", x => x.completeQuestionnaireId);
                    table.ForeignKey(
                        name: "FK_completeQuestionnaires_tests_testId",
                        column: x => x.testId,
                        principalTable: "tests",
                        principalColumn: "testId");
                });

            migrationBuilder.CreateTable(
                name: "questionnairesForUsers",
                columns: table => new
                {
                    questionnaireForUserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    questionnaireId = table.Column<int>(type: "int", nullable: false),
                    testId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_questionnairesForUsers", x => x.questionnaireForUserId);
                    table.ForeignKey(
                        name: "FK_questionnairesForUsers_tests_testId",
                        column: x => x.testId,
                        principalTable: "tests",
                        principalColumn: "testId");
                });

            migrationBuilder.CreateTable(
                name: "questionnairesHistories",
                columns: table => new
                {
                    questionnairesHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    questionnaireId = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    testId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_questionnairesHistories", x => x.questionnairesHistoryId);
                    table.ForeignKey(
                        name: "FK_questionnairesHistories_tests_testId",
                        column: x => x.testId,
                        principalTable: "tests",
                        principalColumn: "testId");
                });

            migrationBuilder.CreateTable(
                name: "UserFlower",
                columns: table => new
                {
                    userFlowerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    flowerId = table.Column<int>(type: "int", nullable: false),
                    state = table.Column<int>(type: "int", nullable: false),
                    active = table.Column<bool>(type: "bit", nullable: false),
                    position = table.Column<int>(type: "int", nullable: true),
                    userInterfaceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFlower", x => x.userFlowerId);
                    table.ForeignKey(
                        name: "FK_UserFlower_flowers_flowerId",
                        column: x => x.flowerId,
                        principalTable: "flowers",
                        principalColumn: "flowerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserFlower_userInterfaces_userInterfaceId",
                        column: x => x.userInterfaceId,
                        principalTable: "userInterfaces",
                        principalColumn: "userInterfaceId");
                });

            migrationBuilder.CreateTable(
                name: "UserSticker",
                columns: table => new
                {
                    userStickerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    stickerId = table.Column<int>(type: "int", nullable: false),
                    timesEarned = table.Column<int>(type: "int", nullable: false),
                    position = table.Column<int>(type: "int", nullable: true),
                    dayGiven = table.Column<DateTime>(type: "datetime2", nullable: false),
                    latestUpdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    userInterfaceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSticker", x => x.userStickerId);
                    table.ForeignKey(
                        name: "FK_UserSticker_stickers_stickerId",
                        column: x => x.stickerId,
                        principalTable: "stickers",
                        principalColumn: "stickerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSticker_userInterfaces_userInterfaceId",
                        column: x => x.userInterfaceId,
                        principalTable: "userInterfaces",
                        principalColumn: "userInterfaceId");
                });

            migrationBuilder.CreateTable(
                name: "Answer",
                columns: table => new
                {
                    answerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    answerText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    value = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    questionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answer", x => x.answerId);
                    table.ForeignKey(
                        name: "FK_Answer_Question_questionId",
                        column: x => x.questionId,
                        principalTable: "Question",
                        principalColumn: "questionId");
                });

            migrationBuilder.CreateTable(
                name: "patients",
                columns: table => new
                {
                    userId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    specialistuserId = table.Column<int>(type: "int", nullable: true),
                    syncDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    diaryId = table.Column<int>(type: "int", nullable: false),
                    achievementCollectionId = table.Column<int>(type: "int", nullable: true),
                    testId = table.Column<int>(type: "int", nullable: false),
                    settingsId = table.Column<int>(type: "int", nullable: false),
                    registerState = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    confirmed = table.Column<bool>(type: "bit", nullable: false),
                    codeHelper = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    progressId = table.Column<int>(type: "int", nullable: true),
                    userInterfaceId = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    bornDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    age = table.Column<int>(type: "int", nullable: false),
                    sex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    relationalToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    termsAndConditionsId = table.Column<int>(type: "int", nullable: false),
                    dateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patients", x => x.userId);
                    table.ForeignKey(
                        name: "FK_patients_achievementCollections_achievementCollectionId",
                        column: x => x.achievementCollectionId,
                        principalTable: "achievementCollections",
                        principalColumn: "achievementCollectionId");
                    table.ForeignKey(
                        name: "FK_patients_diaries_diaryId",
                        column: x => x.diaryId,
                        principalTable: "diaries",
                        principalColumn: "diaryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_patients_progresses_progressId",
                        column: x => x.progressId,
                        principalTable: "progresses",
                        principalColumn: "progressId");
                    table.ForeignKey(
                        name: "FK_patients_settings_settingsId",
                        column: x => x.settingsId,
                        principalTable: "settings",
                        principalColumn: "settingsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_patients_specialists_specialistuserId",
                        column: x => x.specialistuserId,
                        principalTable: "specialists",
                        principalColumn: "userId");
                    table.ForeignKey(
                        name: "FK_patients_terms_termsAndConditionsId",
                        column: x => x.termsAndConditionsId,
                        principalTable: "terms",
                        principalColumn: "termsAndConditionsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_patients_tests_testId",
                        column: x => x.testId,
                        principalTable: "tests",
                        principalColumn: "testId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_patients_userInterfaces_userInterfaceId",
                        column: x => x.userInterfaceId,
                        principalTable: "userInterfaces",
                        principalColumn: "userInterfaceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestInfoModel",
                columns: table => new
                {
                    testInfoModelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    result = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    visible = table.Column<bool>(type: "bit", nullable: false),
                    questionnairesHistoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestInfoModel", x => x.testInfoModelId);
                    table.ForeignKey(
                        name: "FK_TestInfoModel_questionnairesHistories_questionnairesHistoryId",
                        column: x => x.questionnairesHistoryId,
                        principalTable: "questionnairesHistories",
                        principalColumn: "questionnairesHistoryId");
                });

            migrationBuilder.CreateTable(
                name: "carts",
                columns: table => new
                {
                    cartId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    communityId = table.Column<int>(type: "int", nullable: false),
                    transmitterId = table.Column<int>(type: "int", nullable: false),
                    transmitterInitial = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    userType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    state = table.Column<int>(type: "int", nullable: false),
                    dateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PatientuserId = table.Column<int>(type: "int", nullable: true),
                    SpecialistuserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_carts", x => x.cartId);
                    table.ForeignKey(
                        name: "FK_carts_patients_PatientuserId",
                        column: x => x.PatientuserId,
                        principalTable: "patients",
                        principalColumn: "userId");
                    table.ForeignKey(
                        name: "FK_carts_specialists_SpecialistuserId",
                        column: x => x.SpecialistuserId,
                        principalTable: "specialists",
                        principalColumn: "userId");
                });

            migrationBuilder.CreateTable(
                name: "dates",
                columns: table => new
                {
                    dateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    hour = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    place = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    specialistNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    patientConfirm = table.Column<bool>(type: "bit", nullable: false),
                    specialistConfirm = table.Column<bool>(type: "bit", nullable: false),
                    done = table.Column<bool>(type: "bit", nullable: false),
                    patientuserId = table.Column<int>(type: "int", nullable: true),
                    status = table.Column<int>(type: "int", nullable: false),
                    sentBySpecialist = table.Column<bool>(type: "bit", nullable: false),
                    SpecialistuserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dates", x => x.dateId);
                    table.ForeignKey(
                        name: "FK_dates_patients_patientuserId",
                        column: x => x.patientuserId,
                        principalTable: "patients",
                        principalColumn: "userId");
                    table.ForeignKey(
                        name: "FK_dates_specialists_SpecialistuserId",
                        column: x => x.SpecialistuserId,
                        principalTable: "specialists",
                        principalColumn: "userId");
                });

            migrationBuilder.CreateTable(
                name: "goals",
                columns: table => new
                {
                    goalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    desription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    type = table.Column<int>(type: "int", nullable: false),
                    stickerId = table.Column<int>(type: "int", nullable: true),
                    flowerId = table.Column<int>(type: "int", nullable: true),
                    logoUlr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientuserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_goals", x => x.goalId);
                    table.ForeignKey(
                        name: "FK_goals_patients_PatientuserId",
                        column: x => x.PatientuserId,
                        principalTable: "patients",
                        principalColumn: "userId");
                });

            migrationBuilder.CreateTable(
                name: "notifications",
                columns: table => new
                {
                    notificationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    notificationType = table.Column<int>(type: "int", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    recomendationId = table.Column<int>(type: "int", nullable: true),
                    recomendationType = table.Column<int>(type: "int", nullable: true),
                    reference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    stickerId = table.Column<int>(type: "int", nullable: true),
                    achivementId = table.Column<int>(type: "int", nullable: true),
                    emitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PostponeUntil = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PatientuserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notifications", x => x.notificationId);
                    table.ForeignKey(
                        name: "FK_notifications_patients_PatientuserId",
                        column: x => x.PatientuserId,
                        principalTable: "patients",
                        principalColumn: "userId");
                });

            migrationBuilder.CreateTable(
                name: "PatientRequest",
                columns: table => new
                {
                    patientRequestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    patientuserId = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SpecialistuserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientRequest", x => x.patientRequestId);
                    table.ForeignKey(
                        name: "FK_PatientRequest_patients_patientuserId",
                        column: x => x.patientuserId,
                        principalTable: "patients",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientRequest_specialists_SpecialistuserId",
                        column: x => x.SpecialistuserId,
                        principalTable: "specialists",
                        principalColumn: "userId");
                });

            migrationBuilder.CreateTable(
                name: "recomendationComplete",
                columns: table => new
                {
                    recomendationCompleteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    recomendationId = table.Column<int>(type: "int", nullable: false),
                    dateCompleted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PatientuserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recomendationComplete", x => x.recomendationCompleteId);
                    table.ForeignKey(
                        name: "FK_recomendationComplete_patients_PatientuserId",
                        column: x => x.PatientuserId,
                        principalTable: "patients",
                        principalColumn: "userId");
                });

            migrationBuilder.CreateTable(
                name: "TestQuestionWithAnswer",
                columns: table => new
                {
                    testQuestionWithAnswerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    testInfoModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestQuestionWithAnswer", x => x.testQuestionWithAnswerId);
                    table.ForeignKey(
                        name: "FK_TestQuestionWithAnswer_TestInfoModel_testInfoModelId",
                        column: x => x.testInfoModelId,
                        principalTable: "TestInfoModel",
                        principalColumn: "testInfoModelId");
                });

            migrationBuilder.CreateTable(
                name: "cartAnswers",
                columns: table => new
                {
                    cartAnswerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cartId = table.Column<int>(type: "int", nullable: false),
                    receiverId = table.Column<int>(type: "int", nullable: false),
                    receiverInitial = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    stickerId = table.Column<int>(type: "int", nullable: true),
                    read = table.Column<bool>(type: "bit", nullable: false),
                    dateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cartAnswers", x => x.cartAnswerId);
                    table.ForeignKey(
                        name: "FK_cartAnswers_carts_cartId",
                        column: x => x.cartId,
                        principalTable: "carts",
                        principalColumn: "cartId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "dateRequests",
                columns: table => new
                {
                    dateRequestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CitadateId = table.Column<int>(type: "int", nullable: true),
                    dateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SpecialistuserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dateRequests", x => x.dateRequestId);
                    table.ForeignKey(
                        name: "FK_dateRequests_dates_CitadateId",
                        column: x => x.CitadateId,
                        principalTable: "dates",
                        principalColumn: "dateId");
                    table.ForeignKey(
                        name: "FK_dateRequests_specialists_SpecialistuserId",
                        column: x => x.SpecialistuserId,
                        principalTable: "specialists",
                        principalColumn: "userId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answer_questionId",
                table: "Answer",
                column: "questionId");

            migrationBuilder.CreateIndex(
                name: "IX_cartAnswers_cartId",
                table: "cartAnswers",
                column: "cartId");

            migrationBuilder.CreateIndex(
                name: "IX_carts_PatientuserId",
                table: "carts",
                column: "PatientuserId");

            migrationBuilder.CreateIndex(
                name: "IX_carts_SpecialistuserId",
                table: "carts",
                column: "SpecialistuserId");

            migrationBuilder.CreateIndex(
                name: "IX_completeQuestionnaires_testId",
                table: "completeQuestionnaires",
                column: "testId");

            migrationBuilder.CreateIndex(
                name: "IX_dateRequests_CitadateId",
                table: "dateRequests",
                column: "CitadateId");

            migrationBuilder.CreateIndex(
                name: "IX_dateRequests_SpecialistuserId",
                table: "dateRequests",
                column: "SpecialistuserId");

            migrationBuilder.CreateIndex(
                name: "IX_dates_patientuserId",
                table: "dates",
                column: "patientuserId");

            migrationBuilder.CreateIndex(
                name: "IX_dates_SpecialistuserId",
                table: "dates",
                column: "SpecialistuserId");

            migrationBuilder.CreateIndex(
                name: "IX_dummyUsers_badgeCollectionId",
                table: "dummyUsers",
                column: "badgeCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_goals_PatientuserId",
                table: "goals",
                column: "PatientuserId");

            migrationBuilder.CreateIndex(
                name: "IX_ImageModel_flowerId",
                table: "ImageModel",
                column: "flowerId");

            migrationBuilder.CreateIndex(
                name: "IX_notes_diaryId",
                table: "notes",
                column: "diaryId");

            migrationBuilder.CreateIndex(
                name: "IX_notes_emotionId",
                table: "notes",
                column: "emotionId");

            migrationBuilder.CreateIndex(
                name: "IX_notifications_PatientuserId",
                table: "notifications",
                column: "PatientuserId");

            migrationBuilder.CreateIndex(
                name: "IX_opps_bukayoSakaDiaryId",
                table: "opps",
                column: "bukayoSakaDiaryId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientRequest_patientuserId",
                table: "PatientRequest",
                column: "patientuserId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientRequest_SpecialistuserId",
                table: "PatientRequest",
                column: "SpecialistuserId");

            migrationBuilder.CreateIndex(
                name: "IX_patients_achievementCollectionId",
                table: "patients",
                column: "achievementCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_patients_diaryId",
                table: "patients",
                column: "diaryId");

            migrationBuilder.CreateIndex(
                name: "IX_patients_progressId",
                table: "patients",
                column: "progressId");

            migrationBuilder.CreateIndex(
                name: "IX_patients_settingsId",
                table: "patients",
                column: "settingsId");

            migrationBuilder.CreateIndex(
                name: "IX_patients_specialistuserId",
                table: "patients",
                column: "specialistuserId");

            migrationBuilder.CreateIndex(
                name: "IX_patients_termsAndConditionsId",
                table: "patients",
                column: "termsAndConditionsId");

            migrationBuilder.CreateIndex(
                name: "IX_patients_testId",
                table: "patients",
                column: "testId");

            migrationBuilder.CreateIndex(
                name: "IX_patients_userInterfaceId",
                table: "patients",
                column: "userInterfaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_questionnaireId",
                table: "Question",
                column: "questionnaireId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireResult_questionnaireId",
                table: "QuestionnaireResult",
                column: "questionnaireId");

            migrationBuilder.CreateIndex(
                name: "IX_questionnairesForUsers_testId",
                table: "questionnairesForUsers",
                column: "testId");

            migrationBuilder.CreateIndex(
                name: "IX_questionnairesHistories_testId",
                table: "questionnairesHistories",
                column: "testId");

            migrationBuilder.CreateIndex(
                name: "IX_recomendationComplete_PatientuserId",
                table: "recomendationComplete",
                column: "PatientuserId");

            migrationBuilder.CreateIndex(
                name: "IX_sakaNotes_BukayoId",
                table: "sakaNotes",
                column: "BukayoId");

            migrationBuilder.CreateIndex(
                name: "IX_specialists_termsAndConditionsId",
                table: "specialists",
                column: "termsAndConditionsId");

            migrationBuilder.CreateIndex(
                name: "IX_StageRequest_stageId",
                table: "StageRequest",
                column: "stageId");

            migrationBuilder.CreateIndex(
                name: "IX_TestInfoModel_questionnairesHistoryId",
                table: "TestInfoModel",
                column: "questionnairesHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TestQuestionWithAnswer_testInfoModelId",
                table: "TestQuestionWithAnswer",
                column: "testInfoModelId");

            migrationBuilder.CreateIndex(
                name: "IX_userAchievements_achievementCollectionId",
                table: "userAchievements",
                column: "achievementCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_userAchievements_achievementId",
                table: "userAchievements",
                column: "achievementId");

            migrationBuilder.CreateIndex(
                name: "IX_userBadges_badgeCollectionId",
                table: "userBadges",
                column: "badgeCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_userBadges_badgeId",
                table: "userBadges",
                column: "badgeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFlower_flowerId",
                table: "UserFlower",
                column: "flowerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFlower_userInterfaceId",
                table: "UserFlower",
                column: "userInterfaceId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSticker_stickerId",
                table: "UserSticker",
                column: "stickerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSticker_userInterfaceId",
                table: "UserSticker",
                column: "userInterfaceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answer");

            migrationBuilder.DropTable(
                name: "cartAnswers");

            migrationBuilder.DropTable(
                name: "completeQuestionnaires");

            migrationBuilder.DropTable(
                name: "dateRequests");

            migrationBuilder.DropTable(
                name: "dummyUsers");

            migrationBuilder.DropTable(
                name: "goals");

            migrationBuilder.DropTable(
                name: "ImageModel");

            migrationBuilder.DropTable(
                name: "inAppTexts");

            migrationBuilder.DropTable(
                name: "notes");

            migrationBuilder.DropTable(
                name: "notifications");

            migrationBuilder.DropTable(
                name: "opps");

            migrationBuilder.DropTable(
                name: "PatientRequest");

            migrationBuilder.DropTable(
                name: "QuestionnaireResult");

            migrationBuilder.DropTable(
                name: "questionnairesForUsers");

            migrationBuilder.DropTable(
                name: "Recomendation");

            migrationBuilder.DropTable(
                name: "recomendationComplete");

            migrationBuilder.DropTable(
                name: "sakaNotes");

            migrationBuilder.DropTable(
                name: "StageRequest");

            migrationBuilder.DropTable(
                name: "TestQuestionWithAnswer");

            migrationBuilder.DropTable(
                name: "userAchievements");

            migrationBuilder.DropTable(
                name: "userBadges");

            migrationBuilder.DropTable(
                name: "UserFlower");

            migrationBuilder.DropTable(
                name: "UserSticker");

            migrationBuilder.DropTable(
                name: "Question");

            migrationBuilder.DropTable(
                name: "carts");

            migrationBuilder.DropTable(
                name: "dates");

            migrationBuilder.DropTable(
                name: "emotions");

            migrationBuilder.DropTable(
                name: "bukayos");

            migrationBuilder.DropTable(
                name: "stages");

            migrationBuilder.DropTable(
                name: "TestInfoModel");

            migrationBuilder.DropTable(
                name: "achievements");

            migrationBuilder.DropTable(
                name: "badgeCollections");

            migrationBuilder.DropTable(
                name: "badges");

            migrationBuilder.DropTable(
                name: "flowers");

            migrationBuilder.DropTable(
                name: "stickers");

            migrationBuilder.DropTable(
                name: "questionnaires");

            migrationBuilder.DropTable(
                name: "patients");

            migrationBuilder.DropTable(
                name: "questionnairesHistories");

            migrationBuilder.DropTable(
                name: "achievementCollections");

            migrationBuilder.DropTable(
                name: "diaries");

            migrationBuilder.DropTable(
                name: "progresses");

            migrationBuilder.DropTable(
                name: "settings");

            migrationBuilder.DropTable(
                name: "specialists");

            migrationBuilder.DropTable(
                name: "userInterfaces");

            migrationBuilder.DropTable(
                name: "tests");

            migrationBuilder.DropTable(
                name: "terms");
        }
    }
}
