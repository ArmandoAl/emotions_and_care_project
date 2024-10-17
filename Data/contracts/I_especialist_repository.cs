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

        string? GetByToken(int idPatient);

        int login(string email, string password);

        bool vincularPaciente(int idSpecialist, string tokenPatient);
        List<Patient>? GetPacientes(int id);
        List<Specialist> ListarEspecialistas(int offset, int limit);
    }
}
