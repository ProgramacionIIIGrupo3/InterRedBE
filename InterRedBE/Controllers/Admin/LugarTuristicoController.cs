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

        // Método para crear un nuevo lugar turístico con imagen
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] LugarTuristicoDTO lugarTuristicoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (lugarTuristicoDTO.ImagenFile == null || lugarTuristicoDTO.ImagenFile.Length == 0)
            {
                return BadRequest("An image file is required.");
            }

            var fileName = Path.GetFileName(lugarTuristicoDTO.ImagenFile.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await lugarTuristicoDTO.ImagenFile.CopyToAsync(stream);
            }

            lugarTuristicoDTO.Imagen = Path.Combine("/images", fileName);

            var lugarTuristico = new LugarTuristico
            {
                Nombre = lugarTuristicoDTO.Nombre,
                Descripcion = lugarTuristicoDTO.Descripcion,
                Imagen = lugarTuristicoDTO.Imagen,
                IdMunicipio = lugarTuristicoDTO.IdMunicipio,
                IdDepartamento = lugarTuristicoDTO.IdDepartamento
            };

            // Suponiendo que tienes una lógica de negocio similar (BAO) para LugarTuristico
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

        // Método para actualizar un lugar turístico existente con la opción de actualizar la imagen
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] LugarTuristicoDTO lugarTuristicoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var lugarTuristicoExistente = await _lugarTuristicoBAO.GetOneInt(id);
            if (lugarTuristicoExistente == null || lugarTuristicoExistente.Data == null)
            {
                return NotFound($"LugarTuristico with Id = {id} not found.");
            }

            if (lugarTuristicoDTO.ImagenFile != null && lugarTuristicoDTO.ImagenFile.Length > 0)
            {
                var fileName = Path.GetFileName(lugarTuristicoDTO.ImagenFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await lugarTuristicoDTO.ImagenFile.CopyToAsync(stream);
                }
                lugarTuristicoDTO.Imagen = Path.Combine("/images", fileName);
            }
            else
            {
                lugarTuristicoDTO.Imagen = lugarTuristicoExistente.Data.Imagen;
            }

            LugarTuristico updatedLugarTuristico = lugarTuristicoExistente.Data;
            updatedLugarTuristico.Nombre = lugarTuristicoDTO.Nombre;
            updatedLugarTuristico.Descripcion = lugarTuristicoDTO.Descripcion;
            updatedLugarTuristico.Imagen = lugarTuristicoDTO.Imagen;
            updatedLugarTuristico.IdMunicipio = lugarTuristicoDTO.IdMunicipio;
            updatedLugarTuristico.IdDepartamento = lugarTuristicoDTO.IdDepartamento;

            var result = await _lugarTuristicoBAO.UpdateOne(updatedLugarTuristico);
            if (result.Data != null)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Message);
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

