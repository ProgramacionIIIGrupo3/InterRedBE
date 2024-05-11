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
using System.Threading.Tasks;

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
                // Obtener los nombres de los departamentos ordenados alfabéticamente o por métrica de cercanía
                var departamentosCercanos = await _context.Departamento
                    .OrderBy(depto => depto.Nombre)                                    
                    .Select(depto => depto.Nombre)
                    .ToListAsync();

                // Ordenar los departamentos según el orden especificado
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
            "Quiche"
        };

                // Filtrar y ordenar los departamentos según el orden especificado
                var departamentosOrdenados = departamentosCercanos
                    .Where(d => ordenDepartamentos.Contains(d))
                    .OrderBy(d => ordenDepartamentos.IndexOf(d))
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
                // Obtener todos los departamentos de la lista de orden especificado
                var ordenDepartamentos = new List<string>
        {
            "Peten",
            "Izabal",
            "Huehuetenango",
            "San Marcos",
            "Retalhuleu",
            "Suchitepequez",
            "Quetzaltenango",
            "Totonicapan",
            "Solola",
            "Zacapa"
        };

                // Obtener los nombres de los departamentos que están en la tabla
                var departamentosExistentes = await _context1.Departamento
                    .Select(depto => depto.Nombre)
                    .ToListAsync();

                
                var departamentosFaltantes = ordenDepartamentos.Except(departamentosExistentes).ToList();

              
                var departamentosOrdenados = ordenDepartamentos.Union(departamentosFaltantes).ToList();

                return Ok(departamentosOrdenados);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud: " + ex.Message);
            }
        }

    }
}


