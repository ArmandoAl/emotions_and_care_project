using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Contracts
{
    public interface INotificacionRepository
    {
        int AddNotificacion(Notificacion notificacion);

        bool UpdateNotificacion(Notificacion notificacion);

        bool DeleteNotificacion(int idNotificacion);

        Notificacion? GetNotificacion(int idNotificacion);

        List<Notificacion>? GetNotificacionesByPaciente(int idPaciente);

        bool vincularNotificacionConPaciente(int idNotificacion, int idPaciente);

        bool updateDateEmision(int idNotificacion);
    }
}
