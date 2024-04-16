using InterRedBE.BAL.Bao;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InterRedBE.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class DepartamentoController : ControllerBase
    {
        public readonly IDepartamentoBAO _departamentoBAO;

        public DepartamentoController(IDepartamentoBAO departamentoBAO)
        {
            _departamentoBAO = departamentoBAO;
        }

        [HttpGet] 
        public IActionResult GetAll ()
        {
            try
            {
                return Ok(_departamentoBAO.GetAll);
            }
            catch (SystemException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            
        }
    }
}
