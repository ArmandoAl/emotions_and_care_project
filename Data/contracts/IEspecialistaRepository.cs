using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Contracts
{
    public interface IEspecialistaRepository
    {
        int Add(AgregarEspecialista especialista);

        Especialista? Get(int id);

        bool Delete(int id);

        bool Update(Especialista especialista);

        string? GetByToken(int idPaciente);

        int login(string email, string password);

        bool vincularPaciente(int idSpecialist, string tokenPaciente);
        List<Paciente>? GetPacientes(int id);
    }
}
