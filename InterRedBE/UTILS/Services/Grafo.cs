using System;
using System.Collections.Generic;
using InterRedBE.UTILS.Interfaces;
using InterRedBE.UTILS.Models;

namespace InterRedBE.UTILS.Services
{
    public class Grafo<T> where T : IIdentificable
    {
        private Dictionary<string, NodoGrafo<T>> nodos;

        public Grafo()
        {
            nodos = new Dictionary<string, NodoGrafo<T>>();
        }

        public NodoGrafo<T> AgregarNodo(string idX, T dato)
        {
            if (!nodos.ContainsKey(idX))
            {
                var nuevoNodo = new NodoGrafo<T>(idX, dato);
                nodos.Add(idX, nuevoNodo);
                return nuevoNodo;
            }
            else
            {
                throw new InvalidOperationException("Un nodo con el mismo ID ya existe.");
            }
        }

        public void ConectarPorIdX(string idInicio, string idFin, double distancia)
        {
            var nodoInicio = ObtenerNodoPorIdX(idInicio);
            var nodoFin = ObtenerNodoPorIdX(idFin);

            nodoInicio.Adyacentes.InsertarAlFinal(new Arista<T>(nodoFin, distancia));
            nodoFin.Adyacentes.InsertarAlFinal(new Arista<T>(nodoInicio, distancia)); // Si el grafo es no dirigido.
        }

        public NodoGrafo<T> ObtenerNodoPorIdX(string idX)
        {
            if (nodos.ContainsKey(idX))
            {
                return nodos[idX];
            }
            else
            {
                throw new KeyNotFoundException("El nodo con el IdX proporcionado no existe.");
            }
        }

        public Dictionary<string, NodoGrafo<T>> ObtenerNodos()
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
            if (visitados.Contains(actual.Dato.IdX))
            {
                return; // Evita ciclos dentro de la misma ruta de exploración
            }

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
                foreach (var arista in actual.Adyacentes)
                {
                    var vecino = arista.Nodo;
                    if (!visitados.Contains(vecino.Dato.IdX))
                    {
                        var edgeKey = (actual.Dato.IdX, vecino.Dato.IdX);
                        var nuevosVisitados = CopiarLista(visitados);
                        var nuevaRutaActual = CopiarLista(rutaActual);
                        BuscarRutasDFS(vecino, destino, nuevosVisitados, nuevaRutaActual, distanciaAcumulada + distancias[edgeKey], todasLasRutas, distancias);
                    }
                }
            }

            rutaActual.EliminarDatoX(actual.Dato.IdX);
            visitados.EliminarDatoX(actual.Dato.IdX);
        }

        private ListaEnlazadaDoble<T> CopiarLista(ListaEnlazadaDoble<T> lista)
        {
            var nuevaLista = new ListaEnlazadaDoble<T>();
            foreach (var item in lista)
            {
                nuevaLista.InsertarAlFinal(item);
            }
            return nuevaLista;
        }

        private ListaEnlazadaDoble<string> CopiarLista(ListaEnlazadaDoble<string> lista)
        {
            var nuevaLista = new ListaEnlazadaDoble<string>();
            foreach (var item in lista)
            {
                nuevaLista.InsertarAlFinal(item);
            }
            return nuevaLista;
        }

        public ListaEnlazadaDoble<(ListaEnlazadaDoble<T>, double)> EncontrarRutaMasCorta(string idInicio, string idFin, Dictionary<(string, string), double> distancias)
        {
            var distanciasDesdeInicio = new Dictionary<string, double>();
            var nodoPrevio = new Dictionary<string, string?>();
            var visitados = new ListaEnlazadaDoble<string>();
            var rutaMasCorta = new ListaEnlazadaDoble<(ListaEnlazadaDoble<T>, double)>();

            foreach (var nodo in nodos.Values)
            {
                distanciasDesdeInicio[nodo.Dato.IdX] = double.PositiveInfinity;
                nodoPrevio[nodo.Dato.IdX] = null;
            }

            distanciasDesdeInicio[idInicio] = 0;

            var cola = new ListaEnlazadaDoble<(string, double)>();
            cola.InsertarAlFinal((idInicio, 0));

            while (!cola.ListaVacia())
            {
                var nodoActual = cola.PrimerNodo.Dato;
                cola.EliminarAlInicio();

                if (visitados.Contains(nodoActual.Item1)) continue;
                visitados.InsertarAlFinal(nodoActual.Item1);

                var nodoId = nodoActual.Item1;
                var distanciaActual = nodoActual.Item2;

                if (nodoId == idFin)
                {
                    var ruta = new ListaEnlazadaDoble<T>();
                    double distanciaTotal = distanciaActual;

                    string? actual = nodoId;
                    while (actual != null)
                    {
                        ruta.InsertarAlInicio(nodos[actual].Dato);
                        actual = nodoPrevio[actual];
                    }

                    rutaMasCorta.InsertarAlFinal((ruta, distanciaTotal));
                    break;
                }

                foreach (var arista in nodos[nodoId].Adyacentes)
                {
                    var vecino = arista.Nodo.Dato.IdX;
                    var distancia = arista.Distancia;
                    var distanciaTentativa = distanciaActual + distancia;

                    if (distanciaTentativa < distanciasDesdeInicio[vecino])
                    {
                        distanciasDesdeInicio[vecino] = distanciaTentativa;
                        nodoPrevio[vecino] = nodoId;

                        var iterador = cola.PrimerNodo;
                        while (iterador != null && iterador.Dato.Item2 < distanciaTentativa)
                        {
                            iterador = (NodoDobleLiga<(string, double)>)iterador.LigaSiguiente;
                        }

                        if (iterador == null)
                        {
                            cola.InsertarAlFinal((vecino, distanciaTentativa));
                        }
                        else
                        {
                            var nuevoNodo = new NodoDobleLiga<(string, double)>((vecino, distanciaTentativa));
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

            return rutaMasCorta;
        }

        public ListaEnlazadaDoble<(ListaEnlazadaDoble<T>, double)> EncontrarKRutasMasCortas(string idInicio, string idFin, int k, Dictionary<(string, string), double> distancias)
        {
            var rutasMasCortas = new ListaEnlazadaDoble<(ListaEnlazadaDoble<T>, double)>();

            for (int i = 0; i < k; i++)
            {
                var distanciasDesdeInicio = new Dictionary<string, double>();
                var nodoPrevio = new Dictionary<string, string?>();
                var visitados = new ListaEnlazadaDoble<string>();

                foreach (var nodo in nodos.Values)
                {
                    distanciasDesdeInicio[nodo.Dato.IdX] = double.PositiveInfinity;
                    nodoPrevio[nodo.Dato.IdX] = null;
                }

                distanciasDesdeInicio[idInicio] = 0;

                var cola = new ListaEnlazadaDoble<(string, double)>();
                cola.InsertarAlFinal((idInicio, 0));

                bool rutaEncontrada = false;

                while (!cola.ListaVacia())
                {
                    var nodoActual = cola.PrimerNodo.Dato;
                    cola.EliminarAlInicio();

                    if (visitados.Contains(nodoActual.Item1)) continue;
                    visitados.InsertarAlFinal(nodoActual.Item1);

                    var nodoId = nodoActual.Item1;
                    var distanciaActual = nodoActual.Item2;

                    if (nodoId == idFin)
                    {
                        var ruta = new ListaEnlazadaDoble<T>();
                        double distanciaTotal = distanciaActual;

                        string? actual = nodoId;
                        while (actual != null)
                        {
                            ruta.InsertarAlInicio(nodos[actual].Dato);
                            actual = nodoPrevio[actual];
                        }

                        rutasMasCortas.InsertarAlFinal((ruta, distanciaTotal));
                        rutaEncontrada = true;
                        break;
                    }

                    foreach (var arista in nodos[nodoId].Adyacentes)
                    {
                        var vecino = arista.Nodo.Dato.IdX;
                        var distancia = arista.DistanciaConPenalizacion; // Usamos la distancia con penalización
                        var distanciaTentativa = distanciaActual + distancia;

                        if (distanciaTentativa < distanciasDesdeInicio[vecino])
                        {
                            distanciasDesdeInicio[vecino] = distanciaTentativa;
                            nodoPrevio[vecino] = nodoId;

                            var iterador = cola.PrimerNodo;
                            while (iterador != null && iterador.Dato.Item2 < distanciaTentativa)
                            {
                                iterador = (NodoDobleLiga<(string, double)>)iterador.LigaSiguiente;
                            }

                            if (iterador == null)
                            {
                                cola.InsertarAlFinal((vecino, distanciaTentativa));
                            }
                            else
                            {
                                var nuevoNodo = new NodoDobleLiga<(string, double)>((vecino, distanciaTentativa));
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

                // Penalizar las aristas de la última ruta encontrada para buscar nuevas rutas
                var ultimaRuta = rutasMasCortas.UltimoNodo.Dato.Item1;
                foreach (var nodo in ultimaRuta)
                {
                    var nodoGrafo = nodos[nodo.IdX];
                    foreach (var arista in nodoGrafo.Adyacentes)
                    {
                        if (ultimaRuta.Contains(arista.Nodo.Dato))
                        {
                            arista.Penalizacion += 1000; // Penalización alta para desincentivar esta arista
                        }
                    }
                }
            }

            return rutasMasCortas;
        }







    }
}
