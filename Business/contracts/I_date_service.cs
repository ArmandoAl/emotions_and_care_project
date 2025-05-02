using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Contracts
{
    public interface IDateService
    {
        AchievementWithDate? AddDate(Date Date, int idPaciente, int idEspacialista);

        bool UpdateDate(Date Date);

        bool DeleteDate(int idDate);

        Date? GetDate(int idDate);

        List<Date>? GetDatesPorPaciente(int idPaciente);

        bool confirmarDatePorPaciente(int idDate, int idPaciente);

        bool cancelarDatePorPaciente(int idDate, int idPaciente);

        bool confirmarDatePorEspecialista(int idDate, int idEspecialista);

        bool cancelarDatePorEspecialista(int idDate, int idEspecialista);
        List<Date>? GetDatesPorEspecialista(int idSpecialist);
        int AddDateSpecialist(Date Date, int idEspecialista, int idPaciente);

          bool UpdateStatusCita(Date cita);
    }
}
