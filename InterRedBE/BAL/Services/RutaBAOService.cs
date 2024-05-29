using System.Threading.Tasks;
using InterRedBE.BAL.Bao;
using InterRedBE.DAL.Dao;
using InterRedBE.DAL.Models;
using InterRedBE.UTILS.Models;
using InterRedBE.UTILS.Services;
using System.Linq;
using InterRedBE.UTILS.Interfaces;

namespace InterRedBE.BAL.Services
{
    public class RutaBAOService : IRutaBAO
    {
        private readonly IRuta _rutaService;

        public RutaBAOService(IRuta rutaService)
        {
            _rutaService = rutaService;
        }

        public async Task<ListaEnlazadaDoble<(ListaEnlazadaDoble<IIdentificable>, double)>> EncontrarTodasLasRutasAsync(int idInicio, TipoEntidad tipoInicio, int idFin, TipoEntidad tipoFin, int numeroDeRutas = 5)
        {
            var (grafoEntidades, distancias) = await _rutaService.CargarRutasAsync();

            if (!grafoEntidades.ObtenerNodos().ContainsKey(idInicio) || !grafoEntidades.ObtenerNodos().ContainsKey(idFin))
            {
                return new ListaEnlazadaDoble<(ListaEnlazadaDoble<IIdentificable>, double)>(); // Retorna vacío si no se encuentran los nodos
            }

            var todasLasRutas = grafoEntidades.BuscarTodasLasRutas(idInicio, idFin, distancias);

            // Crear un diccionario para almacenar las rutas únicas
            var rutasUnicas = new Dictionary<string, (ListaEnlazadaDoble<IIdentificable>, double)>();

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
            var resultado = new ListaEnlazadaDoble<(ListaEnlazadaDoble<IIdentificable>, double)>();
            foreach (var ruta in rutasOrdenadasUnicas.Take(numeroDeRutas))
            {
                resultado.InsertarAlFinal(ruta);
            }

            return resultado;
        }


        public async Task<ListaEnlazadaDoble<(ListaEnlazadaDoble<Departamento>, double)>> EncontrarKRutasMasCortasAsync(int idDepartamentoInicio, int idDepartamentoFin, int k)
        {
            throw new System.NotImplementedException();
            //var (grafoDepartamentos, distancias) = await _rutaService.CargarRutasAsync();
            //var rutasMasCortas = grafoDepartamentos.EncontrarKRutasMasCortas(idDepartamentoInicio, idDepartamentoFin, k, distancias);
            //return rutasMasCortas;
        }
    }
}