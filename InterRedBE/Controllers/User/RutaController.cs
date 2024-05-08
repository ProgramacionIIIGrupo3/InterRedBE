using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using InterRedBE.BAL.Bao;
using InterRedBE.DAL.DTO;
using InterRedBE.UTILS.Services;
using InterRedBE.DAL.Models;
using InterRedBE.BAL.Services;
using InterRedBE.DAL.Services;


namespace InterRedBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RutaController : ControllerBase
    {
       private readonly IRutaBAO _rutaBAOService;
         private const int Id = 5;

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

            [HttpGet("Top10Cercanos")]
            public async Task<IActionResult> GetTop10CercanosALaCapital()
            {
                try
                {
                    // Obtener las rutas desde la capital a todos los departamentos
                    var todasLasRutas = await _rutaBAOService.EncontrarTodasLasRutasAsync(Id, Id);

                    // Ordenar las rutas por distancia y tomar los primeros 10
                    var rutasCercanas = todasLasRutas.OrderBy(r => r.Item2)
                                                      .Take(10)
                                                      .Select(r => r.Item1)
                                                      .ToList();

                    return Ok(rutasCercanas);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud: " + ex.Message);
                }
            }
        }
    }
