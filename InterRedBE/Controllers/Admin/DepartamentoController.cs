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

        // Método para crear un nuevo departamento
        [HttpPost]
        public async Task<IActionResult> CreateOne([FromBody] DepartamentoDTO departamentoViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var departamento = new Departamento
                {
                    // Asignación de propiedades desde el DTO al modelo
                    Nombre = departamentoViewModel.Nombre,
                    Descripcion = departamentoViewModel.Descripcion,
                    Imagen = departamentoViewModel.Imagen,
                    Poblacion = departamentoViewModel.Poblacion,
                    IdCabecera = departamentoViewModel.IdCabecera
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
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // Método para actualizar un departamento existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOne(int id, [FromBody] DepartamentoDTO departamentoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var departamento = new Departamento
                {
                    Id = id,
                    Nombre = departamentoDTO.Nombre,
                    Descripcion = departamentoDTO.Descripcion,
                    Imagen = departamentoDTO.Imagen,
                    Poblacion = departamentoDTO.Poblacion,
                    IdCabecera = departamentoDTO.IdCabecera
                };

                var result = await _departamentoBAO.UpdateOne(departamento);
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
