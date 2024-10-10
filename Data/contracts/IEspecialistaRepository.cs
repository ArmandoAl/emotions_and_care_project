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
        int Add(AddSpecialist especialista);

        Specialist? Get(int id);

        bool Delete(int id);

        bool Update(Specialist especialista);

        string? GetByToken(int idPaciente);

        int login(string email, string password);

        bool vincularPaciente(int idSpecialist, string tokenPaciente);
        List<Patient>? GetPacientes(int id);
        List<Specialist> ListarEspecialistas(int offset, int limit);
    }
}
