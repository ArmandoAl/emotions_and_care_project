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

        public CartService(ICartRepository cartRepository, IGoalRepository logroRepository, 
            IItemsRepository itemsRepository
        )
        {
            _cartRepository = cartRepository;
            _logroRepository = logroRepository;
            _itemsRepository = itemsRepository;
        }

        public GoalWithCart? Add(Cart cart, int idUsuario, bool isPatient, bool isFirtTime)
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

                if(isFirtTime && isPatient)
                {
                    var idLogro = _logroRepository.AddGoalPatient(idUsuario, 1);

                    _itemsRepository.addStickerToPatient(1, idUsuario);

     
                    
                    return new GoalWithCart
                    {
                        goal = _logroRepository.GetGoal(idLogro),
                        cartId = idCart
                    };
                } else {
                    
                    return new GoalWithCart
                    {
                        goal = null,
                        cartId = idCart
                    };
                }
            }
            return null;
        }

        public GoalWithCartAnswer? AddRespuesta(CartAnswer respuesta, int idCart, bool isFirtTime)
        {
            if (respuesta == null || idCart <= 0) return null;
            bool res = _cartRepository.AddRespuesta(respuesta, idCart);

            if (res)
            {

                if(isFirtTime) {
                var idLogro = _logroRepository.AddGoalPatient(respuesta.receiverId, 2);

                _itemsRepository.addStickerToPatient(2, respuesta.receiverId);

                if (idLogro > 0)
                {
                return new GoalWithCartAnswer
                {
                    goal = _logroRepository.GetGoal(idLogro),
                    cartAnswerId = respuesta.cartId
                };

                }

                } else {
                    return new GoalWithCartAnswer
                    {
                        goal = null,
                        cartAnswerId = respuesta.cartId
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
    }
}
