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

            var recomendacion = _recomendacionRepository.Get(idRecomendation);
            if (recomendacion == null) return false;

            // do a switch case, from the recomendation type

            int stickerId;
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
                    stickerId = 0; 
                    break; // Default case
                
            }

            //does patient has the sticker?
           

           
                //genera una funcion que de de maera aleatoria un sticker, pero que tenga probabilidad, cada que un usuario complete una recomendacion, tiene un 20% de probabilidad de obtener un sticker

                //numero ramdom del 1 y el 6

                if(stickerId < 1){ 
                    return _recomendacionRepository.recomendationCompleted(idRecomendation, idUsuario);
                }

                Random random = new Random();   
                var probability = random.Next(1, 100);                       

                if (probability <= 20)
                {
                     var hasSticker = _itemsService.HasSticker(stickerId, idUsuario);
                     if (hasSticker == null)
                     {
                        _itemsService.addStickerToPatient(stickerId, idUsuario);

                          var notiId = _notificationRepository.AddNotification(new NotificationModel
                            {
                            notificationType = NotificationType.sticker,
                            Titulo = "¡Nuevo sticker!",
                            Descripcion = "¡Felicidades, has desbloqueado un nuevo sticker!",
                            url = _itemsService.GetSticker(stickerId).url,
                            stickerId = stickerId                    
                            });

                        _notificationRepository.vincularNotificationConPaciente(notiId, idUsuario);

                        return _recomendacionRepository.recomendationCompleted(idRecomendation, idUsuario);
                    }
                }

            //if the sticker is not null, then we have to send a notification
            // tiene que ser de tipo sticker o goal

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
