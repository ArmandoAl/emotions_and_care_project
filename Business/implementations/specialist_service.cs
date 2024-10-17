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
    public class EspecialistaService : ISpecialistService
    {
        private readonly ISpecialistRepository _service;
        private readonly IDateRepository _citaRepository;

        public EspecialistaService(ISpecialistRepository service, IDateRepository citaRepository)
        {
            _service = service;
            _citaRepository = citaRepository;
        }

        public int Add(AddSpecialist especialista)
        {
            if (especialista == null) { return 0; }
            return _service.Add(especialista);
        }

        public bool Delete(int id)
        {
            if (id < 1) { return false; }
            return _service.Delete(id);
        }

        public Specialist? Get(int id)
        {
            if (id < 1) return null;
            return _service.Get(id);
        }

        public bool Update(Specialist especialista)
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

            DateRequest solicitudCita = _citaRepository.GetSolicitudCita(idCita)!;

            if (solicitudCita == null) { return false; }

            bool vinculacion = _citaRepository.vincularCitaConEspecialista(idEspecialista, idCita);

            if (vinculacion)
            {
                _citaRepository.confirmarCitaPorEspecialista(idCita, idEspecialista);

                bool result = _citaRepository.eliminarSolicitudCita(solicitudCita.dateRequestId);

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

        public List<Patient>? GetPacientes(int id)
        {
            if (id < 1) { return null; }
            return _service.GetPacientes(id);
        }

        public List<DateRequest>? GetSolicitudesCitas(int id)
        {
            if(id < 1) { return null; }
            return _citaRepository.GetSolicitudesCitas(id);
        }

        public List<Specialist> ListarEspecialists(int offset, int limit)
        {
            if (offset < 0 || limit < 1) { return new List<Specialist>(); }
            return _service.ListarEspecialistas(offset, limit);
        }
    }
}