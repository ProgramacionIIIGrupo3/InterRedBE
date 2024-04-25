using InterRedBE.BAL.Bao;
using InterRedBE.DAL.Dao;
using InterRedBE.DAL.Models;
using InterRedBE.UTILS.Models;
using InterRedBE.UTILS.Services;

namespace InterRedBE.BAL.Services
{
    public class RutaBAOService : IRutaBAO
    {
        public readonly IRuta _rutaService;

        public RutaBAOService(IRuta ruta)
        {
            _rutaService = ruta;
        }

        public async Task<(ListaEnlazadaDoble<Departamento>, double)> EncontrarRutaAsync(int idDepartamentoInicio, int idDepartamentoFin)
        {
            var (redDepartamentos, distancias) = await _rutaService.CargarRutasAsync();

            var visitados = new HashSet<int>();
            var cola = new ListaEnlazadaDoble<NodoCuadruple<Departamento>>();
            var ruta = new Dictionary<int, (int previo, double distanciaAcumulada)>();

            cola.InsertarAlFinal(redDepartamentos.ObtenerNodo(idDepartamentoInicio));
            visitados.Add(idDepartamentoInicio);
            ruta[idDepartamentoInicio] = (-1, 0);

            while (!cola.ListaVacia())
            {
                var actual = cola.PrimerNodo.Dato;
                cola.EliminarAlInicio();

                if (actual.Id == idDepartamentoFin)
                {
                    return ReconstruirRuta(ruta, actual.Id, redDepartamentos);
                }

                foreach (var vecino in new[] { actual.Norte, actual.Sur, actual.Este, actual.Oeste })
                {
                    if (vecino != null && !visitados.Contains(vecino.Id))
                    {
                        var edgeKey = (actual.Id, vecino.Id);
                        double nuevaDistancia = ruta[actual.Id].distanciaAcumulada + distancias[edgeKey];
                        cola.InsertarAlFinal(vecino);
                        visitados.Add(vecino.Id);
                        ruta[vecino.Id] = (actual.Id, nuevaDistancia);
                    }
                }
            }

            return (new ListaEnlazadaDoble<Departamento>(), 0);  // Si no se encuentra ruta
        }

        private (ListaEnlazadaDoble<Departamento>, double) ReconstruirRuta(Dictionary<int, (int previo, double distancia)> ruta, int nodoFinal, ListaCuadruple<Departamento> redDepartamentos)
        {
            var camino = new ListaEnlazadaDoble<Departamento>();
            double distanciaTotal = ruta[nodoFinal].distancia;
            var actual = nodoFinal;
            while (actual != -1)
            {
                var nodoActual = redDepartamentos.ObtenerNodo(actual).Dato;
                camino.InsertarAlInicio(nodoActual);
                actual = ruta[actual].previo;
            }
            return (camino, distanciaTotal);
        }
    }
}
