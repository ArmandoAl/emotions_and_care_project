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
        int Add(AddSpecialist especialista);

        Specialist? Get(int id);

        bool Delete(int id);

        bool Update(Specialist especialista);

        string? GetByToken(int idPaciente);

        int login(string email, string password);

        bool vincularPaciente(int idSpecialist, string tokenPaciente);

        bool aceptarCita(int idEspecialista, int idCita);
        bool rechazarCita(int id, int idCita);
        List<Patient>? GetPacientes(int id);
        List<DateRequest>? GetSolicitudesCitas(int id);
        List<Specialist> ListarEspecialistas(int offset, int limit);
    }
}
