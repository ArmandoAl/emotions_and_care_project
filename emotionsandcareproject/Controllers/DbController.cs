using Business.Contracts;
using Business.Implementations;
using Data.Contracts;
using Domain;
using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class DbController : Controller
    {

        private readonly IDBRepository _service;

        public DbController(IDBRepository service){
            _service = service;
        }

        [HttpDelete("Delete")]
        public ActionResult Delete()
        {
            bool result = _service.deleteDatabase();
            if (result == false) return BadRequest();
            return Ok(result);
        
        }
    }

}


    