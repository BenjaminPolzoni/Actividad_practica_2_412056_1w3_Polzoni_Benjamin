using Actividad_practica_1_412056_1w3_Polzoni_Benjamin.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api_Proyecto_actividad_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        private static SellerServices _SellerServices;

        public SellerController (SellerServices seller)
        {
            _SellerServices = seller;
        }
        // GET: api/<SellerController>
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                var lista = _SellerServices.GetAll();

                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET api/<SellerController>/5
        [HttpGet]
        [Route("GetById{id}")]
        public IActionResult GetById([FromRoute]int id)
        {
            try
            {
                var seller = _SellerServices.GetById(id);
                
                return Ok(seller);

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
