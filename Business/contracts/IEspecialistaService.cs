using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Contracts
{
    public interface IEspecialistaService
    {
        int Add(AgregarEspecialista especialista);

        Especialista? Get(int id);

        bool Delete(int id);

        bool Update(Especialista especialista);

        string? GetByToken(int idPaciente);

        int login(string email, string password);

        bool vincularPaciente(int idSpecialist, string tokenPaciente);

        bool aceptarCita(int idEspecialista, int idCita);
        bool rechazarCita(int id, int idCita);
        List<Paciente>? GetPacientes(int id);
        List<SolicitudCita>? GetSolicitudesCitas(int id);
    }
}
