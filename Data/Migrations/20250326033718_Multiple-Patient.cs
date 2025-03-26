using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class MultiplePatient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompleteQuestionnaires_Test_testId",
                table: "CompleteQuestionnaires");

            migrationBuilder.DropForeignKey(
                name: "FK_patients_Progress_progressId",
                table: "patients");

            migrationBuilder.DropForeignKey(
                name: "FK_patients_SettingsP_settingsId",
                table: "patients");

            migrationBuilder.DropForeignKey(
                name: "FK_patients_Test_testId",
                table: "patients");

            migrationBuilder.DropForeignKey(
                name: "FK_patients_UserInterface_userInterfaceId",
                table: "patients");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionnaireForUser_Test_testId",
                table: "QuestionnaireForUser");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionnairesHistory_Test_testId",
                table: "QuestionnairesHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_TestInfoModel_QuestionnairesHistory_questionnairesHistoryId",
                table: "TestInfoModel");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFlower_UserInterface_userInterfaceId",
                table: "UserFlower");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSticker_UserInterface_userInterfaceId",
                table: "UserSticker");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompleteQuestionnaires",
                table: "CompleteQuestionnaires");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserInterface",
                table: "UserInterface");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Test",
                table: "Test");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SettingsP",
                table: "SettingsP");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionnairesHistory",
                table: "QuestionnairesHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionnaireForUser",
                table: "QuestionnaireForUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Progress",
                table: "Progress");

            migrationBuilder.RenameTable(
                name: "CompleteQuestionnaires",
                newName: "completeQuestionnaires");

            migrationBuilder.RenameTable(
                name: "UserInterface",
                newName: "userInterfaces");

            migrationBuilder.RenameTable(
                name: "Test",
                newName: "tests");

            migrationBuilder.RenameTable(
                name: "SettingsP",
                newName: "settings");

            migrationBuilder.RenameTable(
                name: "QuestionnairesHistory",
                newName: "questionnairesHistories");

            migrationBuilder.RenameTable(
                name: "QuestionnaireForUser",
                newName: "questionnairesForUsers");

            migrationBuilder.RenameTable(
                name: "Progress",
                newName: "progresses");

            migrationBuilder.RenameIndex(
                name: "IX_CompleteQuestionnaires_testId",
                table: "completeQuestionnaires",
                newName: "IX_completeQuestionnaires_testId");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionnairesHistory_testId",
                table: "questionnairesHistories",
                newName: "IX_questionnairesHistories_testId");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionnaireForUser_testId",
                table: "questionnairesForUsers",
                newName: "IX_questionnairesForUsers_testId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_completeQuestionnaires",
                table: "completeQuestionnaires",
                column: "completeQuestionnaireId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_userInterfaces",
                table: "userInterfaces",
                column: "userInterfaceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tests",
                table: "tests",
                column: "testId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_settings",
                table: "settings",
                column: "settingsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_questionnairesHistories",
                table: "questionnairesHistories",
                column: "questionnairesHistoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_questionnairesForUsers",
                table: "questionnairesForUsers",
                column: "questionnaireForUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_progresses",
                table: "progresses",
                column: "progressId");

            migrationBuilder.AddForeignKey(
                name: "FK_completeQuestionnaires_tests_testId",
                table: "completeQuestionnaires",
                column: "testId",
                principalTable: "tests",
                principalColumn: "testId");

            migrationBuilder.AddForeignKey(
                name: "FK_patients_progresses_progressId",
                table: "patients",
                column: "progressId",
                principalTable: "progresses",
                principalColumn: "progressId");

            migrationBuilder.AddForeignKey(
                name: "FK_patients_settings_settingsId",
                table: "patients",
                column: "settingsId",
                principalTable: "settings",
                principalColumn: "settingsId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_patients_tests_testId",
                table: "patients",
                column: "testId",
                principalTable: "tests",
                principalColumn: "testId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_patients_userInterfaces_userInterfaceId",
                table: "patients",
                column: "userInterfaceId",
                principalTable: "userInterfaces",
                principalColumn: "userInterfaceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_questionnairesForUsers_tests_testId",
                table: "questionnairesForUsers",
                column: "testId",
                principalTable: "tests",
                principalColumn: "testId");

            migrationBuilder.AddForeignKey(
                name: "FK_questionnairesHistories_tests_testId",
                table: "questionnairesHistories",
                column: "testId",
                principalTable: "tests",
                principalColumn: "testId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestInfoModel_questionnairesHistories_questionnairesHistoryId",
                table: "TestInfoModel",
                column: "questionnairesHistoryId",
                principalTable: "questionnairesHistories",
                principalColumn: "questionnairesHistoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFlower_userInterfaces_userInterfaceId",
                table: "UserFlower",
                column: "userInterfaceId",
                principalTable: "userInterfaces",
                principalColumn: "userInterfaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSticker_userInterfaces_userInterfaceId",
                table: "UserSticker",
                column: "userInterfaceId",
                principalTable: "userInterfaces",
                principalColumn: "userInterfaceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_completeQuestionnaires_tests_testId",
                table: "completeQuestionnaires");

            migrationBuilder.DropForeignKey(
                name: "FK_patients_progresses_progressId",
                table: "patients");

            migrationBuilder.DropForeignKey(
                name: "FK_patients_settings_settingsId",
                table: "patients");

            migrationBuilder.DropForeignKey(
                name: "FK_patients_tests_testId",
                table: "patients");

            migrationBuilder.DropForeignKey(
                name: "FK_patients_userInterfaces_userInterfaceId",
                table: "patients");

            migrationBuilder.DropForeignKey(
                name: "FK_questionnairesForUsers_tests_testId",
                table: "questionnairesForUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_questionnairesHistories_tests_testId",
                table: "questionnairesHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_TestInfoModel_questionnairesHistories_questionnairesHistoryId",
                table: "TestInfoModel");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFlower_userInterfaces_userInterfaceId",
                table: "UserFlower");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSticker_userInterfaces_userInterfaceId",
                table: "UserSticker");

            migrationBuilder.DropPrimaryKey(
                name: "PK_completeQuestionnaires",
                table: "completeQuestionnaires");

            migrationBuilder.DropPrimaryKey(
                name: "PK_userInterfaces",
                table: "userInterfaces");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tests",
                table: "tests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_settings",
                table: "settings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_questionnairesHistories",
                table: "questionnairesHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_questionnairesForUsers",
                table: "questionnairesForUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_progresses",
                table: "progresses");

            migrationBuilder.RenameTable(
                name: "completeQuestionnaires",
                newName: "CompleteQuestionnaires");

            migrationBuilder.RenameTable(
                name: "userInterfaces",
                newName: "UserInterface");

            migrationBuilder.RenameTable(
                name: "tests",
                newName: "Test");

            migrationBuilder.RenameTable(
                name: "settings",
                newName: "SettingsP");

            migrationBuilder.RenameTable(
                name: "questionnairesHistories",
                newName: "QuestionnairesHistory");

            migrationBuilder.RenameTable(
                name: "questionnairesForUsers",
                newName: "QuestionnaireForUser");

            migrationBuilder.RenameTable(
                name: "progresses",
                newName: "Progress");

            migrationBuilder.RenameIndex(
                name: "IX_completeQuestionnaires_testId",
                table: "CompleteQuestionnaires",
                newName: "IX_CompleteQuestionnaires_testId");

            migrationBuilder.RenameIndex(
                name: "IX_questionnairesHistories_testId",
                table: "QuestionnairesHistory",
                newName: "IX_QuestionnairesHistory_testId");

            migrationBuilder.RenameIndex(
                name: "IX_questionnairesForUsers_testId",
                table: "QuestionnaireForUser",
                newName: "IX_QuestionnaireForUser_testId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompleteQuestionnaires",
                table: "CompleteQuestionnaires",
                column: "completeQuestionnaireId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserInterface",
                table: "UserInterface",
                column: "userInterfaceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Test",
                table: "Test",
                column: "testId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SettingsP",
                table: "SettingsP",
                column: "settingsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionnairesHistory",
                table: "QuestionnairesHistory",
                column: "questionnairesHistoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionnaireForUser",
                table: "QuestionnaireForUser",
                column: "questionnaireForUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Progress",
                table: "Progress",
                column: "progressId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompleteQuestionnaires_Test_testId",
                table: "CompleteQuestionnaires",
                column: "testId",
                principalTable: "Test",
                principalColumn: "testId");

            migrationBuilder.AddForeignKey(
                name: "FK_patients_Progress_progressId",
                table: "patients",
                column: "progressId",
                principalTable: "Progress",
                principalColumn: "progressId");

            migrationBuilder.AddForeignKey(
                name: "FK_patients_SettingsP_settingsId",
                table: "patients",
                column: "settingsId",
                principalTable: "SettingsP",
                principalColumn: "settingsId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_patients_Test_testId",
                table: "patients",
                column: "testId",
                principalTable: "Test",
                principalColumn: "testId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_patients_UserInterface_userInterfaceId",
                table: "patients",
                column: "userInterfaceId",
                principalTable: "UserInterface",
                principalColumn: "userInterfaceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionnaireForUser_Test_testId",
                table: "QuestionnaireForUser",
                column: "testId",
                principalTable: "Test",
                principalColumn: "testId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionnairesHistory_Test_testId",
                table: "QuestionnairesHistory",
                column: "testId",
                principalTable: "Test",
                principalColumn: "testId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestInfoModel_QuestionnairesHistory_questionnairesHistoryId",
                table: "TestInfoModel",
                column: "questionnairesHistoryId",
                principalTable: "QuestionnairesHistory",
                principalColumn: "questionnairesHistoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFlower_UserInterface_userInterfaceId",
                table: "UserFlower",
                column: "userInterfaceId",
                principalTable: "UserInterface",
                principalColumn: "userInterfaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSticker_UserInterface_userInterfaceId",
                table: "UserSticker",
                column: "userInterfaceId",
                principalTable: "UserInterface",
                principalColumn: "userInterfaceId");
        }
    }
}
