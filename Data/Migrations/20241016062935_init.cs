using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Diary",
                columns: table => new
                {
                    diaryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diary", x => x.diaryId);
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
                name: "SettingsP",
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
                    table.PrimaryKey("PK_SettingsP", x => x.settingsId);
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
                name: "Test",
                columns: table => new
                {
                    testId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Test", x => x.testId);
                });

            migrationBuilder.CreateTable(
                name: "UserInterface",
                columns: table => new
                {
                    userInterfaceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    backgroundUrl = table.Column<int>(type: "int", nullable: false),
                    themeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInterface", x => x.userInterfaceId);
                });

            migrationBuilder.CreateTable(
                name: "Note",
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
                    table.PrimaryKey("PK_Note", x => x.noteId);
                    table.ForeignKey(
                        name: "FK_Note_Diary_diaryId",
                        column: x => x.diaryId,
                        principalTable: "Diary",
                        principalColumn: "diaryId");
                    table.ForeignKey(
                        name: "FK_Note_emotions_emotionId",
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
                name: "CompleteQuestionnaires",
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
                    table.PrimaryKey("PK_CompleteQuestionnaires", x => x.completeQuestionnaireId);
                    table.ForeignKey(
                        name: "FK_CompleteQuestionnaires_Test_testId",
                        column: x => x.testId,
                        principalTable: "Test",
                        principalColumn: "testId");
                });

            migrationBuilder.CreateTable(
                name: "QuestionnaireForUser",
                columns: table => new
                {
                    questionnaireForUserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    questionnaireId = table.Column<int>(type: "int", nullable: false),
                    testId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionnaireForUser", x => x.questionnaireForUserId);
                    table.ForeignKey(
                        name: "FK_QuestionnaireForUser_Test_testId",
                        column: x => x.testId,
                        principalTable: "Test",
                        principalColumn: "testId");
                });

            migrationBuilder.CreateTable(
                name: "QuestionnairesHistory",
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
                    table.PrimaryKey("PK_QuestionnairesHistory", x => x.questionnairesHistoryId);
                    table.ForeignKey(
                        name: "FK_QuestionnairesHistory_Test_testId",
                        column: x => x.testId,
                        principalTable: "Test",
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
                        name: "FK_UserFlower_UserInterface_userInterfaceId",
                        column: x => x.userInterfaceId,
                        principalTable: "UserInterface",
                        principalColumn: "userInterfaceId");
                });

            migrationBuilder.CreateTable(
                name: "UserSticker",
                columns: table => new
                {
                    userStickerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    stickerId = table.Column<int>(type: "int", nullable: false),
                    position = table.Column<int>(type: "int", nullable: true),
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
                        name: "FK_UserSticker_UserInterface_userInterfaceId",
                        column: x => x.userInterfaceId,
                        principalTable: "UserInterface",
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
                    diaryId = table.Column<int>(type: "int", nullable: false),
                    testId = table.Column<int>(type: "int", nullable: false),
                    settingsId = table.Column<int>(type: "int", nullable: false),
                    registerState = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                        name: "FK_patients_Diary_diaryId",
                        column: x => x.diaryId,
                        principalTable: "Diary",
                        principalColumn: "diaryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_patients_SettingsP_settingsId",
                        column: x => x.settingsId,
                        principalTable: "SettingsP",
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
                        name: "FK_patients_Test_testId",
                        column: x => x.testId,
                        principalTable: "Test",
                        principalColumn: "testId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_patients_UserInterface_userInterfaceId",
                        column: x => x.userInterfaceId,
                        principalTable: "UserInterface",
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
                        name: "FK_TestInfoModel_QuestionnairesHistory_questionnairesHistoryId",
                        column: x => x.questionnairesHistoryId,
                        principalTable: "QuestionnairesHistory",
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
                    patientConfirm = table.Column<bool>(type: "bit", nullable: false),
                    specialistConfirm = table.Column<bool>(type: "bit", nullable: false),
                    done = table.Column<bool>(type: "bit", nullable: false),
                    patientuserId = table.Column<int>(type: "int", nullable: true),
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
                    emitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                name: "patientRequest",
                columns: table => new
                {
                    patientRequestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    patientuserId = table.Column<int>(type: "int", nullable: false),
                    SpecialistuserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patientRequest", x => x.patientRequestId);
                    table.ForeignKey(
                        name: "FK_patientRequest_patients_patientuserId",
                        column: x => x.patientuserId,
                        principalTable: "patients",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_patientRequest_specialists_SpecialistuserId",
                        column: x => x.SpecialistuserId,
                        principalTable: "specialists",
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
                name: "CartAnswer",
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
                    table.PrimaryKey("PK_CartAnswer", x => x.cartAnswerId);
                    table.ForeignKey(
                        name: "FK_CartAnswer_carts_cartId",
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
                name: "IX_CartAnswer_cartId",
                table: "CartAnswer",
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
                name: "IX_CompleteQuestionnaires_testId",
                table: "CompleteQuestionnaires",
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
                name: "IX_goals_PatientuserId",
                table: "goals",
                column: "PatientuserId");

            migrationBuilder.CreateIndex(
                name: "IX_ImageModel_flowerId",
                table: "ImageModel",
                column: "flowerId");

            migrationBuilder.CreateIndex(
                name: "IX_Note_diaryId",
                table: "Note",
                column: "diaryId");

            migrationBuilder.CreateIndex(
                name: "IX_Note_emotionId",
                table: "Note",
                column: "emotionId");

            migrationBuilder.CreateIndex(
                name: "IX_notifications_PatientuserId",
                table: "notifications",
                column: "PatientuserId");

            migrationBuilder.CreateIndex(
                name: "IX_patientRequest_patientuserId",
                table: "patientRequest",
                column: "patientuserId");

            migrationBuilder.CreateIndex(
                name: "IX_patientRequest_SpecialistuserId",
                table: "patientRequest",
                column: "SpecialistuserId");

            migrationBuilder.CreateIndex(
                name: "IX_patients_diaryId",
                table: "patients",
                column: "diaryId");

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
                name: "IX_QuestionnaireForUser_testId",
                table: "QuestionnaireForUser",
                column: "testId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireResult_questionnaireId",
                table: "QuestionnaireResult",
                column: "questionnaireId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnairesHistory_testId",
                table: "QuestionnairesHistory",
                column: "testId");

            migrationBuilder.CreateIndex(
                name: "IX_specialists_termsAndConditionsId",
                table: "specialists",
                column: "termsAndConditionsId");

            migrationBuilder.CreateIndex(
                name: "IX_TestInfoModel_questionnairesHistoryId",
                table: "TestInfoModel",
                column: "questionnairesHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TestQuestionWithAnswer_testInfoModelId",
                table: "TestQuestionWithAnswer",
                column: "testInfoModelId");

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
                name: "CartAnswer");

            migrationBuilder.DropTable(
                name: "CompleteQuestionnaires");

            migrationBuilder.DropTable(
                name: "dateRequests");

            migrationBuilder.DropTable(
                name: "goals");

            migrationBuilder.DropTable(
                name: "ImageModel");

            migrationBuilder.DropTable(
                name: "Note");

            migrationBuilder.DropTable(
                name: "notifications");

            migrationBuilder.DropTable(
                name: "patientRequest");

            migrationBuilder.DropTable(
                name: "QuestionnaireForUser");

            migrationBuilder.DropTable(
                name: "QuestionnaireResult");

            migrationBuilder.DropTable(
                name: "Recomendation");

            migrationBuilder.DropTable(
                name: "TestQuestionWithAnswer");

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
                name: "TestInfoModel");

            migrationBuilder.DropTable(
                name: "flowers");

            migrationBuilder.DropTable(
                name: "stickers");

            migrationBuilder.DropTable(
                name: "questionnaires");

            migrationBuilder.DropTable(
                name: "patients");

            migrationBuilder.DropTable(
                name: "QuestionnairesHistory");

            migrationBuilder.DropTable(
                name: "Diary");

            migrationBuilder.DropTable(
                name: "SettingsP");

            migrationBuilder.DropTable(
                name: "specialists");

            migrationBuilder.DropTable(
                name: "UserInterface");

            migrationBuilder.DropTable(
                name: "Test");

            migrationBuilder.DropTable(
                name: "terms");
        }
    }
}
