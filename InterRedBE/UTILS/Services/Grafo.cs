using InterRedBE.UTILS.Interfaces;
using InterRedBE.UTILS.Models;
using System.Collections.Generic;

namespace InterRedBE.UTILS.Services
{
    public class Grafo<T> where T : IIdentificable
    {
        private Dictionary<int, NodoGrafo<T>> nodos;

        public Grafo()
        {
            nodos = new Dictionary<int, NodoGrafo<T>>();
        }

        public NodoGrafo<T> AgregarNodo(int id, T dato)
        {
            if (!nodos.ContainsKey(id))
            {
                var nuevoNodo = new NodoGrafo<T>(id, dato);
                nodos.Add(id, nuevoNodo);
                return nuevoNodo;
            }
            else
            {
                throw new InvalidOperationException("Un nodo con el mismo ID ya existe.");
            }
        }

        public void Conectar(int idInicio, int idFin, double distancia)
        {
            if (!nodos.ContainsKey(idInicio) || !nodos.ContainsKey(idFin))
            {
                throw new KeyNotFoundException("Uno de los IDs proporcionados no existe en el grafo.");
            }

            NodoGrafo<T> nodoInicio = nodos[idInicio];
            NodoGrafo<T> nodoFin = nodos[idFin];

            nodoInicio.Adyacentes.InsertarAlFinal(new Arista<T>(nodoFin, distancia));
            nodoFin.Adyacentes.InsertarAlFinal(new Arista<T>(nodoInicio, distancia)); // Si el grafo es no dirigido.
        }

        public NodoGrafo<T> ObtenerNodo(int id)
        {
            if (nodos.ContainsKey(id))
            {
                return nodos[id];
            }
            else
            {
                throw new KeyNotFoundException("El nodo con el ID proporcionado no existe.");
            }
        }

        public Dictionary<int, NodoGrafo<T>> ObtenerNodos()
        {
            return nodos;
        }

        public ListaEnlazadaDoble<(ListaEnlazadaDoble<T>, double)> BuscarTodasLasRutas(string idInicio, string idFin, Dictionary<(string, string), double> distancias)
        {
            var todasLasRutas = new ListaEnlazadaDoble<(ListaEnlazadaDoble<T>, double)>();
            var rutaActual = new ListaEnlazadaDoble<string>();
            var distanciaAcumulada = 0.0;
            var visitados = new ListaEnlazadaDoble<string>();

            var nodoInicio = ObtenerNodoPorIdX(idInicio);
            BuscarRutasDFS(nodoInicio, idFin, visitados, rutaActual, distanciaAcumulada, todasLasRutas, distancias);

            return todasLasRutas;
        }

        private void BuscarRutasDFS(NodoGrafo<T> actual, string destino, ListaEnlazadaDoble<string> visitados, ListaEnlazadaDoble<string> rutaActual, double distanciaAcumulada, ListaEnlazadaDoble<(ListaEnlazadaDoble<T>, double)> todasLasRutas, Dictionary<(string, string), double> distancias)
        {
            // Verifica si el nodo actual ya fue visitado en esta ruta
            if (visitados.Contains(actual.Dato.IdX))
            {
                return; // Evita ciclos dentro de la misma ruta de exploración
            }

            // Agrega el nodo actual a la ruta y marca como visitado
            rutaActual.InsertarAlFinal(actual.Dato.IdX);
            visitados.InsertarAlFinal(actual.Dato.IdX);

            if (actual.Dato.IdX == destino)
            {
                var ruta = new ListaEnlazadaDoble<T>();
                foreach (var id in rutaActual)
                {
                    ruta.InsertarAlFinal(ObtenerNodoPorIdX(id).Dato);
                }
                todasLasRutas.InsertarAlFinal((ruta, distanciaAcumulada));
            }
            else
            {
                // Continúa con la exploración
                foreach (var arista in actual.Adyacentes)
                {
                    var vecino = arista.Nodo;
                    if (!visitados.Contains(vecino.Dato.IdX))
                    {
                        var edgeKey = (actual.Dato.IdX, vecino.Dato.IdX);
                        var nuevosVisitados = new ListaEnlazadaDoble<string>();
                        foreach (var visitado in visitados)
                        {
                            nuevosVisitados.InsertarAlFinal(visitado);
                        }
                        var nuevaRutaActual = new ListaEnlazadaDoble<string>();
                        foreach (var nodo in rutaActual)
                        {
                            nuevaRutaActual.InsertarAlFinal(nodo);
                        }
                        BuscarRutasDFS(vecino, destino, nuevosVisitados, nuevaRutaActual, distanciaAcumulada + distancias[edgeKey], todasLasRutas, distancias);
                    }
                }
            }

            // Elimina el nodo actual de rutaActual y desmarca como visitado
            rutaActual.EliminarDatoX(actual.Dato.IdX);
            visitados.EliminarDatoX(actual.Dato.IdX);
        }


        public ListaEnlazadaDoble<(ListaEnlazadaDoble<T>, double)> EncontrarKRutasMasCortas(int idInicio, int idFin, int k, Dictionary<(int, int), double> distancias)
        {
            var rutasMasCortas = new ListaEnlazadaDoble<(ListaEnlazadaDoble<T>, double)>();
            var nodosExcluidos = new HashSet<int>();

            for (int i = 0; i < k; i++)
            {
                var distanciasDesdeInicio = new Dictionary<int, double>();
                var nodoPrevio = new Dictionary<int, int?>();
                var visitados = new HashSet<int>();

                // Inicializar distancias y nodos previos
                foreach (var nodo in nodos.Values)
                {
                    distanciasDesdeInicio[nodo.Id] = double.PositiveInfinity;
                    nodoPrevio[nodo.Id] = null;
                }

                distanciasDesdeInicio[idInicio] = 0;

                // Cola de prioridad usando ListaEnlazadaDoble
                var cola = new ListaEnlazadaDoble<(int, double)>();
                cola.InsertarAlFinal((idInicio, 0));

                bool rutaEncontrada = false;

                while (!cola.ListaVacia())
                {
                    // Extraer el nodo con la menor distancia
                    var nodoActual = cola.PrimerNodo.Dato;
                    cola.EliminarAlInicio();

                    if (visitados.Contains(nodoActual.Item1) || nodosExcluidos.Contains(nodoActual.Item1)) continue;
                    visitados.Add(nodoActual.Item1);

                    var nodoId = nodoActual.Item1;
                    var distanciaActual = nodoActual.Item2;

                    if (nodoId == idFin)
                    {
                        var ruta = new ListaEnlazadaDoble<T>();
                        double distanciaTotal = distanciaActual;

                        // Construir la ruta desde el destino al inicio
                        int? actual = nodoId;
                        while (actual != null)
                        {
                            ruta.InsertarAlInicio(nodos[(int)actual].Dato);
                            actual = nodoPrevio[(int)actual];
                        }

                        rutasMasCortas.InsertarAlFinal((ruta, distanciaTotal));
                        rutaEncontrada = true;
                        break;
                    }

                    foreach (var arista in nodos[nodoId].Adyacentes)
                    {
                        var vecino = arista.Nodo.Id;
                        var distancia = arista.Distancia;
                        var distanciaTentativa = distanciaActual + distancia;

                        if (distanciaTentativa < distanciasDesdeInicio[vecino] && !nodosExcluidos.Contains(vecino))
                        {
                            distanciasDesdeInicio[vecino] = distanciaTentativa;
                            nodoPrevio[vecino] = nodoId;

                            // Inserción ordenada en la cola
                            var iterador = cola.PrimerNodo;
                            while (iterador != null && iterador.Dato.Item2 < distanciaTentativa)
                            {
                                iterador = (NodoDobleLiga<(int, double)>)iterador.LigaSiguiente;
                            }

                            if (iterador == null)
                            {
                                cola.InsertarAlFinal((vecino, distanciaTentativa));
                            }
                            else
                            {
                                var nuevoNodo = new NodoDobleLiga<(int, double)>((vecino, distanciaTentativa));
                                nuevoNodo.LigaSiguiente = iterador;
                                nuevoNodo.LigaAnterior = iterador.LigaAnterior;
                                if (iterador.LigaAnterior != null)
                                {
                                    iterador.LigaAnterior.LigaSiguiente = nuevoNodo;
                                }
                                iterador.LigaAnterior = nuevoNodo;

                                if (iterador == cola.PrimerNodo)
                                {
                                    cola.PrimerNodo = nuevoNodo;
                                }
                            }
                        }
                    }
                }

                if (!rutaEncontrada)
                {
                    break;
                }

                // Excluir los nodos de la última ruta encontrada
                var ultimaRuta = rutasMasCortas.UltimoNodo.Dato.Item1;
                foreach (var nodo in ultimaRuta)
                {
                    nodosExcluidos.Add(nodo.Id);
                }
            }

            return rutasMasCortas;
        }

        public void ConectarPorIdX(string idInicio, string idFin, double distancia)
        {
            var nodoInicio = ObtenerNodoPorIdX(idInicio);
            var nodoFin = ObtenerNodoPorIdX(idFin);

            nodoInicio.Adyacentes.InsertarAlFinal(new Arista<T>(nodoFin, distancia));
            nodoFin.Adyacentes.InsertarAlFinal(new Arista<T>(nodoInicio, distancia)); // Si el grafo es no dirigido.
        }

        private NodoGrafo<T> ObtenerNodoPorIdX(string idX)
        {
            foreach (var nodo in nodos.Values)
            {
                if (nodo.Dato.IdX == idX)
                {
                    return nodo;
                }
            }
            throw new KeyNotFoundException("El nodo con el IdX proporcionado no existe.");
        }



    }
}