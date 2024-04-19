using Business.Contracts;
using Data.Contracts;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Implementations
{
    public class EspecialistaService : IEspecialistaService
    {
        private readonly IEspecialistaRepository _service;
        private readonly ICitaRepository _citaRepository;

        public EspecialistaService(IEspecialistaRepository service, ICitaRepository citaRepository)
        {
            _service = service;
            _citaRepository = citaRepository;
        }

        public int Add(AgregarEspecialista especialista)
        {
            if (especialista == null) { return 0; }
            return _service.Add(especialista);
        }

        public bool Delete(int id)
        {
            if (id < 1) { return false; }
            return _service.Delete(id);
        }

        public Especialista? Get(int id)
        {
            if (id < 1) return null;
            return _service.Get(id);
        }

        public bool Update(Especialista especialista)
        {
            if (especialista == null) { return false; }
            return _service.Update(especialista);
        }

        public string? GetByToken(int idPaciente)
        {
            if (idPaciente < 1) { return null; }
            return _service.GetByToken(idPaciente);
        }

        public int login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password)) { return 0; }
            return _service.login(email, password);
        }

        public bool vincularPaciente(int idSpecialist, string tokenPaciente)
        {
            if (idSpecialist < 1 || string.IsNullOrEmpty(tokenPaciente)) { return false; }
            return _service.vincularPaciente(idSpecialist, tokenPaciente);
        }

        public bool aceptarCita(int idEspecialista, int idCita)
        {
            if (idCita < 1 || idEspecialista < 1) { return false; }

            SolicitudCita solicitudCita = _citaRepository.GetSolicitudCita(idCita)!;

            if (solicitudCita == null) { return false; }

            bool vinculacion = _citaRepository.vincularCitaConEspecialista(idEspecialista, idCita);

            if (vinculacion)
            {
                _citaRepository.confirmarCitaPorEspecialista(idCita, idEspecialista);

                bool result = _citaRepository.eliminarSolicitudCita(solicitudCita.IdSolicitudCita);

                return result;
            }
            else
            {
                return false;
            }
        }

        public bool rechazarCita(int id, int idCita)
        {
            if(id < 1 || idCita < 1) { return false; }

            return _citaRepository.eliminarSolicitudCita(idCita);
        }

        public List<Paciente>? GetPacientes(int id)
        {
            if (id < 1) { return null; }
            return _service.GetPacientes(id);
        }

        public List<SolicitudCita>? GetSolicitudesCitas(int id)
        {
            if(id < 1) { return null; }
            return _citaRepository.GetSolicitudesCitas(id);
        }
    }
}