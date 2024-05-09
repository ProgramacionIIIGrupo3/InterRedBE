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
        private const int CapitalId = 5;

        public RutaController(IRutaBAO rutaBAOService, InterRedContext context)
        {
            _rutaBAOService = rutaBAOService;
            _context = context;
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
                
                var todasLasRutas = await _rutaBAOService.EncontrarTodasLasRutasAsync(idDepartamentoInicio: 5, idDepartamentoFin: 10, numeroDeRutas: 10);

                
                var departamentos = await _context.Departamento
                    .Where(depto => depto.Nombre != "Guatemala") 
                    .ToListAsync();

             
                var distanciasAGuatemala = new Dictionary<string, double>();

               
                foreach (var ruta in todasLasRutas)
                {
                    foreach (var depto in ruta.Item1)
                    {
                        if (!distanciasAGuatemala.ContainsKey(depto.Nombre))
                        {
                            distanciasAGuatemala.Add(depto.Nombre, 0); 
                        }
                        distanciasAGuatemala[depto.Nombre] += ruta.Item2;
                    }
                }

               
                var departamentosConDistancia = departamentos
                    .Select(depto =>
                    {
                        var distanciaAGuatemala = distanciasAGuatemala.ContainsKey(depto.Nombre) ? distanciasAGuatemala[depto.Nombre] : 0; // Si la distancia no está definida, devuelve 0
                        return new
                        {
                            Nombre = depto.Nombre,
                            DistanciaAGuatemala = distanciaAGuatemala
                        };
                    })
                    .ToList();

             
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

                
                var departamentosOrdenados = departamentosConDistancia
                    .Where(d => ordenDepartamentos.Contains(d.Nombre)) 
                    .OrderBy(d => ordenDepartamentos.IndexOf(d.Nombre) + 0) 
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

                var todasLasRutas = await _rutaBAOService.EncontrarTodasLasRutasAsync(idDepartamentoInicio: 5, idDepartamentoFin: 10, numeroDeRutas: 10);


                var departamentos = await _context.Departamento
                    .Where(depto => depto.Nombre != "Guatemala")
                    .ToListAsync();


                var distanciasAGuatemala = new Dictionary<string, double>();


                foreach (var ruta in todasLasRutas)
                {
                    foreach (var depto in ruta.Item1)
                    {
                        if (!distanciasAGuatemala.ContainsKey(depto.Nombre))
                        {
                            distanciasAGuatemala.Add(depto.Nombre, 0);
                        }
                        distanciasAGuatemala[depto.Nombre] += ruta.Item2;
                    }
                }


                var departamentosConDistancia = departamentos
                    .Select(depto =>
                    {
                        var distanciaAGuatemala = distanciasAGuatemala.ContainsKey(depto.Nombre) ? distanciasAGuatemala[depto.Nombre] : 0; // Si la distancia no está definida, devuelve 0
                        return new
                        {
                            Nombre = depto.Nombre,
                            DistanciaAGuatemala = distanciaAGuatemala
                        };
                    })
                    .ToList();


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


                var departamentosOrdenados = departamentosConDistancia
                    .Where(d => ordenDepartamentos.Contains(d.Nombre))
                    .OrderBy(d => ordenDepartamentos.IndexOf(d.Nombre) + 0)
                    .ToList();

                return Ok(departamentosOrdenados);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud: " + ex.Message);
            }
        }
    }
}
