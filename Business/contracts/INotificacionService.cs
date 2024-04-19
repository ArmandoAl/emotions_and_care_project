using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Contracts
{
    public interface INotificacionService
    {
        int AddNotificacion(Notificacion notificacion, int idPaciente);

        bool UpdateNotificacion(Notificacion notificacion);

        bool DeleteNotificacion(int idNotificacion);

        Notificacion? GetNotificacion(int idNotificacion);

        List<Notificacion>? GetNotificacionesByPaciente(int idPaciente);

        bool vincularNotificacionConPaciente(int idNotificacion, int idPaciente);

        bool updateDateEmision(int idNotificacion);

        List<Notificacion> init(int idPaciente); 
    }
}
