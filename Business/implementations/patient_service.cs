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
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _service;

        public PatientService(IPatientRepository service)
        {
            _service = service;
        }

        public int Add(AddPatient paciente)
        {
            if(paciente == null) { return 0; }
            int id = _service.Add(paciente);

             if(id == 0)
            {
            return 0;        
            }
            return id;
        }

        public bool Delete(int id)
        {
           if(id < 1) { return false; }
           return _service.Delete(id);
        }

        public Patient? Get(int id)
        {
            if (id < 1) return null;
            
            return _service.Get(id);
        }

        public bool Update(Patient paciente)
        {
            if(paciente == null) { return false; };
            return _service.Update(paciente);
        }

        public bool VincularEspecialista(int id, string tokenEspecialista)
        {
            if (id < 1 || string.IsNullOrEmpty(tokenEspecialista)) { return false; }
            return _service.VincularEspecialista(id, tokenEspecialista);
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

        public bool MoficarConfiguracionNotificaciones(int id, bool notificacionesActivas, bool dirioActivado, bool progresoActivado)
        {
            if (id < 1) { return false; }
            return _service.MoficarConfiguracionNotificaciones(id, notificacionesActivas, dirioActivado, progresoActivado);
        }

        public string? registerSet(
            int id,
            string state)
        {
            if (id < 1) { return  null; }
            if (string.IsNullOrEmpty(state)) { return null; }
            return _service.registerSet(id, state);

        }

        public int putStickeriInInterface(int idPatient, int idUserSticker, int position) {
            
            if (idPatient < 1 || idUserSticker < 1 || position < 1) { return 0; }
            return _service.putStickeriInInterface(idPatient, idUserSticker, position);
            
         }
    }
}
