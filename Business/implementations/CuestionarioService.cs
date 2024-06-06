using Business.contracts;
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
    public class CuestionarioService : ICuestionarioService
    {
        private readonly ICuestionarioRepository _cuestionarioService;
        private readonly INotificacionService _notificacionService;

        private readonly ILogroService _logroService;

        public CuestionarioService(ICuestionarioRepository cuestionarioService, INotificacionService notificacionService, ILogroService logroService)
        {
            _cuestionarioService = cuestionarioService;
            _notificacionService = notificacionService;
            _logroService = logroService;
        }
        public int AddCuestionario(Cuestionario cuestionario)
        {
            if (cuestionario == null) return 0;

            var idCuestionario = _cuestionarioService.AddCuestionario(cuestionario);

            if (idCuestionario > 0)
            {
               var result = _cuestionarioService.AgregarCuestionarioAPacientes(idCuestionario);
               if (result) return idCuestionario;
            }

            return 0;
        }

        public LogroWithTestInfoModel? completarCuestionario(int idCuestionario, int idPaciente, List<TestQuestionForComplete> respuestas, bool isFirstTime)
        {
           if (idCuestionario <= 0 || idPaciente <= 0) return null;

            var result = _cuestionarioService.completarCuestionario(idCuestionario, idPaciente);
            if (result)
            {
                var idHistoralCuestionario = _cuestionarioService.AgregarHistorialCuestionario(idCuestionario, respuestas, idPaciente);

                if (idHistoralCuestionario >= 0)
                {
                    bool relate = _cuestionarioService.relacionarHistorialCuestionarioConPaciente(idHistoralCuestionario, idPaciente);

                    if (!relate)
                    {
                        return null;
                    }

                    if(isFirstTime) {
                        var idLogro = _logroService.AgregarLogroAPaciente(idPaciente, 6);

                        if (idLogro <= 0) return null;

                        return new LogroWithTestInfoModel
                        {
                        TestInfoModel = _cuestionarioService.GetTestInfoModel(idHistoralCuestionario)!,

                        Logro = _logroService.GetLogro(idLogro)
                        };
                    } else {

                        return new LogroWithTestInfoModel
                        {
                            TestInfoModel = _cuestionarioService.GetTestInfoModel(idHistoralCuestionario)!,

                            Logro = null
                        };

                    }   
                }
            }

            return null;
        }

        public bool DeleteCuestionario(int idCuestionario)
        {
            if (idCuestionario <= 0) return false;

            return _cuestionarioService.DeleteCuestionario(idCuestionario);
        }

        public Cuestionario? GetCuestionario(int idCuestionario)
        {
            if (idCuestionario <= 0) return null;

            return _cuestionarioService.GetCuestionario(idCuestionario);
        }

        public CuestionariosInfo? GetCuestionariosInfo(int pacienteId)
        {
            if(pacienteId <= 0) return null;
               
            CuestionariosInfo cuestionariosInfo = _cuestionarioService.GetCuestionariosInfo(pacienteId)!;

            if (cuestionariosInfo != null)
            {
                bool canMakeTest = _cuestionarioService.CanMakeTest(pacienteId);

                if (canMakeTest)
                {
                    _notificacionService.AddNotificacion(new Notificacion
                    {
                        Titulo = "Cuestionarios",
                        Descripcion = "Tienes un cuestionario disponible para completar",
                        TipoNotificacion = TipoNotificacion.NotificacionRecordatorio,
                    }, pacienteId);
                }

                return cuestionariosInfo;
            }

            return null;
        }

        public bool UpdateCuestionario(Cuestionario cuestionario)
        {
            if (cuestionario == null) return false;

            return _cuestionarioService.UpdateCuestionario(cuestionario);
        }

        public bool changeVisibility(int idCuestionario, int idTestInfoModel, bool visible)
        {
            if (idCuestionario <= 0 || idTestInfoModel <= 0) return false;

            return _cuestionarioService.changeVisibility(idCuestionario, idTestInfoModel, visible);

        }
    }
}
