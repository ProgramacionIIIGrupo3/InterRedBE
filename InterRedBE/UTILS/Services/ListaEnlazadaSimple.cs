using InterRedBE.UTILS.Models;
using System.Collections;

namespace InterRedBE.UTILS.Services
{
    public class ListaEnlazadaSimple<T> : IEnumerable<T>
    {
        private Nodo<T>? PrimerNodo { get; set; }
        private Nodo<T>? UltimoNodo { get; set; }

        public ListaEnlazadaSimple()
        {
            PrimerNodo = null;
            UltimoNodo = null;
        }

        public bool ListaVacia()
        {
            return PrimerNodo == null;
        }

        // Metodo para insertar un nodo al inicio de la lista
        public string InsertarAlInicio(T dato)
        {
            Nodo<T> nuevoNodo = new(dato);
            if (ListaVacia())
            {
                PrimerNodo = nuevoNodo;
                UltimoNodo = nuevoNodo;
            }
            else
            {
                nuevoNodo.LigaSiguiente = PrimerNodo;
                PrimerNodo = nuevoNodo;
            }
            return "Nodo insertado al inicio";
        }

        // Metodo para insertar un nodo al final de la lista
        public string InsertarAlFinal(T dato)
        {
            Nodo<T> nuevoNodo = new(dato);
            if (ListaVacia())
            {
                PrimerNodo = nuevoNodo;
                UltimoNodo = nuevoNodo;
            }
            else
            {
                UltimoNodo.LigaSiguiente = nuevoNodo;
                UltimoNodo = nuevoNodo;
            }
            return "Nodo insertado al final";
        }

        // Metodo para insertar un nodo antes de una posicion en especifico
        public string InsertarAntesDePosicionX(T dato, int posicion)
        {
            Nodo<T> nuevoNodo = new(dato);
            if (ListaVacia())
            {
                return "La lista esta vacia";
            }

            if (posicion <= 0)
            {
                return "La posicion especificada debe ser un numereo mayor a 0";
            }

            if (posicion == 1)
            {
                return InsertarAlInicio(dato);
            }

            else
            {
                Nodo<T>? nodoActual = PrimerNodo;
                Nodo<T>? nodoAnterior = null;
                int contador = 2;
                while (nodoActual != null && contador < posicion)
                {
                    nodoAnterior = nodoActual;
                    nodoActual = nodoActual.LigaSiguiente;
                    contador++;
                }
                if (nodoActual == null)
                {
                    return "La posicion especificada esta fuera de rango";
                }
                else
                {
                    nuevoNodo.LigaSiguiente = nodoActual;
                    nodoAnterior.LigaSiguiente = nuevoNodo;
                    return $"Nodo insertado antes de la posicion: {posicion}";
                }
            }
        }

        // Metodo para insertar un nodo despues de una posicion en especifico
        public string InsertarDespuesDePosicionX(T dato, int posicion)
        {
            Nodo<T> nuevoNodo = new(dato);
            if (ListaVacia())
            {
                return "La lista esta vacia";
            }

            if (posicion <= 0)
            {
                return "La posicion especificada debe ser un numereo mayor a 0";
            }

            Nodo<T>? nodoActual = PrimerNodo;
            int contador = 1;
            while (nodoActual != null && contador < posicion)
            {
                nodoActual = nodoActual.LigaSiguiente;
                contador++;
            }

            if (nodoActual == null)
            {
                return "La posicion especificada esta fuera de rango";
            }
            else
            {
                nuevoNodo.LigaSiguiente = nodoActual.LigaSiguiente;
                nodoActual.LigaSiguiente = nuevoNodo;
                return $"Nodo insertado despues de la posicion: {posicion}";
            }
        }

        // Metodo para insertar un nodo en una posicion en especifico
        public string InsertarEnPosicionX(T dato, int posicion)
        {
            Nodo<T> nuevoNodo = new(dato);
            if (ListaVacia())
            {
                return "La lista esta vacia";
            }

            if (posicion <= 0)
            {
                return "La posicion especificada debe ser un numero mayor a 0";
            }

            if (posicion == 1)
            {
                return InsertarAlInicio(dato);
            }

            Nodo<T>? nodoActual = PrimerNodo;
            Nodo<T>? nodoAnterior = null;
            int contador = 1;
            while (nodoActual != null && contador < posicion)
            {
                nodoAnterior = nodoActual;
                nodoActual = nodoActual.LigaSiguiente;
                contador++;
            }

            if (nodoActual == null)
            {
                return "La posicion especificada esta fuera de rango";
            }
            else
            {
                nuevoNodo.LigaSiguiente = nodoActual;
                nodoAnterior.LigaSiguiente = nuevoNodo;
                return $"Nodo insertado en la posicion: {posicion}";
            }
        }

        // Metodo para Insertar un nodo antes de un dato en especifico
        public string InsertarAntesDeDatoX(T dato, T datoX)
        {
            Nodo<T> nuevoNodo = new(dato);
            if (ListaVacia())
            {
                return "La lista esta vacia";
            }

            Nodo<T>? nodoActual = PrimerNodo;
            Nodo<T>? nodoAnterior = null;
            while (nodoActual != null && !nodoActual.Dato.Equals(datoX))
            {
                nodoAnterior = nodoActual;
                nodoActual = nodoActual.LigaSiguiente;
            }

            if (nodoActual == null)
            {
                return "El dato especificado no se encuentra en la lista";
            }
            else
            {
                if (nodoActual == PrimerNodo)
                {
                    return InsertarAlInicio(dato);
                }
                else
                {
                    nuevoNodo.LigaSiguiente = nodoActual;
                    nodoAnterior.LigaSiguiente = nuevoNodo;
                    return $"Nodo insertado antes del dato: {datoX}";
                }
            }
        }

        // Metodo para Insertar un nodo despues de un dato en especifico
        public string InsertarDespuesDeDatoX(T dato, T datoX)
        {
            Nodo<T> nuevoNodo = new(dato);
            if (ListaVacia())
            {
                return "La lista esta vacia";
            }

            Nodo<T>? nodoActual = PrimerNodo;
            while (nodoActual != null && !nodoActual.Dato.Equals(datoX))
            {
                nodoActual = nodoActual.LigaSiguiente;
            }

            if (nodoActual == null)
            {
                return "El dato especificado no se encuentra en la lista";
            }
            else
            {
                nuevoNodo.LigaSiguiente = nodoActual.LigaSiguiente;
                nodoActual.LigaSiguiente = nuevoNodo;
                return $"Nodo insertado despues del dato: {datoX}";
            }
        }

        // Metodo para eliminar un nodo al inicio de la lista
        public string EliminarAlInicio()
        {
            if (ListaVacia())
            {
                return "La lista esta vacia";
            }

            if (PrimerNodo == UltimoNodo)
            {
                PrimerNodo = null;
                UltimoNodo = null;
            }
            else
            {
                Nodo<T>? nodoTemporal;
                nodoTemporal = PrimerNodo;
                PrimerNodo = PrimerNodo.LigaSiguiente;
                nodoTemporal = null;
            }
            return "¡Se ha eliminado el nodo al inicio!";
        }

        // Metodo para eliminar un nodo al final de la lista
        public string EliminarAlFinal()
        {
            if (ListaVacia())
            {
                return "La lista esta vacia";
            }

            if (PrimerNodo == UltimoNodo)
            {
                PrimerNodo = null;
                UltimoNodo = null;
            }
            else
            {
                Nodo<T>? nodoActual = PrimerNodo;
                while (nodoActual?.LigaSiguiente != UltimoNodo)
                {
                    nodoActual = nodoActual?.LigaSiguiente;
                }

                nodoActual.LigaSiguiente = null;
                UltimoNodo = nodoActual;

            }
            return "¡Se ha eliminado el nodo al final!";
        }

        // Metodo para eliminar un nodo en una posicion en especifico
        public string EliminarEnPosicionX(int posicion)
        {
            if (ListaVacia())
            {
                return "La lista esta vacia";
            }

            if (posicion <= 0)
            {
                return "La posicion especificada debe ser un numero mayor a 0";
            }

            if (posicion == 1)
            {
                return EliminarAlInicio();
            }

            Nodo<T>? nodoActual = PrimerNodo;
            Nodo<T>? nodoAnterior = null;
            int contador = 1;
            while (nodoActual != null && contador < posicion)
            {
                nodoAnterior = nodoActual;
                nodoActual = nodoActual.LigaSiguiente;
                contador++;
            }

            if (nodoActual == null)
            {
                return "La posicion especificada esta fuera de rango";
            }

            if (nodoAnterior != null)
            {
                nodoAnterior.LigaSiguiente = nodoActual.LigaSiguiente;
                nodoActual = null;
            }
            else
            {
                PrimerNodo = nodoActual.LigaSiguiente;
                nodoActual = null;
            }

            if (nodoActual == UltimoNodo)
            {
                UltimoNodo = nodoAnterior;
            }

            return $"¡Se ha eliminado el nodo en la posicion: {posicion}!";

        }

        // Metodo para eliminar un nodo antes de una posicion en especifico
        public string EliminarAntesDePosicionX(int posicion)
        {
            if (ListaVacia())
            {
                return "La lista esta vacia";
            }

            if (posicion <= 0)
            {
                return "La posicion especificada debe ser un numero mayor a 0";
            }

            if (posicion == 1)
            {
                return "No hay un nodo antes de la posicion 1";
            }

            Nodo<T>? nodoActual = PrimerNodo;
            Nodo<T>? nodoAnterior = null;
            Nodo<T>? nodoAnteriorAnterior = null;
            int contador = 1;
            while (nodoActual != null && contador < posicion)
            {
                nodoAnteriorAnterior = nodoAnterior;
                nodoAnterior = nodoActual;
                nodoActual = nodoActual.LigaSiguiente;
                contador++;
            }

            if (nodoActual == null)
            {
                return "La posicion especificada esta fuera de rango";
            }

            if (nodoAnteriorAnterior == null)
            {
                return "No hay un nodo antes de la posicion especificada";
            }

            nodoAnteriorAnterior.LigaSiguiente = nodoActual;
            nodoAnterior = null;

            return $"¡Se ha eliminado el nodo antes de la posicion: {posicion}!";
        }

        // Metodo para eliminar un nodo despues de una posicion en especifico
        public string EliminarDespuesDePosicionX(int posicion)
        {
            if (ListaVacia())
            {
                return "La lista esta vacia";
            }

            if (posicion <= 0)
            {
                return "La posicion especificada debe ser un numero mayor a 0";
            }

            Nodo<T>? nodoActual = PrimerNodo;
            int contador = 1;
            while (nodoActual != null && contador < posicion)
            {
                nodoActual = nodoActual.LigaSiguiente;
                contador++;
            }

            if (nodoActual == null)
            {
                return "La posicion especificada esta fuera de rango";
            }

            if (nodoActual.LigaSiguiente == null)
            {
                return "No hay un nodo despues de la posicion especificada";
            }

            Nodo<T>? nodoTemporal = nodoActual.LigaSiguiente;
            nodoActual.LigaSiguiente = nodoTemporal?.LigaSiguiente;
            nodoTemporal = null;

            return $"¡Se ha eliminado el nodo despues de la posicion: {posicion}!";
        }

        // Metodo para eliminar un nodo antes de un dato en especifico
        public string EliminarAntesDeDatoX(T datoX)
        {
            if (ListaVacia())
            {
                return "La lista esta vacia";
            }

            Nodo<T>? nodoActual = PrimerNodo;
            Nodo<T>? nodoAnterior = null;
            Nodo<T>? nodoAnteriorAnterior = null;
            while (nodoActual != null && !nodoActual.Dato.Equals(datoX))
            {
                nodoAnteriorAnterior = nodoAnterior;
                nodoAnterior = nodoActual;
                nodoActual = nodoActual.LigaSiguiente;
            }

            if (nodoActual == null)
            {
                return "El dato especificado no se encuentra en la lista";
            }

            if (nodoAnteriorAnterior == null)
            {
                return "No hay un nodo antes del dato especificado";
            }

            nodoAnteriorAnterior.LigaSiguiente = nodoActual;
            nodoAnterior = null;

            return $"¡Se ha eliminado el nodo antes del dato: {datoX}!";
        }

        // Metodo para eliminar un nodo despues de un dato en especifico
        public string EliminarDespuesDeDatoX(T datoX)
        {
            if (ListaVacia())
            {
                return "La lista esta vacia";
            }

            Nodo<T>? nodoActual = PrimerNodo;
            while (nodoActual != null && !nodoActual.Dato.Equals(datoX))
            {
                nodoActual = nodoActual.LigaSiguiente;
            }

            if (nodoActual == null)
            {
                return "El dato especificado no se encuentra en la lista";
            }

            if (nodoActual.LigaSiguiente == null)
            {
                return "No hay un nodo despues del dato especificado";
            }

            Nodo<T>? nodoTemporal = nodoActual.LigaSiguiente;
            nodoActual.LigaSiguiente = nodoTemporal?.LigaSiguiente;
            nodoTemporal = null;

            return $"¡Se ha eliminado el nodo despues del dato: {datoX}!";
        }

        // Metodo para eliminar un dato en especifico
        public string EliminarDatoX(T datoX)
        {
            if (ListaVacia())
            {
                return "La lista esta vacia";
            }

            Nodo<T>? nodoActual = PrimerNodo;
            Nodo<T>? nodoAnterior = null;
            while (nodoActual != null && !nodoActual.Dato.Equals(datoX))
            {
                nodoAnterior = nodoActual;
                nodoActual = nodoActual.LigaSiguiente;
            }

            if (nodoActual == null)
            {
                return "El dato especificado no se encuentra en la lista";
            }

            if (nodoAnterior != null)
            {
                nodoAnterior.LigaSiguiente = nodoActual.LigaSiguiente;
                nodoActual = null;
            }
            else
            {
                PrimerNodo = nodoActual.LigaSiguiente;
                nodoActual = null;
            }

            if (nodoActual == UltimoNodo)
            {
                UltimoNodo = nodoAnterior;
            }

            return $"¡Se ha eliminado el dato: {datoX}!";
        }

        // Metodo para buscar un dato en la lista
        public string BuscarDato(T dato)
        {
            if (ListaVacia())
            {
                return "La lista esta vacia";
            }

            Nodo<T>? nodoActual = PrimerNodo;
            while (nodoActual != null && !nodoActual.Dato.Equals(dato))
            {
                nodoActual = nodoActual.LigaSiguiente;
            }

            if (nodoActual == null)
            {
                return "El dato no se encuentra en la lista";
            }
            else
            {
                return "El dato se encuentra en la lista";
            }
        }

        // Metodo para ordenar la lista de forma ascendente o alfabeticamente dependiente el tipo que vaya a ser
        public string OrdenarListaAscendente()
        {
            if (ListaVacia())
            {
                return "La lista esta vacia";
            }

            Nodo<T>? nodoActual = PrimerNodo;
            Nodo<T>? nodoSiguiente = null;
            T datoTemporal;

            while (nodoActual != null)
            {
                nodoSiguiente = nodoActual.LigaSiguiente;
                while (nodoSiguiente != null)
                {
                    if (Comparer.Default.Compare(nodoActual.Dato, nodoSiguiente.Dato) > 0)
                    {
                        datoTemporal = nodoActual.Dato;
                        nodoActual.Dato = nodoSiguiente.Dato;
                        nodoSiguiente.Dato = datoTemporal;
                    }
                    nodoSiguiente = nodoSiguiente.LigaSiguiente;
                }
                nodoActual = nodoActual.LigaSiguiente;
            }

            return "¡La lista ha sido ordenada de forma ascendente!";
        }


        // Metodo para recorrar la lista con IEnumerator
        public IEnumerator<T> GetEnumerator()
        {
            Nodo<T>? nodoActual = PrimerNodo;
            while (nodoActual != null)
            {
                yield return nodoActual.Dato;
                nodoActual = nodoActual.LigaSiguiente;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class ListaEnlazadoDoble<T> : IEnumerable<T>
    {
        public NodoDobleLiga<T>? PrimerNodo { get; set; }
        public NodoDobleLiga<T>? UltimoNodo { get; set; }

        public ListaEnlazadoDoble()
        {
            PrimerNodo = null;
            UltimoNodo = null;
        }

        public bool ListaVacia()
        {
            return PrimerNodo == null;
        }

        // Metodo para insertar un nodo al inicio de la lista
        public string InsertarAlInicio(T dato)
        {
            NodoDobleLiga<T> nuevoNodo = new(dato);
            if (ListaVacia())
            {
                PrimerNodo = nuevoNodo;
                UltimoNodo = nuevoNodo;
            }
            else
            {
                nuevoNodo.LigaSiguiente = PrimerNodo;
                PrimerNodo.LigaAnterior = nuevoNodo;
                PrimerNodo = nuevoNodo;
            }
            return "Nodo insertado al inicio";
        }

        // Metodo para insertar un nodo al final de la lista
        public string InsertarAlFinal(T dato)
        {
            NodoDobleLiga<T> nuevoNodo = new(dato);
            if (ListaVacia())
            {
                PrimerNodo = nuevoNodo;
                UltimoNodo = nuevoNodo;
            }
            else
            {
                UltimoNodo.LigaSiguiente = nuevoNodo;
                nuevoNodo.LigaAnterior = UltimoNodo;
                UltimoNodo = nuevoNodo;
            }
            return "Nodo insertado al final";
        }

        // Metodo para insertar un nodo antes de una posicion en especifico
        public string InsertarAntesDePosicionX(T dato, int posicion)
        {
            NodoDobleLiga<T> nuevoNodo = new(dato);
            if (ListaVacia())
            {
                return "La lista esta vacia";
            }

            if (posicion <= 0)
            {
                return "La posicion especificada debe ser un numereo mayor a 0";
            }

            if (posicion == 1)
            {
                return InsertarAlInicio(dato);
            }

            else
            {
                NodoDobleLiga<T>? nodoActual = PrimerNodo;
                NodoDobleLiga<T>? nodoAnterior = null;
                int contador = 2;
                while (nodoActual != null && contador < posicion)
                {
                    nodoAnterior = nodoActual;
                    nodoActual = (NodoDobleLiga<T>)nodoActual.LigaSiguiente;
                    contador++;
                }
                if (nodoActual == null)
                {
                    return "La posicion especificada esta fuera de rango";
                }
                else
                {
                    nuevoNodo.LigaSiguiente = nodoActual;
                    nodoAnterior.LigaSiguiente = nuevoNodo;
                    nodoActual.LigaAnterior = nuevoNodo;
                    nuevoNodo.LigaAnterior = nodoAnterior;
                    return $"Nodo insertado antes de la posicion: {posicion}";
                }
            }
        }

        // Metodo para insertar un nodo despues de una posicion en especifico
        public string InsertarDespuesDePosicionX(T dato, int posicion)
        {
            NodoDobleLiga<T> nuevoNodo = new(dato);
            if (ListaVacia())
            {
                return "La lista esta vacia";
            }

            if (posicion <= 0)
            {
                return "La posicion especificada debe ser un numereo mayor a 0";
            }

            NodoDobleLiga<T>? nodoActual = PrimerNodo;
            int contador = 1;
            while (nodoActual != null && contador < posicion)
            {
                nodoActual = (NodoDobleLiga<T>)nodoActual.LigaSiguiente;
                contador++;
            }

            if (nodoActual == null)
            {
                return "La posicion especificada esta fuera de rango";
            }
            else
            {
                nuevoNodo.LigaSiguiente = nodoActual.LigaSiguiente;
                nodoActual.LigaSiguiente = nuevoNodo;
                nuevoNodo.LigaAnterior = nodoActual;
                return $"Nodo insertado despues de la posicion: {posicion}";
            }
        }

        // Metodo para insertar un nodo en una posicion en especifico
        public string InsertarEnPosicionX(T dato, int posicion)
        {
            NodoDobleLiga<T> nuevoNodo = new(dato);
            if (ListaVacia())
            {
                return "La lista esta vacia";
            }

            if (posicion <= 0)
            {
                return "La posicion especificada debe ser un numero mayor a 0";
            }

            if (posicion == 1)
            {
                return InsertarAlInicio(dato);
            }

            NodoDobleLiga<T>? nodoActual = PrimerNodo;
            NodoDobleLiga<T>? nodoAnterior = null;
            int contador = 1;
            while (nodoActual != null && contador < posicion)
            {
                nodoAnterior = nodoActual;
                nodoActual = (NodoDobleLiga<T>)nodoActual.LigaSiguiente;
                contador++;
            }

            if (nodoActual == null)
            {
                return "La posicion especificada esta fuera de rango";
            }
            else
            {
                nuevoNodo.LigaSiguiente = nodoActual;
                nodoAnterior.LigaSiguiente = nuevoNodo;
                nodoActual.LigaAnterior = nuevoNodo;
                nuevoNodo.LigaAnterior = nodoAnterior;
                return $"Nodo insertado en la posicion: {posicion}";
            }
        }

        // Metodo para Insertar un nodo antes de un dato en especifico
        public string InsertarAntesDeDatoX(T dato, T datoX)
        {
            NodoDobleLiga<T> nuevoNodo = new(dato);
            if (ListaVacia())
            {
                return "La lista esta vacia";
            }

            NodoDobleLiga<T>? nodoActual = PrimerNodo;
            NodoDobleLiga<T>? nodoAnterior = null;
            while (nodoActual != null && !nodoActual.Dato.Equals(datoX))
            {
                nodoAnterior = nodoActual;
                nodoActual = (NodoDobleLiga<T>)nodoActual.LigaSiguiente;
            }

            if (nodoActual == null)
            {
                return "El dato especificado no se encuentra en la lista";
            }
            else
            {
                if (nodoActual == PrimerNodo)
                {
                    return InsertarAlInicio(dato);
                }
                else
                {
                    nuevoNodo.LigaSiguiente = nodoActual;
                    nodoAnterior.LigaSiguiente = nuevoNodo;
                    nodoActual.LigaAnterior = nuevoNodo;
                    nuevoNodo.LigaAnterior = nodoAnterior;
                    return $"Nodo insertado antes del dato: {datoX}";
                }
            }
        }

        // Metodo para Insertar un nodo despues de un dato en especifico
        public string InsertarDespuesDeDatoX(T dato, T datoX)
        {
            NodoDobleLiga<T> nuevoNodo = new(dato);
            if (ListaVacia())
            {
                return "La lista esta vacia";
            }

            NodoDobleLiga<T>? nodoActual = PrimerNodo;
            while (nodoActual != null && !nodoActual.Dato.Equals(datoX))
            {
                nodoActual = (NodoDobleLiga<T>)nodoActual.LigaSiguiente;
            }

            if (nodoActual == null)
            {
                return "El dato especificado no se encuentra en la lista";
            }
            else
            {
                nuevoNodo.LigaSiguiente = nodoActual.LigaSiguiente;
                nodoActual.LigaSiguiente = nuevoNodo;
                nuevoNodo.LigaAnterior = nodoActual;
                return $"Nodo insertado despues del dato: {datoX}";
            }
        }

        // Metodo para eliminar un nodo al inicio de la lista
        public string EliminarAlInicio()
        {
            if (ListaVacia())
            {
                return "La lista esta vacia";
            }

            if (PrimerNodo == UltimoNodo)
            {
                PrimerNodo = null;
                UltimoNodo = null;
            }
            else
            {
                NodoDobleLiga<T>? nodoEliminar = PrimerNodo;
                PrimerNodo = (NodoDobleLiga<T>)PrimerNodo.LigaSiguiente;
                PrimerNodo.LigaAnterior = null;
                nodoEliminar = null;
            }
            return "¡Se ha eliminado el nodo al inicio!";
        }

        // Metodo para eliminar un nodo al final de la lista
        public string EliminarAlFinal()
        {
            if (ListaVacia())
            {
                return "La lista esta vacia";
            }

            if (PrimerNodo == UltimoNodo)
            {
                PrimerNodo = null;
                UltimoNodo = null;
            }
            else
            {
                NodoDobleLiga<T>? nodoEliminar = UltimoNodo;
                UltimoNodo = (NodoDobleLiga<T>)UltimoNodo.LigaAnterior;
                UltimoNodo.LigaSiguiente = null;
                nodoEliminar = null;
            }
            return "¡Se ha eliminado el nodo al final!";
        }

        // Metodo para eliminar un nodo en una posicion en especifico
        public string EliminarEnPosicionX(int posicion)
        {
            if (ListaVacia())
            {
                return "La lista esta vacia";
            }

            if (posicion <= 0)
            {
                return "La posicion especificada debe ser un numero mayor a 0";
            }

            if (posicion == 1)
            {
                return EliminarAlInicio();
            }

            NodoDobleLiga<T>? nodoActual = PrimerNodo;
            int contador = 1;
            while (nodoActual != null && contador < posicion)
            {
                nodoActual = (NodoDobleLiga<T>)nodoActual.LigaSiguiente;
                contador++;
            }

            if (nodoActual == null)
            {
                return "La posicion especificada esta fuera de rango";
            }

            if (nodoActual == UltimoNodo)
            {
                return EliminarAlFinal();
            }

            NodoDobleLiga<T>? nodoAnterior = (NodoDobleLiga<T>)nodoActual.LigaAnterior;
            NodoDobleLiga<T>? nodoSiguiente = (NodoDobleLiga<T>)nodoActual.LigaSiguiente;
            nodoAnterior.LigaSiguiente = nodoSiguiente;
            nodoSiguiente.LigaAnterior = nodoAnterior;
            nodoActual = null;

            return $"¡Se ha eliminado el nodo en la posicion: {posicion}!";
        }

        // Metodo para eliminar un nodo antes de una posicion en especifico
        public string EliminarAntesDePosicionX(int posicion)
        {
            if (ListaVacia())
            {
                return "La lista esta vacia";
            }

            if (posicion <= 0)
            {
                return "La posicion especificada debe ser un numero mayor a 0";
            }

            if (posicion == 1)
            {
                return "No hay un nodo antes de la posicion 1";
            }

            NodoDobleLiga<T>? nodoActual = PrimerNodo;
            NodoDobleLiga<T>? nodoAnterior = null;
            NodoDobleLiga<T>? nodoAnteriorAnterior = null;
            int contador = 1;
            while (nodoActual != null && contador < posicion)
            {
                nodoAnteriorAnterior = nodoAnterior;
                nodoAnterior = nodoActual;
                nodoActual = (NodoDobleLiga<T>)nodoActual.LigaSiguiente;
                contador++;
            }

            if (nodoActual == null)
            {
                return "La posicion especificada esta fuera de rango";
            }

            if (nodoAnteriorAnterior == null)
            {
                return "No hay un nodo antes de la posicion especificada";
            }

            NodoDobleLiga<T>? nodoSiguiente = (NodoDobleLiga<T>)nodoAnterior.LigaSiguiente;
            nodoAnteriorAnterior.LigaSiguiente = nodoSiguiente;
            nodoSiguiente.LigaAnterior = nodoAnteriorAnterior;
            nodoAnterior = null;

            return $"¡Se ha eliminado el nodo antes de la posicion: {posicion}!";
        }   

        // Metodo para eliminar un nodo despues de una posicion en especifico
        public string EliminarDespuesDePosicionX(int posicion)
        {
            if (ListaVacia())
            {
                return "La lista esta vacia";
            }

            if (posicion <= 0)
            {
                return "La posicion especificada debe ser un numero mayor a 0";
            }

            NodoDobleLiga<T>? nodoActual = PrimerNodo;
            int contador = 1;
            while (nodoActual != null && contador < posicion)
            {
                nodoActual = (NodoDobleLiga<T>)nodoActual.LigaSiguiente;
                contador++;
            }

            if (nodoActual == null)
            {
                return "La posicion especificada esta fuera de rango";
            }

            if (nodoActual == UltimoNodo)
            {
                return "No hay un nodo despues de la posicion especificada";
            }

            NodoDobleLiga<T>? nodoSiguiente = (NodoDobleLiga<T>)nodoActual.LigaSiguiente;
            NodoDobleLiga<T>? nodoSiguienteSiguiente = (NodoDobleLiga<T>)nodoSiguiente.LigaSiguiente;
            nodoActual.LigaSiguiente = nodoSiguienteSiguiente;
            nodoSiguienteSiguiente.LigaAnterior = nodoActual;
            nodoSiguiente = null;

            return $"¡Se ha eliminado el nodo despues de la posicion: {posicion}!";
        }

        // Metodo para eliminar un nodo antes de un dato en especifico
        public string EliminarAntesDeDatoX(T datoX)
        {
            if (ListaVacia())
            {
                return "La lista esta vacia";
            }

            NodoDobleLiga<T>? nodoActual = PrimerNodo;
            NodoDobleLiga<T>? nodoAnterior = null;
            NodoDobleLiga<T>? nodoAnteriorAnterior = null;
            while (nodoActual != null && !nodoActual.Dato.Equals(datoX))
            {
                nodoAnteriorAnterior = nodoAnterior;
                nodoAnterior = nodoActual;
                nodoActual = (NodoDobleLiga<T>)nodoActual.LigaSiguiente;
            }

            if (nodoActual == null)
            {
                return "El dato especificado no se encuentra en la lista";
            }

            if (nodoAnteriorAnterior == null)
            {
                return "No hay un nodo antes del dato especificado";
            }

            NodoDobleLiga<T>? nodoSiguiente = (NodoDobleLiga<T>)nodoAnterior.LigaSiguiente;
            nodoAnteriorAnterior.LigaSiguiente = nodoSiguiente;
            nodoSiguiente.LigaAnterior = nodoAnteriorAnterior;
            nodoAnterior = null;

            return $"¡Se ha eliminado el nodo antes del dato: {datoX}!";
        }

        // Metodo para eliminar un nodo despues de un dato en especifico
        public string EliminarDespuesDeDatoX(T datoX)
        {
            if (ListaVacia())
            {
                return "La lista esta vacia";
            }

            NodoDobleLiga<T>? nodoActual = PrimerNodo;
            while (nodoActual != null && !nodoActual.Dato.Equals(datoX))
            {
                nodoActual = (NodoDobleLiga<T>)nodoActual.LigaSiguiente;
            }

            if (nodoActual == null)
            {
                return "El dato especificado no se encuentra en la lista";
            }

            if (nodoActual == UltimoNodo)
            {
                return "No hay un nodo despues del dato especificado";
            }

            NodoDobleLiga<T>? nodoSiguiente = (NodoDobleLiga<T>)nodoActual.LigaSiguiente;
            NodoDobleLiga<T>? nodoSiguienteSiguiente = (NodoDobleLiga<T>)nodoSiguiente.LigaSiguiente;
            nodoActual.LigaSiguiente = nodoSiguienteSiguiente;
            nodoSiguienteSiguiente.LigaAnterior = nodoActual;
            nodoSiguiente = null;

            return $"¡Se ha eliminado el nodo despues del dato: {datoX}!";
        }

        // Metodo para eliminar un dato en especifico
        public string EliminarDatoX(T datoX)
        {
            if (ListaVacia())
            {
                return "La lista esta vacia";
            }

            NodoDobleLiga<T>? nodoActual = PrimerNodo;
            NodoDobleLiga<T>? nodoAnterior = null;
            while (nodoActual != null && !nodoActual.Dato.Equals(datoX))
            {
                nodoAnterior = nodoActual;
                nodoActual = (NodoDobleLiga<T>)nodoActual.LigaSiguiente;
            }

            if (nodoActual == null)
            {
                return "El dato especificado no se encuentra en la lista";
            }

            if (nodoAnterior == null)
            {
                return EliminarAlInicio();
            }

            if (nodoActual == UltimoNodo)
            {
                return EliminarAlFinal();
            }

            NodoDobleLiga<T>? nodoSiguiente = (NodoDobleLiga<T>)nodoActual.LigaSiguiente;
            nodoAnterior.LigaSiguiente = nodoSiguiente;
            nodoSiguiente.LigaAnterior = nodoAnterior;
            nodoActual = null;

            return $"¡Se ha eliminado el dato: {datoX}!";
        }

        // Metodo para buscar un dato en la lista
        public string BuscarDato(T dato)
        {
            if (ListaVacia())
            {
                return "La lista esta vacia";
            }

            NodoDobleLiga<T>? nodoActual = PrimerNodo;
            while (nodoActual != null && !nodoActual.Dato.Equals(dato))
            {
                nodoActual = (NodoDobleLiga<T>)nodoActual.LigaSiguiente;
            }

            if (nodoActual == null)
            {
                return "El dato no se encuentra en la lista";
            }
            else
            {
                return "El dato se encuentra en la lista";
            }
        }

        // Metodo para ordenar la lista de forma ascendente o alfabeticamente dependiente el tipo que vaya a ser
        public string OrdenarListaAscendente()
        {
            if (ListaVacia())
            {
                return "La lista esta vacia";
            }

            NodoDobleLiga<T>? nodoActual = PrimerNodo;
            NodoDobleLiga<T>? nodoSiguiente = null;
            T datoTemporal;

            while (nodoActual != null)
            {
                nodoSiguiente = (NodoDobleLiga<T>)nodoActual.LigaSiguiente;
                while (nodoSiguiente != null)
                {
                    if (Comparer.Default.Compare(nodoActual.Dato, nodoSiguiente.Dato) > 0)
                    {
                        datoTemporal = nodoActual.Dato;
                        nodoActual.Dato = nodoSiguiente.Dato;
                        nodoSiguiente.Dato = datoTemporal;
                    }
                    nodoSiguiente = (NodoDobleLiga<T>)nodoSiguiente.LigaSiguiente;
                }
                nodoActual = (NodoDobleLiga<T>)nodoActual.LigaSiguiente;
            }

            return "¡La lista ha sido ordenada de forma ascendente!";
        }

        // Metodo para recorrar la lista con IEnumerator
        public IEnumerator<T> GetEnumerator()
        {
            NodoDobleLiga<T>? nodoActual = PrimerNodo;
            while (nodoActual != null)
            {
                yield return nodoActual.Dato;
                nodoActual = (NodoDobleLiga<T>)nodoActual.LigaSiguiente;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }

    public class ListaCuadruplementeEnlazada<T> : IEnumerable<T>
    {
        public NodoCuadrupleLiga<T>? PrimerNodo { get; set; }
        public NodoCuadrupleLiga<T>? UltimoNodo { get; set; }

        public ListaCuadruplementeEnlazada()
        {
            PrimerNodo = null;
            UltimoNodo = null;
        }

        public bool ListaVacia()
        {
            return PrimerNodo == null;
        }

        // Metodo para insertar un nodo al inicio de la lista 

    }


}



