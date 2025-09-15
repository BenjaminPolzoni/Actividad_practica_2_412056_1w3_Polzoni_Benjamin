using Actividad_practica_1_412056_1w3_Polzoni_Benjamin.Domain;
using Actividad_practica_1_412056_1w3_Polzoni_Benjamin.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_Proyecto_actividad_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly InvoiceServices _Services;
        
        public InvoiceController(InvoiceServices invoiceServices)
        {
            _Services = invoiceServices;
        }

        [HttpGet]
        [Route("GetAllInvoice")]

        public IActionResult GetAllInvoice()
        {
            try
            {
                var list = _Services.GetAll();

                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetById{id}")]
        public IActionResult GetInvoice(int id)
        {
            try
            {
                var Invoice = _Services.GetById(id);

                return Ok(Invoice);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

        }

        [HttpPut]
        [Route("PutInvoices")]

        public IActionResult GetInvoice([FromBody] Invoice invoice) 
        {
            try
            {
                var Cargar = _Services.Save(invoice);
                return Ok(Cargar);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }
    }
}
