using InterRedBE.DAL.Context;
using InterRedBE.DAL.Dao;
using InterRedBE.DAL.Models;
using InterRedBE.UTILS.Services;
using Microsoft.EntityFrameworkCore;

namespace InterRedBE.DAL.Services
{
    public class RutaService : IRuta
    {

        public readonly InterRedContext _context;

        public RutaService(InterRedContext context)
        {
            _context = context;
        }

        public async Task<(ListaCuadruple<Departamento>, Dictionary<(int, int), double>)> CargarRutasAsync()
        {
            ListaCuadruple<Departamento> lista = new ListaCuadruple<Departamento>();
            Dictionary<(int, int), double> distancias = new Dictionary<(int, int), double>();

            var rutas = await _context.Ruta.Include(r => r.DepartamentoInicio).Include(r => r.DepartamentoFin).ToListAsync();

            foreach (var ruta in rutas)
            {
                lista.AgregarNodoSiNoExiste(ruta.IdDepartamentoInicio, ruta.DepartamentoInicio);
                lista.AgregarNodoSiNoExiste(ruta.IdDepartamentoFin, ruta.DepartamentoFin);
                lista.Conectar(ruta.IdDepartamentoInicio, ruta.IdDepartamentoFin, ruta.Direccion);

                // Guardar la distancia en el diccionario para uso posterior en BAO
                distancias[(ruta.IdDepartamentoInicio, ruta.IdDepartamentoFin)] = ruta.Distancia;
            }

            return (lista, distancias);
        }

        public class ListaCuadruple<T>
        {
            private List<Vertice<T>> vertices = new List<Vertice<T>>();
            private Dictionary<(int, int), List<int>> aristas = new Dictionary<(int, int), List<int>>();

            public T? Buscar(int id)
            {
                var vertice = vertices.FirstOrDefault(v => v.Id == id);
                return vertice?.Valor;
            }

            public IEnumerable<Vertice<T>> Vertices => vertices;

            public double CalcularDistancia(int idInicio, int idFin, Dictionary<(int, int), double> distancias)
            {
                if (distancias.TryGetValue((idInicio, idFin), out double distancia))
                {
                    return distancia;
                }

                return double.PositiveInfinity;
            }

            public void AgregarNodoSiNoExiste(int id, T valor)
            {
                if (!vertices.Any(v => v.Id == id))
                {
                    vertices.Add(new Vertice<T> { Id = id, Valor = valor });
                }
            }

            public void Conectar(int idInicio, int idFin, int peso)
            {
                if (!aristas.ContainsKey((idInicio, idFin)))
                {
                    aristas[(idInicio, idFin)] = new List<int>();
                }

                aristas[(idInicio, idFin)].Add(peso);
            }
        }
    }

}

