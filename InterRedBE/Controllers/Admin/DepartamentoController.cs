using InterRedBE.BAL.Bao;
using InterRedBE.DAL.DTO;
using InterRedBE.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InterRedBE.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class DepartamentoController : ControllerBase
    {
        // Inyección de dependencia para el acceso a operaciones de departamento
        public readonly IDepartamentoBAO _departamentoBAO;

        public DepartamentoController(IDepartamentoBAO departamentoBAO)
        {
            _departamentoBAO = departamentoBAO;
        }

        // Método para obtener todos los departamentos
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

        // Método para obtener un departamento específico por ID
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

        [HttpPost]
        public async Task<IActionResult> CreateOne([FromForm] DepartamentoDTO departamentoDTO)
        {
            departamentoDTO.Imagen = null;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (departamentoDTO.ImagenFile == null || departamentoDTO.ImagenFile.Length == 0)
            {
                return BadRequest("An image file is required.");
            }

            var fileName = Path.GetFileName(departamentoDTO.ImagenFile.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await departamentoDTO.ImagenFile.CopyToAsync(stream);
            }

            // Aquí es donde se genera la URL de la imagen que se guardará en la base de datos
            departamentoDTO.Imagen = Path.Combine("/images", fileName);

            var departamento = new Departamento
            {
                Nombre = departamentoDTO.Nombre,
                Descripcion = departamentoDTO.Descripcion,
                Imagen = departamentoDTO.Imagen,
                Poblacion = departamentoDTO.Poblacion,
                IdCabecera = departamentoDTO.IdCabecera
            };

            var result = await _departamentoBAO.CreateOne(departamento);
            if (result.Data != null)
            {
                return StatusCode(StatusCodes.Status201Created, result.Data);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }



        // Método para actualizar un departamento existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOne(int id, [FromForm] DepartamentoDTO departamentoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var departamentoExistente = await _departamentoBAO.GetOneInt(id);
            if (departamentoExistente == null || departamentoExistente.Data == null)
            {
                return NotFound($"Departamento with Id = {id} not found.");
            }

            if (departamentoDTO.ImagenFile != null && departamentoDTO.ImagenFile.Length > 0)
            {
                var fileName = Path.GetFileName(departamentoDTO.ImagenFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await departamentoDTO.ImagenFile.CopyToAsync(stream);
                }
                departamentoDTO.Imagen = Path.Combine("/images", fileName); // Se genera la ruta de la imagen
            }
            else
            {
                departamentoDTO.Imagen = departamentoExistente.Data.Imagen; // Mantener la imagen existente si no se sube una nueva
            }

            Departamento updatedDepartamento = departamentoExistente.Data;
            updatedDepartamento.Nombre = departamentoDTO.Nombre;
            updatedDepartamento.Descripcion = departamentoDTO.Descripcion;
            updatedDepartamento.Imagen = departamentoDTO.Imagen;
            updatedDepartamento.Poblacion = departamentoDTO.Poblacion;
            updatedDepartamento.IdCabecera = departamentoDTO.IdCabecera;

            var result = await _departamentoBAO.UpdateOne(updatedDepartamento);
            if (result.Data != null)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }


        // Método para eliminar un departamento por ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOne(int id)
        {
            try
            {
                var result = await _departamentoBAO.DeleteOne(id);
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
