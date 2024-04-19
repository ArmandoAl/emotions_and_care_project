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
    public class CartaRapository : ICartaRepository
    {
        public int Add(Carta carta)
        {
            if (carta == null) return 0;
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
            .UseSqlServer(Data.Helpers.Constants.ConnectionString)
            .Options;

            carta.FechaCreacion = DateTime.Now;
            carta.FechaModificacion = DateTime.Now;

            using (var db = new DBContext(options: connectionOptions))
            {
                db.Cartas.Add(carta);
                db.SaveChanges();
                return carta.IdCarta;
            }
        }

        public bool AddRespuesta(RespuestaCarta respuesta, int idCarta)
        {
            
            if (respuesta == null) return false;
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
            .UseSqlServer(Data.Helpers.Constants.ConnectionString)
            .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var carta = db.Cartas.Find(idCarta);
                if (carta == null) return false;
                carta.Respuestas.Add(respuesta);
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
                var carta = db.Cartas.Find(idCarta);
                if (carta == null) return false;
                db.Cartas.Remove(carta);
                db.SaveChanges();
                return true;
            }
        }

        public Carta? Get(int idCarta)
        {
            if (idCarta <= 0) return null;
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
            .UseSqlServer(Data.Helpers.Constants.ConnectionString)
            .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                return db.Cartas.Find(idCarta);
            }
        }

        public List<Carta>? GetAllByUser(int idUsuario, bool isPatient)
        {
            if (idUsuario <= 0) return null;
            List<Carta> cartas = new List<Carta>();
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
            .UseSqlServer(Data.Helpers.Constants.ConnectionString)
            .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                if (isPatient)
                {
                   //dame las cartas en las que el usuario sea el author o haya escruto una respuesta
                   cartas = db.Cartas.Where(c => c.IdEmisor == idUsuario || c.Respuestas.Where(r => r.IdReceptor == idUsuario).Count() > 0).
                        Include(c => c.Respuestas).ToList();
                }
                else
                {
                    cartas = db.Cartas.Where(c => c.IdEmisor == idUsuario || c.Respuestas.Where(r => r.IdReceptor == idUsuario).Count() > 0).
                        Include(c => c.Respuestas).ToList();
                }
                return cartas;
            }
        }

        public List<Carta> GetNotExpiredCarts(int idUsuario)
        {
            List<Carta> cartas = new List<Carta>();
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
            .UseSqlServer(Data.Helpers.Constants.ConnectionString)
            .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                //cartas que no seas del usuario y que no esten expiradas
                List<Carta> cartasTotales = db.Cartas.Where(c => c.IdEmisor != idUsuario && c.Estado == EstadoCarta.Enviada).
                      Include(c => c.Respuestas).ToList();

                var cartasSinMiRespuesta = new List<Carta>();
                cartasSinMiRespuesta = cartasTotales.Where(c => c.Respuestas.Where(r => r.IdReceptor == idUsuario).Count() == 0).ToList();

                cartas = filtrarCartas(cartasSinMiRespuesta);
                return cartas;
            }
        }

        private List<Carta> filtrarCartas(List<Carta> cartasTotales)
        {
            if(cartasTotales.Count <= 20) return cartasTotales;

            //quiero que me des 20 cartas que no esten expiradas de manera aleatoria, pero deben ser maximo dos cartas de la misma persona
            List<Carta> cartas = new List<Carta>();
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
            List<Carta> cartasFiltradas = new List<Carta>();
            foreach (var carta in cartas)
            {
                if (cartasFiltradas.Count == 0)
                {
                    cartasFiltradas.Add(carta);
                }
                else
                {
                    if (cartasFiltradas.Where(c => c.IdEmisor == carta.IdEmisor).Count() < 2)
                    {
                        cartasFiltradas.Add(carta);
                    } else
                    {
                        var cartaRemplazo = cartasTotales.Where(c => c.IdEmisor != carta.IdEmisor).
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
            List<Carta> cartas = new List<Carta>();
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
                .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                cartas = db.Cartas.Where(c => c.Estado == EstadoCarta.Enviada).
                    ToList();
                foreach (var carta in cartas)
                {
                    if (hasMoreThanSevenDays(carta))
                    {
                        carta.Estado = EstadoCarta.Expirada;
                        db.Cartas.Update(carta);
                    }
                }
                db.SaveChanges();
                return true;
            }
        }

        private bool hasMoreThanSevenDays(Carta carta)
        {
            DateTime fechaCreacion = carta.FechaCreacion;
            DateTime fechaActual = DateTime.Now;
            TimeSpan diferencia = fechaActual - fechaCreacion;
            return diferencia.Days > 7;
        }

        public bool Update(Carta carta)
        {
            if (carta == null) return false;
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
            .UseSqlServer(Data.Helpers.Constants.ConnectionString)
            .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                db.Cartas.Update(carta);
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
               var carta = db.Cartas.Find(idCarta);

                if(isPatient)
                {
                    var usuario = db.Pacientes.Find(idUsuario);
                    if(usuario == null) return false;
                    usuario.Cartas.Add(carta!);
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    var usuario = db.Especialistas.Find(idUsuario);
                    if(usuario == null) return false;
                    usuario.CartasDeComunidad.Add(carta!);
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
                var paciente = db.Pacientes.Find(idPaciente);
                if (paciente == null) return false;
                foreach (var carta in paciente.Cartas)
                {
                    foreach (var respuesta in carta.Respuestas)
                    {
                        if (respuesta.Leida == false)
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
