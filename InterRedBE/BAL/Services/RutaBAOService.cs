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
        public readonly IRuta _rutaService;

        public RutaBAOService(IRuta ruta)
        {
            _rutaService = ruta;
        }

        public async Task<ListaEnlazadaDoble<(ListaEnlazadaDoble<Departamento>, double)>> EncontrarTodasLasRutasAsync(int idDepartamentoInicio, int idDepartamentoFin, int numeroDeRutas = 5)
        {
            var (redDepartamentos, distancias) = await _rutaService.CargarRutasAsync();
            var todasLasRutas = new ListaEnlazadaDoble<(ListaEnlazadaDoble<Departamento>, double)>();
            var rutaActual = new ListaEnlazadaDoble<int>();
            var distanciaAcumulada = 0.0;

            EncontrarRutasDFS(redDepartamentos.ObtenerNodo(idDepartamentoInicio), idDepartamentoFin, new ListaEnlazadaDoble<int>(), rutaActual, distanciaAcumulada, todasLasRutas, distancias, redDepartamentos);

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

        private void EncontrarRutasDFS(NodoCuadruple<Departamento> actual, int destino, ListaEnlazadaDoble<int> visitados, ListaEnlazadaDoble<int> rutaActual, double distanciaAcumulada, ListaEnlazadaDoble<(ListaEnlazadaDoble<Departamento>, double)> todasLasRutas, Dictionary<(int, int), double> distancias, ListaCuadruple<Departamento> redDepartamentos)
        {
            // Verifica si el nodo actual ya fue visitado en esta ruta
            if (visitados.Contains(actual.Id))
            {
                return; // Evita ciclos dentro de la misma ruta de exploración
            }

            // Agrega el nodo actual a la ruta y marca como visitado
            rutaActual.InsertarAlFinal(actual.Id);
            visitados.InsertarAlFinal(actual.Id);

            if (actual.Id == destino)
            {
                var ruta = new ListaEnlazadaDoble<Departamento>();
                foreach (var id in rutaActual)
                {
                    ruta.InsertarAlFinal(redDepartamentos.ObtenerNodo(id).Dato);
                }
                todasLasRutas.InsertarAlFinal((ruta, distanciaAcumulada));
            }
            else
            {
                // Continúa con la exploración
                foreach (var vecino in new[] { actual.Norte, actual.Sur, actual.Este, actual.Oeste })
                {
                    if (vecino != null && !visitados.Contains(vecino.Id))
                    {
                        var edgeKey = (actual.Id, vecino.Id);
                        var nuevosVisitados = new ListaEnlazadaDoble<int>();
                        foreach (var visitado in visitados)
                        {
                            nuevosVisitados.InsertarAlFinal(visitado);
                        }
                        var nuevaRutaActual = new ListaEnlazadaDoble<int>();
                        foreach (var nodo in rutaActual)
                        {
                            nuevaRutaActual.InsertarAlFinal(nodo);
                        }
                        EncontrarRutasDFS(vecino, destino, nuevosVisitados, nuevaRutaActual, distanciaAcumulada + distancias[edgeKey], todasLasRutas, distancias, redDepartamentos);
                    }
                }
            }

            // Elimina el nodo actual de rutaActual y desmarca como visitado
            rutaActual.EliminarDatoX(actual.Id);
            visitados.EliminarDatoX(actual.Id);
        }
    }
}