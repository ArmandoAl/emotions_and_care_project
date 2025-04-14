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

        int Update(Specialist especialist);

        Specialist? GetByToken(string relatedToken);

        int login(string email, string password);

        bool aceptarSolicitud(int idSpecialist, int pacientId);

        bool rechazarSolicitud(int idSpecialist, int pacientId);

        bool aceptarCita(int idEspecialist, int idCita);
        bool rechazarCita(int id, int idCita);
        List<Patient>? GetPacientes(int id);
        List<DateRequest>? GetSolicitudesCitas(int id);
        List<Specialist> ListarEspecialists(int offset, int limit);

        bool refreshToken(int id, string token);

        List<PatientRequest>? GetSolicitudesPacientes(int id);
    }
}

