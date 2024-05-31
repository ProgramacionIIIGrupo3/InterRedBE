using InterRedBE.DAL.Context;
using InterRedBE.DAL.Dao;
using InterRedBE.DAL.Models;
using InterRedBE.UTILS.Interfaces;
using InterRedBE.UTILS.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterRedBE.DAL.Services
{
    public class RutaService : IRuta
    {
        private readonly IDbContextFactory<InterRedContext> _contextFactory;
        private readonly IMemoryCache _cache;

        public RutaService(IDbContextFactory<InterRedContext> contextFactory, IMemoryCache cache)
        {
            _contextFactory = contextFactory;
            _cache = cache;
        }

        public Task<(Grafo<IIdentificable>, Dictionary<(int, int), double>)> CargarRutasAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<(Grafo<IIdentificable>, Dictionary<(string, string), double>)> CargarRutasNuevoAsync()
        {
            return await _cache.GetOrCreateAsync<(Grafo<IIdentificable>, Dictionary<(string, string), double>)>("grafo_rutas", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30); // Caché por 30 minutos

                Grafo<IIdentificable> grafo = new Grafo<IIdentificable>();
                Dictionary<(string, string), double> distancias = new Dictionary<(string, string), double>();

                using (var context = _contextFactory.CreateDbContext())
                {
                    var rutas = await context.Rutaa.AsNoTracking().ToListAsync();

                    var tasks = rutas.Select(async ruta =>
                    {
                        var entidadInicio = await ObtenerEntidadPorIdX(ruta.EntidadInicio);
                        var entidadFin = await ObtenerEntidadPorIdX(ruta.EntidadFinal);

                        if (entidadInicio != null && entidadFin != null)
                        {
                            lock (grafo)
                            {
                                if (!grafo.ObtenerNodos().ContainsKey(ruta.EntidadInicio))
                                {
                                    grafo.AgregarNodo(ruta.EntidadInicio, entidadInicio);
                                }

                                if (!grafo.ObtenerNodos().ContainsKey(ruta.EntidadFinal))
                                {
                                    grafo.AgregarNodo(ruta.EntidadFinal, entidadFin);
                                }
                            }

                            lock (distancias)
                            {
                                grafo.ConectarPorIdX(ruta.EntidadInicio, ruta.EntidadFinal, ruta.Distancia);
                                distancias[(ruta.EntidadInicio, ruta.EntidadFinal)] = ruta.Distancia;
                            }
                        }
                    });

                    await Task.WhenAll(tasks);
                }

                return (grafo, distancias);
            });
        }

        private async Task<IIdentificable> ObtenerEntidadPorIdX(string idX)
        {
            return await _cache.GetOrCreateAsync<IIdentificable>($"entidad_{idX}", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30); // Caché por 30 minutos

                using (var context = _contextFactory.CreateDbContext())
                {
                    var departamento = await context.Departamento.AsNoTracking().FirstOrDefaultAsync(d => d.IdX == idX);
                    if (departamento != null) return departamento;

                    var municipio = await context.Municipio.AsNoTracking().FirstOrDefaultAsync(m => m.IdX == idX);
                    return municipio;
                }
            });
        }
    }

}
