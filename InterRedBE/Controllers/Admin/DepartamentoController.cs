using InterRedBE.BAL.Bao;
using InterRedBE.DAL.Models;
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
                return Ok(_departamentoBAO.GetAll());
            }
            catch (SystemException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            
        }
        [HttpGet("{id}")]
        public IActionResult GetOne (int id)
        {
            return Ok(_departamentoBAO.GetOneInt(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateOne([FromBody] Departamento departamento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _departamentoBAO.CreateOne(departamento);

                
                if (result.Data != null)
                {
                    
                    return CreatedAtAction(nameof(GetOne), new { id = result.Data.Id }, result.Data);
                }
                else
                {
                    
                    return BadRequest(result.Message);
                }
            }
            catch (Exception ex)
            {
               
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
