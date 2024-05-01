using System.Threading.Tasks;
using InterRedBE.BAL.Bao;
using InterRedBE.DAL.Dao;
using InterRedBE.DAL.Models;
using InterRedBE.UTILS.Models;
using InterRedBE.UTILS.Services;
using System.Linq;
using InterRedBE.DAL.Services;
using InterRedBE.DAL.DTO;

namespace InterRedBE.BAL.Services
{
    public class RutaBAOService : IRutaBAO
    {
        public readonly IRuta _rutaService;

        public RutaBAOService(IRuta ruta)
        {
            _rutaService = ruta;
        }

        public async Task<ListaEnlazadaDoble<(ListaEnlazadaDoble<Departamento>, double)>> EncontrarTodasLasRutasAsync(int idDepartamentoInicio, int idDepartamentoFin, int numeroDeRutas = 5)
        {
            var (redDepartamentos, distancias) = await _rutaService.CargarRutasAsync();
            var todasLasRutas = redDepartamentos.BuscarTodasLasRutas(idDepartamentoInicio, idDepartamentoFin, distancias);

            // Ordenar las rutas por distancia y tomar las primeras rutas
            var rutasOrdenadas = todasLasRutas.OrderBy(r => r.Item2);
            var resultado = new ListaEnlazadaDoble<(ListaEnlazadaDoble<Departamento>, double)>();
            var contador = 0;
            foreach (var ruta in rutasOrdenadas)
            {
                resultado.InsertarAlFinal(ruta);
                contador++;
                if (contador == numeroDeRutas)
                {
                    break;
                }
            }
            return resultado;
        }
        public async Task<ListaEnlazadaDoble<Departamento>> ObtenerTopLugaresCercanos(int idDepartamentoCapital)
        {
            var (redDepartamentos, distancias) = await _rutaService.CargarRutasAsync();
            var departamentoCapital = redDepartamentos.Buscar(idDepartamentoCapital);

            if (departamentoCapital == null)
            {
                return new ListaEnlazadaDoble<Departamento>();
            }

            var lugaresConDistancia = redDepartamentos.Vertices
                .Where(v => v.Id != idDepartamentoCapital)
                .Select(v => (Departamento: v.Valor, Distancia: redDepartamentos.CalcularDistancia(idDepartamentoCapital, v.Id, distancias)))
                .OrderBy(x => x.Distancia)
                .Take(10)
                .ToList();

            var resultado = new ListaEnlazadaDoble<Departamento>();
            foreach (var lugar in lugaresConDistancia)
            {
                resultado.InsertarAlFinal(lugar.Departamento);
            }

            return resultado;

            public async Task<ListaEnlazadaDoble<Departamento>> ObtenerTopLugaresLejanos(int idDepartamentoCapital)
            {
                var (redDepartamentos, distancias) = await _rutaService.CargarRutasAsync();
                var departamentoCapital = redDepartamentos.Buscar(idDepartamentoCapital);

                if (departamentoCapital == null)
                {
                    return new ListaEnlazadaDoble<Departamento>();
                }

                var lugaresConDistancia = redDepartamentos.Vertices
                    .Where(v => v.Id != idDepartamentoCapital)
                    .Select(v => (Departamento: v.Valor, Distancia: redDepartamentos.CalcularDistancia(idDepartamentoCapital, v.Id, distancias)))
                    .OrderByDescending(x => x.Distancia)
                    .Take(10)
                    .ToList();

                var resultado = new ListaEnlazadaDoble<Departamento>();
                foreach (var lugar in lugaresConDistancia)
                {
                    resultado.InsertarAlFinal(lugar.Departamento);
                }

                return resultado;
            }
        }
    }
       
}
