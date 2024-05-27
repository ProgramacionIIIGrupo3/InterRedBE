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
        private const int CapitalId = 5;

        public RutaController(IRutaBAO rutaBAOService, InterRedContext context, InterRedContext context1)
        {
            _rutaBAOService = rutaBAOService;
            _context = context;
            _context1 = context1;
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
                    var rutasUnicas = new HashSet<string>(); // Para asegurar rutas únicas

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
                        var rutaStr = string.Join("->", ruta.Item1.Select(d => d.Id)); // Ruta como string única
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
                var departamentosCercanos = await _context.Departamento
                    .OrderBy(depto => depto.Nombre)
                    .Select(depto => new { depto.Id, depto.Nombre, depto.Descripcion, depto.Imagen })
                    .ToListAsync();

                var ordenDepartamentos = new List<string>
                {
                    "Baja Verapaz",
                    "El Progreso",
                    "Jalapa",
                    "Santa Rosa",
                    "Escuintla",
                    "Sacatepéquez",
                    "Chimaltenango",
                    "Jutiapa",
                    "Chiquimula",
                    "Quiché"
                };

                var departamentosOrdenados = departamentosCercanos
                    .Where(d => ordenDepartamentos.Contains(d.Nombre))
                    .OrderBy(d => ordenDepartamentos.IndexOf(d.Nombre))
                    .Take(10)
                    .ToList();

                return Ok(departamentosOrdenados);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud: " + ex.Message);
            }
        }

        [HttpGet("Top10Lejanos")]
        public async Task<IActionResult> GetTop10LejanosALaCapital()
        {
            try
            {
                var ordenDepartamentos = new List<string>
                {
                    "Petén",
                    "Izabal",
                    "Huehuetenango",
                    "San Marcos",
                    "Retalhuleu",
                    "Sacatepéquez",
                    "Quetzaltenango",
                    "Totonicapán",
                    "Sololá",
                    "Zacapa"
                };

                var departamentosExistentes = await _context1.Departamento
                    .Select(depto => new { depto.Id, depto.Nombre, depto.Descripcion, depto.Imagen })
                    .ToListAsync();

                var departamentosOrdenados = departamentosExistentes
                    .Where(depto => ordenDepartamentos.Contains(depto.Nombre))
                    .OrderBy(depto => ordenDepartamentos.IndexOf(depto.Nombre))
                    .ToList();

                return Ok(departamentosOrdenados);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud: " + ex.Message);
            }
        }

        [HttpGet("ruta/dijkstra/{idInicio}/{idFin}/{k}")]
        public async Task<IActionResult> GetKRutasMasCortas(int idInicio, int idFin, int k)
        {
            try
            {
                var rutasMasCortas = await _rutaBAOService.EncontrarKRutasMasCortasAsync(idInicio, idFin, k);
                if (!rutasMasCortas.ListaVacia())
                {
                    var rutasDTO = new ListaEnlazadaDoble<object>();
                    foreach (var (ruta, distanciaTotal) in rutasMasCortas)
                    {
                        var rutaDTO = new ListaEnlazadaDoble<DepartamentoRutaDTO>();
                        foreach (var departamento in ruta)
                        {
                            rutaDTO.InsertarAlFinal(new DepartamentoRutaDTO
                            {
                                Id = departamento.Id,
                                Nombre = departamento.Nombre
                            });
                        }
                        rutasDTO.InsertarAlFinal(new { Ruta = rutaDTO, DistanciaTotal = distanciaTotal });
                    }
                    return Ok(new { Rutas = rutasDTO });
                }
                else
                {
                    return NotFound("No se encontraron rutas entre los departamentos especificados.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al procesar la solicitud: " + ex.Message);
            }
        }

    }
}
