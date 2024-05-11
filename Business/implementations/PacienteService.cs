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
    public class PacienteService : IPacienteService
    {
        private readonly IPacienteRepository _service;

        public PacienteService(IPacienteRepository service)
        {
            _service = service;
        }

        public int Add(AgregarPaciente paciente)
        {
            if(paciente == null) { return 0; }
            int id = _service.Add(paciente);

             if(id > 0)
            {
            //     // int result = _service.AgregarFlorInicial(id);
            //     // if(result > 0) {
                    
                
               
            //         int idSticker1 = _service.agregarStickerDeUsuarioModel(
            //             null, id
            //         );

            //         int response = _service.agregarStickerDeUsuarioModel(idSticker1, id);

            //         if (response > 0)
            //         {
                        return id;
                //    }
                    
            }
    
            

            return 0;
        }

        public bool Delete(int id)
        {
           if(id < 1) { return false; }
           return _service.Delete(id);
        }

        public Paciente? Get(int id)
        {
            if (id < 1) return null;
            
            return _service.Get(id);
        }

        public bool Update(Paciente paciente)
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

        public bool registerSet(int id)
        {
            if (id < 1) { return false; }
            return _service.registerSet(id);

        }
    }
}
