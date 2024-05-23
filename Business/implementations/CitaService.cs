using Business.Contracts;
using Data.contracts;
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
      private readonly ILogroRepository _logroRepository;

        public CitaService(ICitaRepository citaRepository, ILogroRepository logroRepositor)
        {
            _citaRepository = citaRepository;
            _logroRepository = logroRepositor;
        }

        public LogroWithCita? AddCita(Cita cita, int idPaciente, int idEspecialista, bool isFirtTime)
        {
            if (cita == null) return null;

            int idCita = _citaRepository.AddCita(cita, idPaciente);

            if (idCita > 0)
            {
                bool vinculacion = _citaRepository.vincularCitaConPeciente(idCita, idPaciente);
                if (!vinculacion)
                {
                    _citaRepository.DeleteCita(idCita);
                    return null;
                }
                else
                {

                    int idSolicitudCita = _citaRepository.AddSolicitudCita(cita, idEspecialista);

                    if(idSolicitudCita == 0)
                    {
                        _citaRepository.DeleteCita(idCita);
                        return null;
                    }

                    if(isFirtTime)
                    {
                        var idLogro = _logroRepository.AgregarLogroAPaciente(idPaciente, 6);
                        if (idLogro <= 0)
                        {
                            _citaRepository.DeleteCita(idCita);
                            return null;
                        }

                        return new LogroWithCita
                        {
                            Logro = _logroRepository.GetLogro(idLogro),
                            IdCita = idCita
                        };

                    } else {
                        return new LogroWithCita
                        {
                            Logro = null,
                            IdCita = idCita
                        };
                    }   
                }
            }

            return null;

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

