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
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IGoalRepository _logroRepository;

        private readonly IItemsRepository _itemsRepository;

        private readonly IPatientRepository _patientRepository;

        public CartService(ICartRepository cartRepository, IGoalRepository logroRepository, 
            IItemsRepository itemsRepository, IPatientRepository patientRepository
        )
        {
            _cartRepository = cartRepository;
            _logroRepository = logroRepository;
            _itemsRepository = itemsRepository;
            _patientRepository = patientRepository;
        }

        public AchievementWithCart? Add(Cart cart, int idUsuario, bool isPatient)
        {
            if(cart == null || idUsuario <= 0) return null;

            int idCart = _cartRepository.Add(cart);

            if (idCart > 0)
            {
               bool vinculacion = _cartRepository.
                    vincularCartConUsuario(idCart, idUsuario, isPatient);

                if (vinculacion == false)
                {
                     _cartRepository.Delete(idCart);
                     return null;
                }

                if(isPatient)
                {
                    var idLogro = _patientRepository.checkAchievement_Community(idUsuario);
                    if (idLogro > 0)
                    {
                        return new AchievementWithCart
                        {
                            achievementId = idLogro,
                            cartId = idCart
                        };
                    } else
                    {
                        return new AchievementWithCart
                        {
                            achievementId = null,
                            cartId = idCart
                        };
                    }
                } else {
                    
                    return new AchievementWithCart
                    {
                        achievementId = null,
                        cartId = idCart
                    };
                }
            }
            return null;
        }

        public AchievementWithCartAnswer? AddRespuesta(CartAnswer respuesta, int idCart)
        {
            if (respuesta == null || idCart <= 0) return null;
            bool res = _cartRepository.AddRespuesta(respuesta, idCart);

            if (res)
            {
                var idLogro = _patientRepository.checkAchievement_Community(respuesta.receiverId);
                if (idLogro > 0)
                {
                    return new AchievementWithCartAnswer
                    {
                        achievementId = idLogro,
                        cartAnswerId = respuesta.cartAnswerId
                    };
                } else
                {
                    return new AchievementWithCartAnswer
                    {
                        achievementId = null,
                        cartAnswerId = respuesta.cartAnswerId
                    };
                }
            } 

            return null;
        }

        public bool Delete(int idCart)
        {
            if (idCart <= 0) return false;
            return _cartRepository.Delete(idCart);
        }

        public Cart? Get(int idCart)
        {
            if (idCart <= 0) return null;
            return _cartRepository.Get(idCart);
        }

        public List<Cart>? GetAllByUser(int idUsuario, bool isPatient)
        {
            if (idUsuario <= 0) return null;
            return _cartRepository.GetAllByUser(idUsuario, isPatient);
        }
        public bool Update(Cart cart)
        {
            if (cart == null) return false;
            return _cartRepository.Update(cart);
        }

        public List<Cart>? initCarts(int idUsuario)
        {
            bool init = _cartRepository.initCarts();
            
            if (init == false) return null;

            return _cartRepository.GetNotExpiredCarts(idUsuario);
        }

        public GoalWithSticker? getGoalWithSticker(int idUsuario, int idCart)
        {
            if (idUsuario <= 0 || idCart <= 0) return null;
            return _cartRepository.getGoalWithSticker(idUsuario, idCart);

        }
    }
}
