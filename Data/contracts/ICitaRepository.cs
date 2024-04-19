using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Contracts
{
    public interface ICitaRepository
    {
        int AddCita(Cita cita, int idPaciente);

        bool UpdateCita(Cita cita);

        bool DeleteCita(int idCita);

        Cita? GetCita(int idCita);

        bool vincularCitaConPeciente(int idCita, int idPaciente);

        List<Cita>? GetCitasPorPaciente(int idPaciente);

        bool confirmarCitaPorPaciente(int idCita, int idPaciente);

        bool cancelarCitaPorPaciente(int idCita, int idPaciente);

        bool confirmarCitaPorEspecialista(int idCita, int idEspecialista);

        bool cancelarCitaPorEspecialista(int idCita, int idEspecialista);

        int AddSolicitudCita(Cita solicitudCita, int idE);

        bool vinvularSolicitudCitaConEspecialista(int idEspecialista, int idSolicitudCita);
        SolicitudCita? GetSolicitudCita(int idCita);
        bool vincularCitaConEspecialista(int idEspecialista, int idCita);
        bool eliminarSolicitudCita(int idCita);
        List<SolicitudCita>? GetSolicitudesCitas(int id);
        List<Cita>? GetCitasPorEspecialista(int idSpecialist);
    }
}
