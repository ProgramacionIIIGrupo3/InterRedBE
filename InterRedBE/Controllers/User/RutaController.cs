using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using InterRedBE.BAL.Bao;
using InterRedBE.DAL.DTO;

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
        public async Task<IActionResult> GetRuta(int idInicio, int idFin, [FromQuery] int numeroDeRutas = 5)
        {
            try
            {
                var todasLasRutas = await _rutaBAOService.EncontrarTodasLasRutasAsync(idInicio, idFin, numeroDeRutas);

                if (todasLasRutas != null && todasLasRutas.Count > 0)
                {
                    var rutas = todasLasRutas.Select(ruta =>
                    {
                        var caminoDTO = ruta.Item1.Select(departamento => new DepartamentoRutaDTO
                        {
                            Id = departamento.Id,
                            Nombre = departamento.Nombre
                        }).ToList();

                        return new
                        {
                            Ruta = caminoDTO,
                            DistanciaTotal = ruta.Item2
                        };
                    }).ToList();

                    return Ok(new { Rutas = rutas });
                }
                else
                {
                    return NotFound("No se encontraron rutas entre los departamentos especificados.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud: " + ex.Message);
            }
        }
    }
}
   
