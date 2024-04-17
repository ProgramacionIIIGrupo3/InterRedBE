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
    }
}