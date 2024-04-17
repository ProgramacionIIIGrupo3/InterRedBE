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

        [HttpPost]
        public async Task<IActionResult> CreateOne([FromBody] DepartamentoDTO departamentoViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Mapear el view model a la entidad Departamento
                var departamento = new Departamento
                {
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
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOne(int id, [FromBody] DepartamentoDTO departamentoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Mapear el DTO a la entidad Departamento
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOne(int id)
        {
            try
            {
                var result = await _departamentoBAO.DeleteOne(id);

                if (result.Code == 1)
                {
                    return Ok(result.Message); // Devolver un código 200 OK si se elimina correctamente
                }
                else if (result.Code == 0)
                {
                    return NotFound(result.Message); // Devolver un código 404 Not Found si el departamento no existe
                }
                else
                {
                    return BadRequest(result.Message); // Devolver un código 400 Bad Request para cualquier otro caso
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message); // Devolver un código 500 Internal Server Error si ocurre un error inesperado
            }
        }

    }
}