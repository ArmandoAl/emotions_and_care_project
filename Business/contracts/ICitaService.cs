using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Contracts
{
    public interface ICitaService
    {
        LogroWithCita? AddCita(Cita cita, int idPaciente, int idEspacialista, bool isFirtTime);

        bool UpdateCita(Cita cita);

        bool DeleteCita(int idCita);

        Cita? GetCita(int idCita);

        List<Cita>? GetCitasPorPaciente(int idPaciente);

        bool confirmarCitaPorPaciente(int idCita, int idPaciente);

        bool cancelarCitaPorPaciente(int idCita, int idPaciente);

        bool confirmarCitaPorEspecialista(int idCita, int idEspecialista);

        bool cancelarCitaPorEspecialista(int idCita, int idEspecialista);
        List<Cita>? GetCitasPorEspecialista(int idSpecialist);
        int AddDateSpecialist(Cita cita, int idEspecialista, int idPaciente);
    }
}
