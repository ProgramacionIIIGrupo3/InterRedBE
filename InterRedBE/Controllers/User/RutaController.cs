using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using InterRedBE.BAL.Bao;
using InterRedBE.DAL.DTO; // Asegúrate de incluir tu DTO
using InterRedBE.UTILS.Services; // Si es necesario

namespace InterRedBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RutaController : ControllerBase
    {
        private readonly IRutaBAO _rutaBAOService;

        public RutaController(IRutaBAO rutaBAOService)
        {
            _rutaBAOService = rutaBAOService;
        }

        [HttpGet("ruta/{idInicio}/{idFin}")]
        public async Task<IActionResult> GetRuta(int idInicio, int idFin)
        {
            try
            {
                var (camino, distanciaTotal) = await _rutaBAOService.EncontrarRutaAsync(idInicio, idFin);

                if (camino != null && !camino.ListaVacia())
                {
                    var ruta = camino.Select(departamento => new DepartamentoRutaDTO
                    {
                        Id = departamento.Id,
                        Nombre = departamento.Nombre
                    }).ToList();

                    var resultado = new
                    {
                        Ruta = ruta,
                        DistanciaTotal = distanciaTotal
                    };

                    return Ok(resultado);
                }
                else
                {
                    return NotFound("No se encontró una ruta entre los departamentos especificados.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
