using System.Threading.Tasks;
using InterRedBE.BAL.Bao;
using InterRedBE.DAL.Context;
using InterRedBE.DAL.DTO;
using InterRedBE.DAL.Models;
using InterRedBE.DAL.Services;
using InterRedBE.UTILS.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InterRedBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RutaController : ControllerBase
    {
        private readonly IRutaBAO _rutaBAOService;
        private readonly InterRedContext _context;
        private readonly InterRedContext _context1;

        public RutaController(IRutaBAO rutaBAOService, InterRedContext context, InterRedContext context1)
        {
            _rutaBAOService = rutaBAOService;
            _context = context;
            _context1 = context1;
        }


        [HttpGet("ruta/nueva/{idXInicio}/{idXFin}")]
        public async Task<IActionResult> GetRutaNueva(string idXInicio, string idXFin, [FromQuery] int numeroDeRutas = 5)
        {
            try
            {
                var todasLasRutas = await _rutaBAOService.EncontrarTodasLasRutasNuevoAsync(idXInicio, idXFin, numeroDeRutas);
                if (!todasLasRutas.ListaVacia())
                {
                    var rutas = new ListaEnlazadaDoble<object>();
                    var rutasUnicas = new HashSet<string>(); // Para asegurar rutas únicas

                    foreach (var ruta in todasLasRutas)
                    {
                        var caminoDTO = new ListaEnlazadaDoble<EntidadRutaDTO>();
                        foreach (var entidad in ruta.Item1)
                        {
                            caminoDTO.InsertarAlFinal(new EntidadRutaDTO
                            {
                                Id = entidad.Id,
                                Nombre = entidad.Nombre
                            });
                        }
                        var rutaStr = string.Join("->", ruta.Item1.Select(e => e.Id)); // Ruta como string única
                        if (!rutasUnicas.Contains(rutaStr))
                        {
                            rutas.InsertarAlFinal(new
                            {
                                Ruta = caminoDTO,
                                DistanciaTotal = ruta.Item2
                            });
                            rutasUnicas.Add(rutaStr);
                        }
                    }
                    return Ok(new { Rutas = rutas });
                }
                else
                {
                    return NotFound("No se encontraron rutas entre las entidades especificadas.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud: " + ex.Message);
            }
        }
        

        [HttpGet("Top10CercanosAGuatemala")]
        public async Task<IActionResult> GetTop10CercanosAGuatemala()
        {
            try
            {
                var rutaService = new RutaService(_context);
                var (grafoEntidades, distancias) = await rutaService.CargarRutasNuevoAsync();

                var nodoGuatemala = grafoEntidades.ObtenerNodos().Values.FirstOrDefault(n => n.Dato.IdX == "D1");
                if (nodoGuatemala == null)
                {
                    return NotFound("No se encontró el nodo de Guatemala (D1)");
                }

                var todasLasRutas = grafoEntidades.BuscarTodasLasRutas(nodoGuatemala.Dato.IdX, null, distancias);

                // Ordenar las rutas por distancia ascendente
                var rutasOrdenadas = todasLasRutas.OrderBy(r => r.Item2).ToList();

                var top10Cercanos = rutasOrdenadas
                    .Take(10)
                    .Select(r => new
                    {
                        Nombre = string.Join(", ", r.Item1.Select(n => n.Nombre)),
                        Distancia = r.Item2
                    })
                    .ToList();

                return Ok(top10Cercanos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud: " + ex.Message);
            }
        }

        [HttpGet("Top10LejanosAGuatemala")]
        public async Task<IActionResult> GetTop10LejanosAGuatemala()
        {
            try
            {
                var rutaService = new RutaService(_context);
                var (grafoEntidades, distancias) = await rutaService.CargarRutasNuevoAsync();

                var nodoGuatemala = grafoEntidades.ObtenerNodos().Values.FirstOrDefault(n => n.Dato.IdX == "D1");
                if (nodoGuatemala == null)
                {
                    return NotFound("No se encontró el nodo de Guatemala (D1)");
                }

                var todasLasRutas = grafoEntidades.BuscarTodasLasRutas(nodoGuatemala.Dato.IdX, null, distancias);

                // Ordenar las rutas por distancia descendente
                var rutasOrdenadas = todasLasRutas.OrderByDescending(r => r.Item2).ToList();

                var top10Lejanos = rutasOrdenadas
                    .Take(10)
                    .Select(r => new
                    {
                        Nombre = string.Join(", ", r.Item1.Select(n => n.Nombre)),
                        Distancia = r.Item2
                    })
                    .ToList();

                return Ok(top10Lejanos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud: " + ex.Message);
            }
        }
    }
}


