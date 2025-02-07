using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Business.Contracts
{
    public interface ISpecialistService
    {
        int Add(AddSpecialist especialist);

        Specialist? Get(int id);

        bool Delete(int id);

        bool Update(Specialist especialist);

        string? GetByToken(int idPaciente);

        int login(string email, string password);

        bool vincularPaciente(int idSpecialist, string tokenPaciente);

        bool aceptarCita(int idEspecialist, int idCita);
        bool rechazarCita(int id, int idCita);
        List<Patient>? GetPacientes(int id);
        List<DateRequest>? GetSolicitudesCitas(int id);
        List<Specialist> ListarEspecialists(int offset, int limit);

        bool refreshToken(int id, string token);
    }
}
