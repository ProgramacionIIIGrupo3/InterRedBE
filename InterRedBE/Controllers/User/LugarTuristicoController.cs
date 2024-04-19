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
    public class LugarTuristicoController : ControllerBase
    {
        public readonly ILugarTuristicoBAO _lugarTuristicoBAO;
        public LugarTuristicoController(ILugarTuristicoBAO lugarTuristicoBAO)
        {
            _lugarTuristicoBAO = lugarTuristicoBAO;
        }
        // Método para obtener todos los datos de  lugares turísticos
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_lugarTuristicoBAO.GetAll());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // Método para obtener un lugar turístico por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneInt(int id)
        {
            try
            {
                var result = await _lugarTuristicoBAO.GetOneInt(id);
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

        [HttpGet("top10-visitas")]
        public IActionResult GetTop10Visitas()
        {
            try
            {
                return Ok(_lugarTuristicoBAO.GetTop10Visitas());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("top10-rating")]
        public IActionResult GetTop10ByRating()
        {
            try
            {
                return Ok(_lugarTuristicoBAO.GetTop10ByRating());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
