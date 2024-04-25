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
            // Si el nodo ya existe, no hace nada o maneja según tu lógica de negocio.
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

        // Aquí podrías incluir más métodos según lo que necesites en tu aplicación, como métodos para:
        // - Eliminar un nodo.
        // - Buscar un camino entre dos nodos.
        // - Imprimir la lista o representarla visualmente.
        // - Etc.
    }

}
