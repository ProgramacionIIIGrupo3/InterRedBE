using InterRedBE.BAL.Bao;
using InterRedBE.DAL.Dao;
using InterRedBE.DAL.DTO;
using InterRedBE.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InterRedBE.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class MunicipioController : ControllerBase
    {
        public readonly IMunicipioBAO _municipioBAO;

        public MunicipioController(IMunicipioBAO municipioBAO)
        {
            _municipioBAO = municipioBAO;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_municipioBAO.GetAll());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateOne([FromBody] MunicipioDTO municipioDto)
        {
            try
            {
                var municipio = new Municipio
                {
                    Nombre = municipioDto.Nombre,
                    Descripcion = municipioDto.Descripcion,
                    Poblacion = municipioDto.Poblacion,
                    IdDepartamento = municipioDto.IdDepartamento

                };
                
                var result = await _municipioBAO.CreateOne(municipio);
                if(result.Data!=null)
                {
                    return StatusCode (StatusCodes.Status201Created, result.Data);
                }
                else {
                    return BadRequest(result.Message);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOne(int id)
        {
            try
            {
                return Ok(_municipioBAO.DeleteOne(id));
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
                var result = await _municipioBAO.GetOneInt(id);

                if (result.Data != null)
                {
                    return Ok(result.Data);
                }
                else
                {
                    return NotFound(result.Message);
                }
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOne(int id, [FromBody]MunicipioDTO municipioDTO)
        {
            try
            {
                var municipio = new Municipio
                {
                    Id = id,
                    Nombre = municipioDTO.Nombre,
                    Descripcion = municipioDTO.Descripcion,
                    Poblacion = municipioDTO.Poblacion,
                    IdDepartamento = municipioDTO.IdDepartamento,
                };
                var result = await _municipioBAO.UpdateOne(municipio);
                
                if (result.Data != null)
                {
                    return Ok(result.Data);    
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
