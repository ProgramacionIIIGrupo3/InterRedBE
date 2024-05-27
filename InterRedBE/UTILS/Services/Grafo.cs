using InterRedBE.UTILS.Models;
using System.Collections.Generic;

namespace InterRedBE.UTILS.Services
{
    public class Grafo<T>
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

        public ListaEnlazadaDoble<(ListaEnlazadaDoble<T>, double)> BuscarTodasLasRutas(int idInicio, int idFin, Dictionary<(int, int), double> distancias)
        {
            var todasLasRutas = new ListaEnlazadaDoble<(ListaEnlazadaDoble<T>, double)>();
            var rutaActual = new ListaEnlazadaDoble<int>();
            var distanciaAcumulada = 0.0;
            var visitados = new ListaEnlazadaDoble<int>();

            BuscarRutasDFS(ObtenerNodo(idInicio), idFin, visitados, rutaActual, distanciaAcumulada, todasLasRutas, distancias);

            return todasLasRutas;
        }

        private void BuscarRutasDFS(NodoGrafo<T> actual, int destino, ListaEnlazadaDoble<int> visitados, ListaEnlazadaDoble<int> rutaActual, double distanciaAcumulada, ListaEnlazadaDoble<(ListaEnlazadaDoble<T>, double)> todasLasRutas, Dictionary<(int, int), double> distancias)
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
                var ruta = new ListaEnlazadaDoble<T>();
                foreach (var id in rutaActual)
                {
                    ruta.InsertarAlFinal(ObtenerNodo(id).Dato);
                }
                todasLasRutas.InsertarAlFinal((ruta, distanciaAcumulada));
            }
            else
            {
                // Continúa con la exploración
                foreach (var arista in actual.Adyacentes)
                {
                    var vecino = arista.Nodo;
                    if (!visitados.Contains(vecino.Id))
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
                        BuscarRutasDFS(vecino, destino, nuevosVisitados, nuevaRutaActual, distanciaAcumulada + distancias[edgeKey], todasLasRutas, distancias);
                    }
                }
            }

            // Elimina el nodo actual de rutaActual y desmarca como visitado
            rutaActual.EliminarDatoX(actual.Id);
            visitados.EliminarDatoX(actual.Id);
        }

        public ListaEnlazadaDoble<(ListaEnlazadaDoble<T>, double)> EncontrarKRutasMasCortas(int idInicio, int idFin, int k, Dictionary<(int, int), double> distancias)
        {
            var todasLasRutas = BuscarTodasLasRutas(idInicio, idFin, distancias);
            var rutasMasCortas = new ListaEnlazadaDoble<(ListaEnlazadaDoble<T>, double)>();

            foreach (var ruta in todasLasRutas)
            {
                // Insertar en la lista de rutas más cortas de forma ordenada por distancia
                var iterador = rutasMasCortas.PrimerNodo;
                while (iterador != null && iterador.Dato.Item2 < ruta.Item2)
                {
                    iterador = (NodoDobleLiga<(ListaEnlazadaDoble<T>, double)>)iterador.LigaSiguiente;
                }

                if (iterador == null)
                {
                    rutasMasCortas.InsertarAlFinal(ruta);
                }
                else
                {
                    var nuevoNodo = new NodoDobleLiga<(ListaEnlazadaDoble<T>, double)>(ruta);
                    nuevoNodo.LigaSiguiente = iterador;
                    nuevoNodo.LigaAnterior = iterador.LigaAnterior;
                    if (iterador.LigaAnterior != null)
                    {
                        iterador.LigaAnterior.LigaSiguiente = nuevoNodo;
                    }
                    iterador.LigaAnterior = nuevoNodo;

                    if (iterador == rutasMasCortas.PrimerNodo)
                    {
                        rutasMasCortas.PrimerNodo = nuevoNodo;
                    }
                }

                // Mantener solo las K rutas más cortas
                if (rutasMasCortas.Count() > k)
                {
                    rutasMasCortas.EliminarAlFinal();
                }
            }

            return rutasMasCortas;
        }

    }
}