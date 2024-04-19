using InterRedBE.BAL.Bao;
using InterRedBE.DAL.Dao;
using InterRedBE.DAL.DTO;
using InterRedBE.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InterRedBE.Controllers.User
{
    [Route("api/user/[controller]")]
    [ApiController]
    public class DepartamentoController : ControllerBase
    {
        
        public readonly IDepartamentoBAO _departamentoBAO;

        public DepartamentoController(IDepartamentoBAO departamentoBAO)
        {
            _departamentoBAO = departamentoBAO;
        }
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        try
        {
            return Ok(_departamentoBAO.GetAll());
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOneInt(int id)
    {
        try
        {
            var result = await _departamentoBAO.GetOneInt(id);
            if (result.Data != null)
            {
                return Ok(result.Data);
            }
            else
            {
                return NotFound(result.Message);
            }
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

}
