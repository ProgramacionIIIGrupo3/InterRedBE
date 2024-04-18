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
    public class LugarTuristicoController : ControllerBase
    {
        // Inyección de dependencia del servicio
        public readonly ILugarTuristicoBAO _lugarTuristicoBAO;

        public LugarTuristicoController(ILugarTuristicoBAO lugarTuristicoBAO)
        {
            _lugarTuristicoBAO = lugarTuristicoBAO;
        }

        // Método para obtener todos los lugares turísticos
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

        // Método para crear un lugar turístico
        [HttpPost]
        public async Task<IActionResult> CreateOne([FromBody] LugarTuristicoDTO lugarTuristicoViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var lugarTuristico = new LugarTuristico
                {
                    // Asignación de propiedades desde DTO
                    Nombre = lugarTuristicoViewModel.Nombre,
                    Descripcion = lugarTuristicoViewModel.Descripcion,
                    Imagen = lugarTuristicoViewModel.Imagen,
                    IdMunicipio = lugarTuristicoViewModel.IdMunicipio ?? 0,
                    IdDepartamento = lugarTuristicoViewModel.IdDepartamento ?? 0
                };

                var result = await _lugarTuristicoBAO.CreateOne(lugarTuristico);
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

        // Método para actualizar un lugar turístico
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOne(int id, [FromBody] LugarTuristicoDTO lugarTuristicoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var lugarTuristico = new LugarTuristico
                {
                    // Actualización de entidad con datos del DTO
                    Id = id,
                    Nombre = lugarTuristicoDTO.Nombre,
                    Descripcion = lugarTuristicoDTO.Descripcion,
                    Imagen = lugarTuristicoDTO.Imagen,
                    IdMunicipio = lugarTuristicoDTO.IdMunicipio ?? 0,
                    IdDepartamento = lugarTuristicoDTO.IdDepartamento ?? 0
                };

                var result = await _lugarTuristicoBAO.UpdateOne(lugarTuristico);
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

        // Método para eliminar un lugar turístico
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOne(int id)
        {
            try
            {
                var result = await _lugarTuristicoBAO.DeleteOne(id);
                if (result.Code == 1)
                {
                    return Ok(result.Message);
                }
                else if (result.Code == 0)
                {
                    return NotFound(result.Message);
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

