using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Contracts
{
    public interface IDateRepository
    {
        int AddCita(Date cita, int idPaciente);

        bool UpdateCita(Date cita);

        bool DeleteCita(int idCita);

        Date? GetCita(int idCita);

        bool vincularCitaConPeciente(int idCita, int idPaciente);

        List<Date>? GetCitasPorPaciente(int idPaciente);

        bool confirmarCitaPorPaciente(int idCita, int idPaciente);

        bool cancelarCitaPorPaciente(int idCita, int idPaciente);

        bool confirmarCitaPorEspecialista(int idCita, int idEspecialista);

        bool cancelarCitaPorEspecialista(int idCita, int idEspecialista);

        int AddSolicitudCita(Date solicitudCita, int idE);

        bool vinvularSolicitudCitaConEspecialista(int idEspecialista, int idSolicitudCita);
        DateRequest? GetSolicitudCita(int idCita);
        bool vincularCitaConEspecialista(int idEspecialista, int idCita);
        bool eliminarSolicitudCita(int idCita);
        List<DateRequest>? GetSolicitudesCitas(int id);
        List<Date>? GetCitasPorEspecialista(int idSpecialist);

        bool UpdateStatusCita(Date cita);
    }
}
