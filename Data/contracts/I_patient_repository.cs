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
        int AgregarFlorInicial(int id);
        int agregarStickerDeUsuarioModel(int? index, int idUsuario);

        string registerSet(int patientId, string state);
    }
}