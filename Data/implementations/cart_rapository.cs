using Data.Contracts;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Implementations
{
    public class CartRepository : ICartRepository
    {
        public int Add(Cart carta)
        {
            if (carta == null) return 0;
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
            .UseSqlServer(Data.Helpers.Constants.ConnectionString)
            .Options;

            carta.dateCreated = DateTime.Now;
            carta.modifiedDate = DateTime.Now;

            using (var db = new DBContext(options: connectionOptions))
            {
                db.carts.Add(carta);
                db.SaveChanges();
                return carta.cartId;
            }
        }

        public bool AddRespuesta(CartAnswer respuesta, int idCarta)
        {
            
            if (respuesta == null) return false;
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
            .UseSqlServer(Data.Helpers.Constants.ConnectionString)
            .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var carta = db.carts.Find(idCarta);
                if (carta == null) return false;
                carta.cartAnswers.Add(respuesta);
                db.SaveChanges();
                return true;
            }
        }

        public bool Delete(int idCarta)
        {
            if(idCarta <= 0) return false;
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
            .UseSqlServer(Data.Helpers.Constants.ConnectionString)
            .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var carta = db.carts.Find(idCarta);
                if (carta == null) return false;
                db.carts.Remove(carta);
                db.SaveChanges();
                return true;
            }
        }

        public Cart? Get(int idCarta)
        {
            if (idCarta <= 0) return null;
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
            .UseSqlServer(Data.Helpers.Constants.ConnectionString)
            .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                return db.carts.Find(idCarta);
            }
        }

        public List<Cart>? GetAllByUser(int idUsuario, bool isPatient)
        {
            if (idUsuario <= 0) return null;
            List<Cart> cartas = new List<Cart>();
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
            .UseSqlServer(Data.Helpers.Constants.ConnectionString)
            .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                if (isPatient)
                {
                   //dame las cartas en las que el usuario sea el author o haya escruto una respuesta
                   cartas = db.carts.Where(c => c.transmitterId == idUsuario || c.cartAnswers.Where(r => r.receiverId == idUsuario).Count() > 0).
                        Include(c => c.cartAnswers).ToList();
                }
                else
                {
                    cartas = db.carts.Where(c => c.transmitterId == idUsuario || c.cartAnswers.Where(r => r.receiverId == idUsuario).Count() > 0).
                        Include(c => c.cartAnswers).ToList();
                }
                return cartas;
            }
        }

        public List<Cart> GetNotExpiredCarts(int idUsuario)
        {
            List<Cart> cartas = new List<Cart>();
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
            .UseSqlServer(Data.Helpers.Constants.ConnectionString)
            .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                //cartas que no seas del usuario y que no esten expiradas
                List<Cart> cartasTotales = db.carts.Where(c => c.transmitterId != idUsuario && c.state == CartState.sent).
                      Include(c => c.cartAnswers).ToList();

                var cartasSinMiRespuesta = new List<Cart>();
                cartasSinMiRespuesta = cartasTotales.Where(c => c.cartAnswers.Where(r => r.receiverId == idUsuario).Count() == 0).ToList();

                cartas = filtrarCartas(cartasSinMiRespuesta);
                return cartas;
            }
        }

        private List<Cart> filtrarCartas(List<Cart> cartasTotales)
        {
            if(cartasTotales.Count <= 20) return cartasTotales;

            //quiero que me des 20 cartas que no esten expiradas de manera aleatoria, pero deben ser maximo dos cartas de la misma persona
            List<Cart> cartas = new List<Cart>();
            Random random = new Random();
            List<int> ids = new List<int>();
            while (cartas.Count < 20)
            {
                int index = random.Next(cartasTotales.Count);
                if (!ids.Contains(index))
                {
                    ids.Add(index);
                    cartas.Add(cartasTotales[index]);
                }
            } 
            
            //verificar que no haya mas de dos cartas de la misma persona
            List<Cart> cartasFiltradas = new List<Cart>();
            foreach (var carta in cartas)
            {
                if (cartasFiltradas.Count == 0)
                {
                    cartasFiltradas.Add(carta);
                }
                else
                {
                    if (cartasFiltradas.Where(c => c.transmitterId == carta.transmitterId).Count() < 2)
                    {
                        cartasFiltradas.Add(carta);
                    } else
                    {
                        var cartaRemplazo = cartasTotales.Where(c => c.transmitterId != carta.transmitterId).
                            FirstOrDefault();
                        if (cartaRemplazo != null)
                        {
                            cartasFiltradas.Add(cartaRemplazo);
                        }
                    } 
                }
            }   

            return cartasFiltradas;
        }

        public bool initCarts()
        {
            //lee todas las cartas y a las cartas que tengan mas de 7 dias de creacion las cambia a estado expirada
            List<Cart> cartas = new List<Cart>();
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                cartas = db.carts.Where(c => c.state == CartState.sent).
                    ToList();

                Console.WriteLine("cartas: " + cartas.Count);

                foreach (var carta in cartas)
                {
                    if (hasMoreThanSevenDays(carta))
                    {
                        Console.WriteLine("carta: " + carta.cartId);
                        carta.state = CartState.expired;
                        db.carts.Update(carta);
                    }
                }
                db.SaveChanges();
                return true;
            }
        }

        private bool hasMoreThanSevenDays(Cart carta)
        {
            DateTime fechaCreacion = carta.dateCreated;
            DateTime fechaActual = DateTime.Now;
            TimeSpan diferencia = fechaActual - fechaCreacion;
            return diferencia.Days > 7;
        }

        public bool Update(Cart carta)
        {
            if (carta == null) return false;
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
            .UseSqlServer(Data.Helpers.Constants.ConnectionString)
            .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                db.carts.Update(carta);
                db.SaveChanges();
                return true;
            }
        }

        public bool vincularCartConUsuario(int idCarta, int idUsuario, bool isPatient)
        {
            
            if (idCarta <= 0 || idUsuario <= 0) return false;
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
            .UseSqlServer(Data.Helpers.Constants.ConnectionString)
            .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
               var carta = db.carts.Find(idCarta);

                if(isPatient)
                {
                    var usuario = db.patients.Find(idUsuario);
                    if(usuario == null) return false;
                    usuario.carts.Add(carta!);
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    var usuario = db.specialists.Find(idUsuario);
                    if(usuario == null) return false;
                    usuario.communityCarts.Add(carta!);
                    db.SaveChanges();
                    return true;

                }
            }
        }

        public bool thereCartsForOpen(int idPaciente)
        {
            //revisa si en las respuestas de las cartas del paciente hay alguna que no haya sido abierta
            if (idPaciente <= 0) return false;
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
           .UseSqlServer(Data.Helpers.Constants.ConnectionString)
           .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var paciente = db.patients.Find(idPaciente);
                if (paciente == null) return false;
                foreach (var carta in paciente.carts)
                {
                    foreach (var respuesta in carta.cartAnswers)
                    {
                        if (respuesta.read == false)
                        {
                            return true;
                        }
                    }
                }

            }

            return false;
        }
    }
}
