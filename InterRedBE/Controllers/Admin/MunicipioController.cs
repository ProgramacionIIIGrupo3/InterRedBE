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
        public async Task<IActionResult> CreateOne([FromForm] MunicipioDTO municipioDto)
        {
            try
            {
                if (municipioDto.ImagenFile == null || municipioDto.ImagenFile.Length == 0)
                {
                    return BadRequest("An image file is required.");
                }

                var fileName = Path.GetFileName(municipioDto.ImagenFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await municipioDto.ImagenFile.CopyToAsync(stream);
                }

                municipioDto.Imagen = Path.Combine("/images", fileName);

                var municipio = new Municipio
                {
                    Nombre = municipioDto.Nombre,
                    Descripcion = municipioDto.Descripcion,
                    Poblacion = municipioDto.Poblacion,
                    IdDepartamento = municipioDto.IdDepartamento,
                    Imagen = municipioDto.Imagen
                };

                var result = await _municipioBAO.CreateOne(municipio);
                if (result.Data != null)
                {
                    return StatusCode(StatusCodes.Status201Created, result.Data);
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOne(int id)
        {
            try
            {
                var result = await _municipioBAO.DeleteOne(id);
                return Ok(result);
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
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOne(int id, [FromForm] MunicipioDTO municipioDTO)
        {
            try
            {
                var municipioExistente = await _municipioBAO.GetOneInt(id);
                if (municipioExistente == null || municipioExistente.Data == null)
                {
                    return NotFound($"Municipio with Id = {id} not found.");
                }

                if (municipioDTO.ImagenFile != null && municipioDTO.ImagenFile.Length > 0)
                {
                    var fileName = Path.GetFileName(municipioDTO.ImagenFile.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await municipioDTO.ImagenFile.CopyToAsync(stream);
                    }
                    municipioDTO.Imagen = Path.Combine("/images", fileName);
                }
                else
                {
                    municipioDTO.Imagen = municipioExistente.Data.Imagen;
                }

                var municipio = new Municipio
                {
                    Id = id,
                    Nombre = municipioDTO.Nombre,
                    Descripcion = municipioDTO.Descripcion,
                    Poblacion = municipioDTO.Poblacion,
                    IdDepartamento = municipioDTO.IdDepartamento,
                    Imagen = municipioDTO.Imagen
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

        [HttpGet("departamento/{idDepartamento}")]
        public async Task<IActionResult> GetByDepartamentoId(int idDepartamento)
        {
            try
            {
                var result = await _municipioBAO.GetByDepartamentoId(idDepartamento);
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
}