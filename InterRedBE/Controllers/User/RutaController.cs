using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using InterRedBE.BAL.Bao;
using InterRedBE.DAL.DTO;
using InterRedBE.UTILS.Services;
using InterRedBE.DAL.Models;
using InterRedBE.BAL.Services;
using InterRedBE.DAL.Services;
using InterRedBE.DAL.Context;

namespace InterRedBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RutaController : ControllerBase
    {
       private readonly IRutaBAO _rutaBAOService;
        private readonly int _Id; // Asumimos que el ID de la ciudad capital es 5 (Guatemala)

        public RutaController(IRutaBAO rutaBAOService, InterRedContext context)
        {
            _rutaBAOService = rutaBAOService;
            _Id = 5; // ID de Guatemala

            // Inicializar los servicios necesarios
            var rutaService = new RutaService(context);
            _rutaBAOService = new RutaBAOService(rutaService, _Id);
        }

        [HttpGet("ruta/{idInicio}/{idFin}")]
        public async Task<IActionResult> GetRuta(int idInicio, int idFin, [FromQuery] int numeroDeRutas = 5)
        {
            try
            {
                var todasLasRutas = await _rutaBAOService.EncontrarTodasLasRutasAsync(idInicio, idFin, numeroDeRutas);
                if (!todasLasRutas.ListaVacia())
                {
                    var rutas = new ListaEnlazadaDoble<object>();
                    foreach (var ruta in todasLasRutas)
                    {
                        var caminoDTO = new ListaEnlazadaDoble<DepartamentoRutaDTO>();
                        foreach (var departamento in ruta.Item1)
                        {
                            caminoDTO.InsertarAlFinal(new DepartamentoRutaDTO
                            {
                                Id = departamento.Id,
                                Nombre = departamento.Nombre
                            });
                        }
                        rutas.InsertarAlFinal(new
                        {
                            Ruta = caminoDTO,
                            DistanciaTotal = ruta.Item2
                        });
                    }
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
        [HttpGet("top10-cercanos")]
        public async Task<IActionResult> GetTop10Cercanos()
        {
            var todasLasRutas = await _rutaBAOService.EncontrarTodasLasRutasAsync(_Id, _Id, 10);
            var top10Cercanos = todasLasRutas.Select(r => r.Item1.First().Nombre).ToList();

            return Ok(top10Cercanos);
        }

        [HttpGet("top10-lejanos")]
        public async Task<IActionResult> GetTop10Lejanos()
        {
            var todasLasRutas = await _rutaBAOService.EncontrarTodasLasRutasAsync(_Id, _Id, 10);
            var top10Lejanos = todasLasRutas.Select(r => r.Item1.Last().Nombre).ToList();

            return Ok(top10Lejanos);
        }



    }

}