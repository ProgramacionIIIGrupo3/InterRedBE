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
        private readonly IRuta _rutaService;
        private readonly int _Id;

        public RutaBAOService(IRuta rutaService)
        {
            _rutaService = rutaService;
 
        }

        public async Task<ListaEnlazadaDoble<(ListaEnlazadaDoble<Departamento>, double)>> EncontrarTodasLasRutasAsync(int idDepartamentoInicio, int idDepartamentoFin, int numeroDeRutas = 5)
        {
            var (redDepartamentos, distancias) = await _rutaService.CargarRutasAsync();
            var todasLasRutas = redDepartamentos.BuscarTodasLasRutas(idDepartamentoInicio, idDepartamentoFin, distancias);

            // Ordenar las rutas por distancia y tomar las primeras rutas
            var rutasOrdenadas = todasLasRutas.OrderBy(r => r.Item2);
            var resultado = new ListaEnlazadaDoble<(ListaEnlazadaDoble<Departamento>, double)>();

            // Verificar si el número de rutas disponibles es menor que el número solicitado
            int numeroDeRutasDisponibles = todasLasRutas.Count();
            if (numeroDeRutasDisponibles < numeroDeRutas)
            {
                // Si hay menos rutas disponibles que las solicitadas, ajustar el número de rutas a devolver
                numeroDeRutas = numeroDeRutasDisponibles;
            }

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


    }

}
