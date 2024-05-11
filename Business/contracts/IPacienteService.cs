using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Business.Contracts
{
    public interface IPacienteService
    {
        int Add(AgregarPaciente paciente);

        Paciente? Get(int id);

        bool Delete(int id);

        bool Update(Paciente paciente);

        bool VincularEspecialista(int id, string tokenEspecialista);

        string? GetByToken(int idPaciente);

        int login(string email, string password);
        bool MoficarConfiguracionNotificaciones(int id, bool notificacionesActivas, bool dirioActivado, bool progresoActivado);

        bool registerSet(int id);
    }
}
