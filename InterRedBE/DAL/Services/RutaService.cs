using InterRedBE.DAL.Context;
using InterRedBE.DAL.Dao;
using InterRedBE.DAL.Models;
using InterRedBE.UTILS.Interfaces;
using InterRedBE.UTILS.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InterRedBE.DAL.Services
{
    public class RutaService : IRuta
    {
        private readonly InterRedContext _context;

        public RutaService(InterRedContext context)
        {
            _context = context;
        }

        public async Task<(Grafo<IIdentificable>, Dictionary<(int, int), double>)> CargarRutasAsync()
        {
            Grafo<IIdentificable> grafo = new Grafo<IIdentificable>();
            Dictionary<(int, int), double> distancias = new Dictionary<(int, int), double>();

            var rutas = await _context.Ruta
                                      .Include(r => r.DepartamentoInicio)
                                      .Include(r => r.DepartamentoFin)
                                      .Include(r => r.MunicipioInicio)
                                      .Include(r => r.MunicipioFin)
                                      .ToListAsync();

            foreach (var ruta in rutas)
            {
                IIdentificable entidadInicio = null;
                IIdentificable entidadFin = null;

                if (ruta.TipoInicio == TipoEntidad.Departamento)
                {
                    entidadInicio = ruta.DepartamentoInicio;
                }
                else if (ruta.TipoInicio == TipoEntidad.Municipio)
                {
                    entidadInicio = ruta.MunicipioInicio;
                }

                if (ruta.TipoFin == TipoEntidad.Departamento)
                {
                    entidadFin = ruta.DepartamentoFin;
                }
                else if (ruta.TipoFin == TipoEntidad.Municipio)
                {
                    entidadFin = ruta.MunicipioFin;
                }

                if (entidadInicio != null && entidadFin != null)
                {
                    // Agregar nodos al grafo si no existen
                    if (!grafo.ObtenerNodos().ContainsKey(ruta.IdEntidadInicio))
                    {
                        grafo.AgregarNodo(ruta.IdEntidadInicio, entidadInicio);
                    }

                    if (!grafo.ObtenerNodos().ContainsKey(ruta.IdEntidadFin))
                    {
                        grafo.AgregarNodo(ruta.IdEntidadFin, entidadFin);
                    }

                    // Conectar los nodos en el grafo
                    grafo.Conectar(ruta.IdEntidadInicio, ruta.IdEntidadFin, ruta.Distancia);

                    // Guardar la distancia en el diccionario para uso posterior
                    distancias[(ruta.IdEntidadInicio, ruta.IdEntidadFin)] = ruta.Distancia;
                    distancias[(ruta.IdEntidadFin, ruta.IdEntidadInicio)] = ruta.Distancia; // Asegurarse de incluir ambas direcciones
                }
            }

            return (grafo, distancias);
        }
    }
}
