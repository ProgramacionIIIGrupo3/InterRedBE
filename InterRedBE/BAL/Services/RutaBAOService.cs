using System.Threading.Tasks;
using InterRedBE.BAL.Bao;
using InterRedBE.DAL.Dao;
using InterRedBE.DAL.Models;
using InterRedBE.UTILS.Models;
using InterRedBE.UTILS.Services;
using System.Linq;

namespace InterRedBE.BAL.Services
{
    public class RutaBAOService : IRutaBAO
    {
        private readonly IRuta _rutaService;

        public RutaBAOService(IRuta rutaService)
        {
            _rutaService = rutaService;
        }

        public async Task<ListaEnlazadaDoble<(ListaEnlazadaDoble<Departamento>, double)>> EncontrarTodasLasRutasAsync(int idDepartamentoInicio, int idDepartamentoFin, int numeroDeRutas = 5)
        {
            var (grafoDepartamentos, distancias) = await _rutaService.CargarRutasAsync();
            var todasLasRutas = grafoDepartamentos.BuscarTodasLasRutas(idDepartamentoInicio, idDepartamentoFin, distancias);

            // Crear un diccionario para almacenar las rutas únicas
            var rutasUnicas = new Dictionary<string, (ListaEnlazadaDoble<Departamento>, double)>();

            foreach (var ruta in todasLasRutas)
            {
                var rutaStr = string.Join(",", ruta.Item1.Select(d => d.Id));
                if (!rutasUnicas.ContainsKey(rutaStr))
                {
                    rutasUnicas[rutaStr] = ruta;
                }
            }

            // Ordenar las rutas únicas por distancia
            var rutasOrdenadasUnicas = rutasUnicas.Values.OrderBy(r => r.Item2);

            // Tomar las primeras numeroDeRutas rutas únicas
            var resultado = new ListaEnlazadaDoble<(ListaEnlazadaDoble<Departamento>, double)>();
            foreach (var ruta in rutasOrdenadasUnicas.Take(numeroDeRutas))
            {
                resultado.InsertarAlFinal(ruta);
            }

            return resultado;
        }
    }
}