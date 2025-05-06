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
    public class RecomendacionService : IRecomendationService
    {
        private readonly IRecomendationRepository _recomendacionRepository;
        private readonly IItemsRepository _itemsService;

        private readonly INotificationRepository _notificationRepository;

        public RecomendacionService(IRecomendationRepository recomendacionRepository, IItemsRepository itemsService, INotificationRepository notificationRepository)
        {
            _recomendacionRepository = recomendacionRepository;
            _itemsService = itemsService;
            _notificationRepository = notificationRepository;
        }
        public int Add(Recomendation recomendacion)
        {
            if (recomendacion == null) return 0;
            return _recomendacionRepository.Add(recomendacion);
        }

        public bool recomendationCompleted(int idRecomendation, int idUsuario){
            if (idRecomendation <= 0 || idUsuario <= 0) return false;
            Console.WriteLine("- RECOMENDACION  FUNCTION -");
            Console.WriteLine("idRecomendation: " + idRecomendation);
            Console.WriteLine("idUsuario: " + idUsuario);


            // Verificar
            //get recomendacion
            var recomendacion = _recomendacionRepository.Get(idRecomendation);
            if (recomendacion == null) return false;

            Console.WriteLine("recomendacion: " + recomendacion.ToString());

            // do a switch case, from the recomendation type

            var stickerId = 0;
            switch (recomendacion.type)
            {
                case RecomendationType.Sleep:
                    stickerId = 1; // ID del sticker para Sleep
                    break;
                case RecomendationType.Food:
                    // Do something for Food
                    stickerId = 2;
                    break;
                case RecomendationType.RelaxationTechniques:
                    // Do something for Relaxation Techniques
                    stickerId = 3;
                    break;
                case RecomendationType.PhysicalActivity:
                    // Do something for Physical Activity
                    stickerId = 4;
                    break;
                case RecomendationType.SocialLife:
                    // Do something for Social Life
                    stickerId = 5;
                    break;
                default:
                    return false; // Invalid recommendation type
            }

            //does patient has the sticker?
            var hasSticker = _itemsService.HasSticker(stickerId, idUsuario);

            Console.WriteLine("stickerId: " + stickerId);

            Console.WriteLine("hasSticker: " + hasSticker.ToString());

            if (hasSticker == null)
            {
                _itemsService.addStickerToPatient(stickerId, idUsuario);
                return _recomendacionRepository.recomendationCompleted(idRecomendation, idUsuario);
            }

            //if the sticker is not null, then we have to send a notification

            // tiene que ser de tipo sticker o goal

            
            var notiId = _notificationRepository.AddNotification(new NotificationModel
                {
                       notificationType = NotificationType.goal,
                        Titulo = "¡Nuevo sticker!",
                        Descripcion = "¡Felicidades, has desbloqueado un nuevo sticker!",
                        url = _itemsService.GetSticker(stickerId).url,
                        stickerId = stickerId                    
                    });

            _notificationRepository.vincularNotificationConPaciente(notiId, idUsuario);

            return _recomendacionRepository.recomendationCompleted(idRecomendation, idUsuario);
        }

        // public bool AddRecomendacionCompletada(int idRecomendacion, int idUsuario)
        // {
        //     if (idRecomendacion <= 0 || idUsuario <= 0) return false;
        //     return _recomendacionRepository.AddRecomendacionCompletada(idRecomendacion, idUsuario);
        // }

        public bool Delete(int idRecomendacion)
        {
            if (idRecomendacion <= 0) return false;
            return _recomendacionRepository.Delete(idRecomendacion);
        }

        public Recomendation? Get(int idRecomendacion)
        {
            if (idRecomendacion <= 0) return null;
            return _recomendacionRepository.Get(idRecomendacion);
        }

        public List<Recomendation> GetRecomentaciones()
        {
            return _recomendacionRepository.GetRecomentaciones()!;
        }

        public bool Update(Recomendation recomendacion)
        {
            if (recomendacion == null) return false;
            return _recomendacionRepository.Update(recomendacion);
        }

        public List<Recomendation>? GetCompletedRecomendations(int idUsuario)
        {
            if (idUsuario <= 0) return null;
            return _recomendacionRepository.GetCompletedRecomendations(idUsuario);
        }
    }
}
