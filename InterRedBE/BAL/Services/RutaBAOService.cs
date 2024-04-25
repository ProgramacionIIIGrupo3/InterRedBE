using System.Collections.Generic;
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

        public async Task<List<(ListaEnlazadaDoble<Departamento>, double)>> EncontrarTodasLasRutasAsync(int idDepartamentoInicio, int idDepartamentoFin, int numeroDeRutas = 5)
        {
            var (redDepartamentos, distancias) = await _rutaService.CargarRutasAsync();
            var todasLasRutas = new List<(ListaEnlazadaDoble<Departamento>, double)>();
            var rutaActual = new List<int>();
            var distanciaAcumulada = 0.0;

            EncontrarRutasDFS(redDepartamentos.ObtenerNodo(idDepartamentoInicio), idDepartamentoFin, new HashSet<int>(), rutaActual, distanciaAcumulada, todasLasRutas, distancias, redDepartamentos);

            // Ordenar las rutas por distancia y tomar las primeras rutas
            return todasLasRutas.OrderBy(r => r.Item2).Take(numeroDeRutas).ToList();
        }


        private void EncontrarRutasDFS(NodoCuadruple<Departamento> actual, int destino, HashSet<int> visitados, List<int> rutaActual, double distanciaAcumulada, List<(ListaEnlazadaDoble<Departamento>, double)> todasLasRutas, Dictionary<(int, int), double> distancias, ListaCuadruple<Departamento> redDepartamentos)
        {
            // Verifica si el nodo actual ya fue visitado en esta ruta
            if (visitados.Contains(actual.Id))
            {
                return; // Evita ciclos dentro de la misma ruta de exploración
            }

            // Agrega el nodo actual a la ruta y marca como visitado
            rutaActual.Add(actual.Id);
            visitados.Add(actual.Id);

            if (actual.Id == destino)
            {
                var ruta = new ListaEnlazadaDoble<Departamento>();
                foreach (var id in rutaActual)
                {
                    ruta.InsertarAlFinal(redDepartamentos.ObtenerNodo(id).Dato);
                }
                todasLasRutas.Add((ruta, distanciaAcumulada));
               
            }
            else
            {
                // Continúa con la exploración
                foreach (var vecino in new[] { actual.Norte, actual.Sur, actual.Este, actual.Oeste })
                {
                    if (vecino != null && !visitados.Contains(vecino.Id))
                    {
                        var edgeKey = (actual.Id, vecino.Id);
                        EncontrarRutasDFS(vecino, destino, new HashSet<int>(visitados), new List<int>(rutaActual), distanciaAcumulada + distancias[edgeKey], todasLasRutas, distancias, redDepartamentos);
                    }
                }
            }

            // Elimina el nodo actual de rutaActual y desmarca como visitado
            rutaActual.RemoveAt(rutaActual.Count - 1);
            visitados.Remove(actual.Id);
        }


    }
}
