using InterRedBE.BAL.Bao;
using InterRedBE.DAL.Dao;
using InterRedBE.DAL.DTO;
using InterRedBE.DAL.Models;
using InterRedBE.DAL.Services;
using InterRedBE.UTILS;
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
        [HttpGet("{departamentoId}/poblacion")]
        public async Task<IActionResult> ObtenerPoblacionDepartamento(int departamentoId)
        {
            try
            {
                OperationResponse<long> response = await _departamentoBAO.ObtenerPoblacionDepartamento(departamentoId);
OperationResult<long> result = response.ToOperationResult();
                if (result.IsSuccess)
                {
                    return Ok(result.Result);
                }
                else
                {
                    return BadRequest(result.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

