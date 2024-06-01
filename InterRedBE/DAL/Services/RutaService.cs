using InterRedBE.DAL.Context;
using InterRedBE.DAL.Dao;
using InterRedBE.DAL.Models;
using InterRedBE.UTILS.Interfaces;
using InterRedBE.UTILS.Services;
using Microsoft.EntityFrameworkCore;

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

        // Método para cargar la nueva estructura de rutas
        public async Task<(Grafo<IIdentificable>, Dictionary<(string, string), double>)> CargarRutasNuevoAsync()
        {
            Grafo<IIdentificable> grafo = new Grafo<IIdentificable>();
            Dictionary<(string, string), double> distancias = new Dictionary<(string, string), double>();

            var rutas = await _context.Rutaa.ToListAsync();

            foreach (var ruta in rutas)
            {
                var entidadInicio = await _context.Departamento.FirstOrDefaultAsync(d => d.IdX == ruta.EntidadInicio) as IIdentificable
                                    ?? await _context.Municipio.FirstOrDefaultAsync(m => m.IdX == ruta.EntidadInicio) as IIdentificable;

                var entidadFin = await _context.Departamento.FirstOrDefaultAsync(d => d.IdX == ruta.EntidadFinal) as IIdentificable
                                 ?? await _context.Municipio.FirstOrDefaultAsync(m => m.IdX == ruta.EntidadFinal) as IIdentificable;

                if (entidadInicio == null)
                {
                    throw new Exception($"El nodo con el IdX {ruta.EntidadInicio} no existe.");
                }

                if (entidadFin == null)
                {
                    throw new Exception($"El nodo con el IdX {ruta.EntidadFinal} no existe.");
                }

                // Agregar nodos al grafo si no existen
                if (!grafo.ObtenerNodos().Values.Any(n => n.Dato.IdX == ruta.EntidadInicio))
                {
                    grafo.AgregarNodo(entidadInicio.Id, entidadInicio);
                }

                if (!grafo.ObtenerNodos().Values.Any(n => n.Dato.IdX == ruta.EntidadFinal))
                {
                    grafo.AgregarNodo(entidadFin.Id, entidadFin);
                }

                // Conectar los nodos en el grafo
                grafo.ConectarPorIdX(ruta.EntidadInicio, ruta.EntidadFinal, ruta.Distancia);

                // Guardar la distancia en el diccionario para uso posterior
                distancias[(ruta.EntidadInicio, ruta.EntidadFinal)] = ruta.Distancia;
                distancias[(ruta.EntidadFinal, ruta.EntidadInicio)] = ruta.Distancia; // Asegurarse de incluir ambas direcciones
            }

            return (grafo, distancias);
        }
 

    }
}
