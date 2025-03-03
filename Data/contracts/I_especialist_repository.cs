using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Contracts
{
    public interface ISpecialistRepository
    {
        int Add(AddSpecialist especialist);

        Specialist? Get(int id);

        bool Delete(int id);

        bool Update(Specialist especialist);

        Specialist? GetByToken(string relatedToken);

        int login(string email, string password);

        bool aceptarSolicitud(int idSpecialist, int pacientId);

        bool rechazarSolicitud(int idSpecialist, int pacientId);
        List<Patient>? GetPacientes(int id);
        List<Specialist> ListarEspecialistas(int offset, int limit);

        bool refreshToken(int id, string token);
    }
}
