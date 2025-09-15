using Actividad_practica_1_412056_1w3_Polzoni_Benjamin.Services;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlTypes;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api_Proyecto_actividad_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ClientServices _services;

        public ClientController(ClientServices services)
        {
            _services = services;
        }
        // GET: api/<ClientController>
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                var list = _services.GetAll();
                
                return Ok(list);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,ex.Message);
            }
        }

        // GET api/<ClientController>/5
        [HttpGet]
        [Route("GetById{id}")]
        public IActionResult Get([FromRoute]int id)
        {

            try
            {
                var client = _services.GetById(id);

                return Ok(client);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

    }
}
