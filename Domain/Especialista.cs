using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Especialista : Usuario
    {
        public string CedulaProfesional { get; set; } = "";

        public List<Carta> CartasDeComunidad { get; set; } = new List<Carta>();

        public List<Cita> Citas { get; set; } = new List<Cita>();

        public List<Paciente> Pacientes { get; set; } = new List<Paciente>();

        public List<SolicitudCita> SolicitudesCita { get; set; } = new List<SolicitudCita>();

        public List<SolicitudPaciente> solicitudPacientes { get; set; } = new List<SolicitudPaciente>();

    }
}
