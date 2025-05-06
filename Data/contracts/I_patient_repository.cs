using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Contracts
{
    public interface IPatientRepository
    {
        int Add(AddPatient paciente);

        Patient? Get(int id);

        bool Delete(int id);

        bool Update(Patient paciente);
        bool VincularEspecialista(int id, string tokenEspecialista);

        string? GetByToken(int idPaciente);

        int login (string email, string password);
        bool MoficarConfiguracionNotificaciones(int id, bool notificacionesActivas, bool dirioActivado, bool progresoActivado);
        int AgregarFlorInicial(int id, 
            int patientId
        );
        int agregarStickerDeUsuarioModel(int? index, int idUsuario);

        string registerSet(int patientId, string state);


        int putStickeriInInterface(int idPatient, int idUserSticker, int position);

        int putFlowerInInterface(int idPatient, int idUserFlower, int position);


        bool canGrowFlower(int idPatient);

        bool growStage(int idPatient);

        bool reviewCanCheck(int idPatient);

        List<StageProgressInfo> GetStageProgress(int idPatient);

        List<StageInfoResponse> GetAllStagesProgress(int idPatient);

        bool updateLastProgressDate(int idPatient); 

        bool refreshToken(int id, string token);

         bool VincularDirecto(int id, string tokenEspecialista);

         bool actualizarThemeId(int id, int themeId);

         bool addAchievementToPatient(int idPatient, int idAchievement);

         AchievementCollection? getAllAchievements(int idPatient);

         bool createAchievementCollection(int idPatient);

         bool AddAllAchievementsToPatient(int idPatient);

         List<progressBool>? CheckAchievements(int idPatient);
         
        int? giveAchievementToPatient(int idPatient, int achievementId);

        int? checkAchievement_Questionnaires(int idUsuario);

        int? checkAchievement_Community(int idUsuario);

        int? checkAchievement_Diary(int idUsuario);

        int? checkAchievement_Agenda(int idUsuario);

        bool removeStickerInInterface(int idPatient, int position);

        bool actualizarBackgroundId(int id, int backgroundId);


        bool SoftDelete(int id);

        bool ConfirmarUsuario(int id);

        Patient? GetByEmail(string email);

        string GetForgotPassword(int id);

        bool ValidarCodigo(int id, string codigo);

        bool ModificarContraseña(int id, string password);

        

    }
}