using InterRedBE.UTILS.Models;
using System.Collections;

namespace InterRedBE.UTILS.Services
{
    public class ListaCuadruple<T>
    {
        private Dictionary<int, NodoCuadruple<T>> nodos;

        public ListaCuadruple()
        {
            nodos = new Dictionary<int, NodoCuadruple<T>>();
        }

        public void AgregarNodoSiNoExiste(int id, T dato)
        {
            if (!nodos.ContainsKey(id))
            {
                nodos[id] = new NodoCuadruple<T>(id, dato);
            }
            // Si el nodo ya existe
        }
        public NodoCuadruple<T> AgregarNodo(int id, T dato)
        {
            if (!nodos.ContainsKey(id))
            {
                var nuevoNodo = new NodoCuadruple<T>(id, dato);
                nodos.Add(id, nuevoNodo);
                return nuevoNodo;
            }
            else
            {
                throw new InvalidOperationException("Un nodo con el mismo ID ya existe.");
            }
        }

        public void Conectar(int idInicio, int idFin, string direccion)
        {
            if (!nodos.ContainsKey(idInicio) || !nodos.ContainsKey(idFin))
            {
                throw new KeyNotFoundException("Uno de los IDs proporcionados no existe en la lista.");
            }

            NodoCuadruple<T> nodoInicio = nodos[idInicio];
            NodoCuadruple<T> nodoFin = nodos[idFin];

            switch (direccion.ToUpper())
            {
                case "NORTE":
                    nodoInicio.Norte = nodoFin;
                    break;
                case "SUR":
                    nodoInicio.Sur = nodoFin;
                    break;
                case "ESTE":
                    nodoInicio.Este = nodoFin;
                    break;
                case "OESTE":
                    nodoInicio.Oeste = nodoFin;
                    break;
                default:
                    throw new ArgumentException("La dirección proporcionada no es válida. Use 'NORTE', 'SUR', 'ESTE', o 'OESTE'.");
            }
        }

        public NodoCuadruple<T> ObtenerNodo(int id)
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


        public ListaEnlazadaDoble<NodoCuadruple<T>> BuscarRuta(int idInicio, int idFin)
        {
            var rutaEncontrada = new ListaEnlazadaDoble<NodoCuadruple<T>>();
            var visitados = new ListaEnlazadaDoble<int>();
            var nodoInicio = ObtenerNodo(idInicio);

            BuscarRutaDFS(nodoInicio, idFin, visitados, rutaEncontrada);

            return rutaEncontrada;
        }

        private void BuscarRutaDFS(NodoCuadruple<T> nodoActual, int idFin, ListaEnlazadaDoble<int> visitados, ListaEnlazadaDoble<NodoCuadruple<T>> rutaEncontrada)
        {
            visitados.InsertarAlFinal(nodoActual.Id);
            rutaEncontrada.InsertarAlFinal(nodoActual);

            if (nodoActual.Id == idFin)
            {
                return;
            }

            foreach (var vecino in new[] { nodoActual.Norte, nodoActual.Sur, nodoActual.Este, nodoActual.Oeste })
            {
                if (vecino != null && !visitados.Contains(vecino.Id))
                {
                    BuscarRutaDFS(vecino, idFin, visitados, rutaEncontrada);
                    if (rutaEncontrada.Contains(nodoActual))
                    {
                        return;
                    }
                }
            }

            rutaEncontrada.EliminarDatoX(nodoActual);
            visitados.EliminarDatoX(nodoActual.Id);
        }

        public ListaEnlazadaDoble<NodoCuadruple<T>> RecorridoEnProfundidad(int idInicio)
        {
            var recorrido = new ListaEnlazadaDoble<NodoCuadruple<T>>();
            var visitados = new ListaEnlazadaDoble<int>();
            var nodoInicio = ObtenerNodo(idInicio);

            RecorridoDFS(nodoInicio, visitados, recorrido);

            return recorrido;
        }

        private void RecorridoDFS(NodoCuadruple<T> nodoActual, ListaEnlazadaDoble<int> visitados, ListaEnlazadaDoble<NodoCuadruple<T>> recorrido)
        {
            visitados.InsertarAlFinal(nodoActual.Id);
            recorrido.InsertarAlFinal(nodoActual);

            foreach (var vecino in new[] { nodoActual.Norte, nodoActual.Sur, nodoActual.Este, nodoActual.Oeste })
            {
                if (vecino != null && !visitados.Contains(vecino.Id))
                {
                    RecorridoDFS(vecino, visitados, recorrido);
                }
            }
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

        private void BuscarRutasDFS(NodoCuadruple<T> actual, int destino, ListaEnlazadaDoble<int> visitados, ListaEnlazadaDoble<int> rutaActual, double distanciaAcumulada, ListaEnlazadaDoble<(ListaEnlazadaDoble<T>, double)> todasLasRutas, Dictionary<(int, int), double> distancias)
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
                        BuscarRutasDFS(vecino, destino, nuevosVisitados, nuevaRutaActual, distanciaAcumulada + distancias[edgeKey], todasLasRutas, distancias);
                    }
                }
            }

            // Elimina el nodo actual de rutaActual y desmarca como visitado
            rutaActual.EliminarDatoX(actual.Id);
            visitados.EliminarDatoX(actual.Id);
        }

    }

}
