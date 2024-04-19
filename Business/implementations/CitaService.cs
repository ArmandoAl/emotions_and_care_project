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
    public class CitaService : ICitaService
    {
      private readonly ICitaRepository _citaRepository;

        public CitaService(ICitaRepository citaRepository)
        {
            _citaRepository = citaRepository;
        }

        public int AddCita(Cita cita, int idPaciente, int idEspecialista)
        {
            if (cita == null) return 0;

            int idCita = _citaRepository.AddCita(cita, idPaciente);

            if (idCita > 0)
            {
                bool vinculacion = _citaRepository.vincularCitaConPeciente(idCita, idPaciente);
                if (!vinculacion)
                {
                    _citaRepository.DeleteCita(idCita);
                    return 0;
                }
                else
                {

                    int idSolicitudCita = _citaRepository.AddSolicitudCita(cita, idEspecialista);

                    if(idSolicitudCita == 0)
                    {
                        _citaRepository.DeleteCita(idCita);
                        return 0;
                    }   

                    return idCita;
                }
            }

            return 0;

        }

        public bool DeleteCita(int idCita)
        {
            if (idCita <= 0) return false;

            return _citaRepository.DeleteCita(idCita);
        }

        public Cita? GetCita(int idCita)
        {
            if (idCita <= 0) return null;

            return _citaRepository.GetCita(idCita);
        }

        public bool UpdateCita(Cita cita)
        {
            if (cita == null) return false;

            return _citaRepository.UpdateCita(cita);
        }

        public List<Cita>? GetCitasPorPaciente(int idPaciente)
        {
            if (idPaciente <= 0) return null;

            return _citaRepository.GetCitasPorPaciente(idPaciente);
        }

        public bool confirmarCitaPorPaciente(int idCita, int idPaciente)
        {
            if (idCita <= 0 || idPaciente <= 0) return false;

            return _citaRepository.confirmarCitaPorPaciente(idCita, idPaciente);
        }

        public bool cancelarCitaPorPaciente(int idCita, int idPaciente)
        {
            if (idCita <= 0 || idPaciente <= 0) return false;

            return _citaRepository.cancelarCitaPorPaciente(idCita, idPaciente);
        }

        public bool confirmarCitaPorEspecialista(int idCita, int idEspecialista)
        {
            if (idCita <= 0 || idEspecialista <= 0) return false;

            return _citaRepository.confirmarCitaPorEspecialista(idCita, idEspecialista);
        }

        public bool cancelarCitaPorEspecialista(int idCita, int idEspecialista)
        {
            if (idCita <= 0 || idEspecialista <= 0) return false;

            return _citaRepository.cancelarCitaPorEspecialista(idCita, idEspecialista);
        }

        public List<Cita>? GetCitasPorEspecialista(int idSpecialist)
        {
            if(idSpecialist <= 0) return null;

            return _citaRepository.GetCitasPorEspecialista(idSpecialist);
        }

        public int AddDateSpecialist(Cita cita, int idEspecialista, int idPaciente)
        {
            if (cita == null) return 0;

            int idCita = _citaRepository.AddCita(cita, idPaciente);

            if (idCita > 0)
            {
                bool vinculacion = _citaRepository.vincularCitaConPeciente(idCita, idPaciente);
                if (!vinculacion)
                {
                    _citaRepository.DeleteCita(idCita);
                    return 0;
                }
                bool vinc = _citaRepository.vincularCitaConEspecialista(idEspecialista, idCita);
                
                if(vinc)
                {
                    return idCita;
                }
                else
                {
                    _citaRepository.DeleteCita(idCita);
                    return 0;
                }
            }
            return 0;

        }
    }
}

