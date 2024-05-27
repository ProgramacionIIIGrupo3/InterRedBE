using InterRedBE.DAL.Context;
using InterRedBE.DAL.Dao;
using InterRedBE.DAL.Models;
using InterRedBE.UTILS.Models;
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

        public async Task<(Grafo<Departamento>, Dictionary<(int, int), double>)> CargarRutasAsync()
        {
            Grafo<Departamento> grafo = new Grafo<Departamento>();
            Dictionary<(int, int), double> distancias = new Dictionary<(int, int), double>();

            var rutas = await _context.Ruta.Include(r => r.DepartamentoInicio).Include(r => r.DepartamentoFin).ToListAsync();

            foreach (var ruta in rutas)
            {
                // Agregar nodos al grafo si no existen
                if (!grafo.ObtenerNodos().ContainsKey(ruta.IdDepartamentoInicio))
                {
                    grafo.AgregarNodo(ruta.IdDepartamentoInicio, ruta.DepartamentoInicio);
                }

                if (!grafo.ObtenerNodos().ContainsKey(ruta.IdDepartamentoFin))
                {
                    grafo.AgregarNodo(ruta.IdDepartamentoFin, ruta.DepartamentoFin);
                }

                // Conectar los nodos en el grafo
                grafo.Conectar(ruta.IdDepartamentoInicio, ruta.IdDepartamentoFin, ruta.Distancia);

                // Guardar la distancia en el diccionario para uso posterior en BAO
                distancias[(ruta.IdDepartamentoInicio, ruta.IdDepartamentoFin)] = ruta.Distancia;
                distancias[(ruta.IdDepartamentoFin, ruta.IdDepartamentoInicio)] = ruta.Distancia; // Asegurarse de incluir ambas direcciones
            }

            return (grafo, distancias);
        }
    }
}