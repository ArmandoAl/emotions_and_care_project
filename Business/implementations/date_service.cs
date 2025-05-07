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
    public class DateService : IDateService
    {
      private readonly IDateRepository _DateRepository;
      private readonly IGoalRepository _logroRepository;

      private readonly IItemsRepository _itemsRepository;
      private readonly IPatientRepository _patientRepository;

        public DateService(IDateRepository DateRepository, IGoalRepository logroRepositor, IItemsRepository itemsRepository, 
            IPatientRepository patientRepository)
        {
            _DateRepository = DateRepository;
            _logroRepository = logroRepositor;
            _itemsRepository = itemsRepository;
            _patientRepository = patientRepository;
        }

        public AchievementWithDate? AddDate(Date date, int idPaciente, int idEspecialista)
        {
            if (date == null) return null;

            int idDate = _DateRepository.AddCita(date, idPaciente);

            Date? cita = _DateRepository.GetCita(idDate);

            if (idDate > 0)
            {
                bool vinculacion = _DateRepository.vincularCitaConPeciente(idDate, idPaciente);
          

                if (!vinculacion)
                {
                    _DateRepository.DeleteCita(idDate);
                    return null;
                }
                else
                {

                    int idSolicitudDate = _DateRepository.AddSolicitudCita(cita!, idEspecialista);

                    if(idSolicitudDate == 0)
                    {
                       
                        _DateRepository.cancelarCitaPorEspecialista(idDate, idEspecialista);
                        return null;
                    }


                    var idLogro = _patientRepository.checkAchievement_Agenda(idPaciente);

                    if (idLogro > 0)
                    {
                        return new AchievementWithDate
                        {
                            achievementId = idLogro,
                            dateId = idDate
                        };
                    }
                    else
                    {
                        return new AchievementWithDate
                        {
                            achievementId = 0,
                            dateId = idDate
                        };
                    }   
                }
            }

            return null;

        }

        public bool DeleteDate(int idDate)
        {
            if (idDate <= 0) return false;

            return _DateRepository.DeleteCita(idDate);
        }

        public Date? GetDate(int idDate)
        {
            if (idDate <= 0) return null;

            return _DateRepository.GetCita(idDate);
        }

        public bool UpdateDate(Date Date)
        {
            if (Date == null) return false;

            return _DateRepository.UpdateCita(Date);
        }

        public bool UpdateStatusCita(Date cita)
        {
            if (cita == null) return false;

            return _DateRepository.UpdateStatusCita(cita);
        }

        public List<Date>? GetDatesPorPaciente(int idPaciente)
        {
            if (idPaciente <= 0) return null;

            return _DateRepository.GetCitasPorPaciente(idPaciente);
        }

        public bool confirmarDatePorPaciente(int idDate, int idPaciente)
        {
            if (idDate <= 0 || idPaciente <= 0) return false;

            return _DateRepository.confirmarCitaPorPaciente(idDate, idPaciente);
        }

        public bool cancelarDatePorPaciente(int idDate, int idPaciente)
        {
            if (idDate <= 0 || idPaciente <= 0) return false;

            return _DateRepository.cancelarCitaPorPaciente(idDate, idPaciente);
        }

        public bool confirmarDatePorEspecialista(int idDate, int idEspecialista)
        {
            if (idDate <= 0 || idEspecialista <= 0) return false;

            return _DateRepository.confirmarCitaPorEspecialista(idDate, idEspecialista);
        }

        public bool cancelarDatePorEspecialista(int idDate, int idEspecialista)
        {
            if (idDate <= 0 || idEspecialista <= 0) return false;

            return _DateRepository.cancelarCitaPorEspecialista(idDate, idEspecialista);
        }

        public List<Date>? GetDatesPorEspecialista(int idSpecialist)
        {
            if(idSpecialist <= 0) return null;

            return _DateRepository.GetCitasPorEspecialista(idSpecialist);
        }

        public int AddDateSpecialist(Date Date, int idEspecialista, int idPaciente)
        {
            if (Date == null) return 0;

            int idDate = _DateRepository.AddCita(Date, idPaciente);

            if (idDate > 0)
            {
                bool vinculacion = _DateRepository.vincularCitaConPeciente(idDate, idPaciente);
                if (!vinculacion)
                {
                    _DateRepository.DeleteCita(idDate);
                    return 0;
                }
                bool vinc = _DateRepository.vincularCitaConEspecialista(idEspecialista, idDate);
                
                if(vinc)
                {
                    return idDate;
                }
                else
                {
                    _DateRepository.DeleteCita(idDate);
                    return 0;
                }
            }
            return 0;

        }
    }
}

