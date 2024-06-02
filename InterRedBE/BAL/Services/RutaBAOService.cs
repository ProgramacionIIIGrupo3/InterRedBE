using InterRedBE.BAL.Bao;
using InterRedBE.DAL.Dao;
using InterRedBE.DAL.Models;
using InterRedBE.UTILS.Interfaces;
using InterRedBE.UTILS.Services;

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
            throw new System.NotImplementedException();
   
        }

        public async Task<ListaEnlazadaDoble<(ListaEnlazadaDoble<IIdentificable>, double)>> EncontrarTodasLasRutasNuevoAsync(string idXInicio, string idXFin, int numeroDeRutas = 5)
        {
            var (grafoEntidades, distancias) = await _rutaService.CargarRutasNuevoAsync();

            var nodoInicio = grafoEntidades.ObtenerNodos().Values.FirstOrDefault(n => n.IdX == idXInicio);
            var nodoFin = grafoEntidades.ObtenerNodos().Values.FirstOrDefault(n => n.IdX == idXFin);

            if (nodoInicio == null || nodoFin == null)
            {
                return new ListaEnlazadaDoble<(ListaEnlazadaDoble<IIdentificable>, double)>(); // Retorna vacío si no se encuentran los nodos
            }

            var todasLasRutas = grafoEntidades.BuscarTodasLasRutas(nodoInicio.IdX, nodoFin.IdX, distancias);

            var rutasUnicas = new Dictionary<string, (ListaEnlazadaDoble<IIdentificable>, double)>();

            foreach (var ruta in todasLasRutas)
            {
                var rutaStr = string.Join(",", ruta.Item1.Select(d => d.IdX));
                if (!rutasUnicas.ContainsKey(rutaStr))
                {
                    rutasUnicas[rutaStr] = ruta;
                }
            }

            var rutasOrdenadasUnicas = rutasUnicas.Values.OrderBy(r => r.Item2);

            var resultado = new ListaEnlazadaDoble<(ListaEnlazadaDoble<IIdentificable>, double)>();
            foreach (var ruta in rutasOrdenadasUnicas.Take(numeroDeRutas))
            {
                resultado.InsertarAlFinal(ruta);
            }

            return resultado;
        }

        public async Task<ListaEnlazadaDoble<(ListaEnlazadaDoble<IIdentificable>, double)>> EncontrarRutaMasCortaAsync(string idXInicio, string idXFin)
        {
            var (grafoEntidades, distancias) = await _rutaService.CargarRutasNuevoAsync();

            var nodoInicio = grafoEntidades.ObtenerNodos().Values.FirstOrDefault(n => n.IdX == idXInicio);
            var nodoFin = grafoEntidades.ObtenerNodos().Values.FirstOrDefault(n => n.IdX == idXFin);

            if (nodoInicio == null || nodoFin == null)
            {
                return new ListaEnlazadaDoble<(ListaEnlazadaDoble<IIdentificable>, double)>(); // Retorna vacío si no se encuentran los nodos
            }

            var rutaMasCorta = grafoEntidades.EncontrarRutaMasCorta(nodoInicio.IdX, nodoFin.IdX, distancias);
            return rutaMasCorta;
        }

        public async Task<ListaEnlazadaDoble<(ListaEnlazadaDoble<Departamento>, double)>> EncontrarKRutasMasCortasAsync(int idDepartamentoInicio, int idDepartamentoFin, int k)
        {
            throw new System.NotImplementedException();
            //var (grafoDepartamentos, distancias) = await _rutaService.CargarRutasAsync();
            //var rutasMasCortas = grafoDepartamentos.EncontrarKRutasMasCortas(idDepartamentoInicio, idDepartamentoFin, k, distancias);
            //return rutasMasCortas;
        }

        public async Task<ListaEnlazadaDoble<(ListaEnlazadaDoble<IIdentificable>, double)>> EncontrarKRutasMasCortasAsync(string idXInicio, string idXFin, int k)
        {
            var (grafoEntidades, distancias) = await _rutaService.CargarRutasNuevoAsync();

            var nodoInicio = grafoEntidades.ObtenerNodos().Values.FirstOrDefault(n => n.IdX == idXInicio);
            var nodoFin = grafoEntidades.ObtenerNodos().Values.FirstOrDefault(n => n.IdX == idXFin);

            if (nodoInicio == null || nodoFin == null)
            {
                return new ListaEnlazadaDoble<(ListaEnlazadaDoble<IIdentificable>, double)>(); // Retorna vacío si no se encuentran los nodos
            }

            var rutasMasCortas = grafoEntidades.EncontrarKRutasMasCortas(nodoInicio.IdX, nodoFin.IdX, k, distancias);
            return rutasMasCortas;
        }





    }
}
