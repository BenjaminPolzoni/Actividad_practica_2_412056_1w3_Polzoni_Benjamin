using Actividad_practica_1_412056_1w3_Polzoni_Benjamin.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_Proyecto_actividad_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly ArticleServices _Services;

        public ArticlesController(ArticleServices invoiceServices)
        {
            _Services = invoiceServices;
        }

        [HttpGet]
        [Route("getAll")]
        public IActionResult GetAll()
        {
            try
            {
                var list = _Services.GetAll();

                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex);
            }
        }

        [HttpGet]
        [Route("GetById{id}")]
        public IActionResult GetById([FromRoute]int id)
        {
            try
            {
                var Article = _Services.GetById(id);
                return Ok(Article);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
