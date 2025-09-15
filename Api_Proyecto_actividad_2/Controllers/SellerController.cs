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
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
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

        // POST api/<SellerController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SellerController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SellerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
