using InterRedBE.UTILS.Models;
using System.Collections;

namespace InterRedBE.UTILS.Services
{
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

        // Metodo para insertar un nodo al inicio 
        public string InsertarAlInicio(T dato)
        {
            NodoCuadrupleLiga<T> nuevoNodo = new(dato);
            if (ListaVacia())
            {
                PrimerNodo = nuevoNodo;
                UltimoNodo = nuevoNodo;
            }
            else
            {
                nuevoNodo.LigaSiguiente = PrimerNodo;
                PrimerNodo.LigaAnterior = nuevoNodo;
                PrimerNodo.LigaSuperior = nuevoNodo;
                nuevoNodo.LigaInferior = PrimerNodo;
                PrimerNodo = nuevoNodo;
            }
            return "Nodo insertado al inicio";
        }

        // Metodo para insertar un nodo al final
        public string InsertarAlFinal(T dato)
        {
            NodoCuadrupleLiga<T> nuevoNodo = new(dato);
            if (ListaVacia())
            {
                PrimerNodo = nuevoNodo;
                UltimoNodo = nuevoNodo;
            }
            else
            {
                UltimoNodo.LigaSiguiente = nuevoNodo;
                nuevoNodo.LigaAnterior = UltimoNodo;
                UltimoNodo.LigaSuperior = nuevoNodo;
                nuevoNodo.LigaInferior = UltimoNodo;
                UltimoNodo = nuevoNodo;
            }
            return "Nodo insertado al final";
        }

        // Metodo para insertar un nodo arriba de un dato en especifico

        // Método para insertar un nodo arriba de un nodo específico (versión recursiva)
        public string InsertarArriba(T datoReferencia, T datoNuevo)
        {
            NodoCuadrupleLiga<T> nuevoNodo = new(datoNuevo);

            string mensaje = InsertarArribaRecursivo(PrimerNodo, datoReferencia, nuevoNodo);

            if (mensaje == "Encontrado")
            {
                return "Nodo insertado arriba";
            }
            else
            {
                return "El nodo de referencia no se encuentra en la lista";
            }
        }

        // Método auxiliar recursivo para buscar el nodo de referencia e insertar el nuevo nodo
        private string InsertarArribaRecursivo(NodoCuadrupleLiga<T>? nodoActual, T datoReferencia, NodoCuadrupleLiga<T> nuevoNodo)
        {
            if (nodoActual == null)
                return "No encontrado";

            if (nodoActual.Dato.Equals(datoReferencia))
            {
                nuevoNodo.LigaInferior = nodoActual;
                nuevoNodo.LigaSuperior = nodoActual.LigaSuperior;

                if (nodoActual.LigaSuperior != null)
                {
                    nodoActual.LigaSuperior.LigaInferior = nuevoNodo;
                }

                nodoActual.LigaSuperior = nuevoNodo;

                return "Encontrado";
            }

            // Buscar hacia arriba
            string mensajeArriba = InsertarArribaRecursivo((NodoCuadrupleLiga<T>)nodoActual.LigaSuperior, datoReferencia, nuevoNodo);
            if (mensajeArriba == "Encontrado")
                return mensajeArriba;

            // Buscar hacia abajo
            string mensajeAbajo = InsertarArribaRecursivo((NodoCuadrupleLiga<T>)nodoActual.LigaInferior, datoReferencia, nuevoNodo);
            if (mensajeAbajo == "Encontrado")
                return mensajeAbajo;

            // Buscar hacia la izquierda
            string mensajeIzquierda = InsertarArribaRecursivo((NodoCuadrupleLiga<T>)nodoActual.LigaAnterior, datoReferencia, nuevoNodo);
            if (mensajeIzquierda == "Encontrado")
                return mensajeIzquierda;

            // Buscar hacia la derecha
            string mensajeDerecha = InsertarArribaRecursivo((NodoCuadrupleLiga<T>)nodoActual.LigaSiguiente, datoReferencia, nuevoNodo);
            if (mensajeDerecha == "Encontrado")
                return mensajeDerecha;

            return "No encontrado";
        }

        // Metodo para insertar un nodo abajo de un dato en especifico

        // Método para insertar un nodo abajo de un nodo específico (versión recursiva)
        public string InsertarAbajo(T datoReferencia, T datoNuevo)
        {
            NodoCuadrupleLiga<T> nuevoNodo = new(datoNuevo);

            string mensaje = InsertarAbajoRecursivo(PrimerNodo, datoReferencia, nuevoNodo);

            if (mensaje == "Encontrado")
            {
                return "Nodo insertado abajo";
            }
            else
            {
                return "El nodo de referencia no se encuentra en la lista";
            }
        }

        // Método auxiliar recursivo para buscar el nodo de referencia e insertar el nuevo nodo
        private string InsertarAbajoRecursivo(NodoCuadrupleLiga<T>? nodoActual, T datoReferencia, NodoCuadrupleLiga<T> nuevoNodo)
        {
            if (nodoActual == null)
                return "No encontrado";

            if (nodoActual.Dato.Equals(datoReferencia))
            {
                nuevoNodo.LigaSuperior = nodoActual;
                nuevoNodo.LigaInferior = nodoActual.LigaInferior;

                if (nodoActual.LigaInferior != null)
                {
                    nodoActual.LigaInferior.LigaSuperior = nuevoNodo;
                }

                nodoActual.LigaInferior = nuevoNodo;

                return "Encontrado";
            }

            // Buscar hacia arriba
            string mensajeArriba = InsertarAbajoRecursivo((NodoCuadrupleLiga<T>)nodoActual.LigaSuperior, datoReferencia, nuevoNodo);
            if (mensajeArriba == "Encontrado")
                return mensajeArriba;

            // Buscar hacia abajo
            string mensajeAbajo = InsertarAbajoRecursivo((NodoCuadrupleLiga<T>)nodoActual.LigaInferior, datoReferencia, nuevoNodo);
            if (mensajeAbajo == "Encontrado")
                return mensajeAbajo;

            // Buscar hacia la izquierda
            string mensajeIzquierda = InsertarAbajoRecursivo((NodoCuadrupleLiga<T>)nodoActual.LigaAnterior, datoReferencia, nuevoNodo);
            if (mensajeIzquierda == "Encontrado")
                return mensajeIzquierda;

            // Buscar hacia la derecha
            string mensajeDerecha = InsertarAbajoRecursivo((NodoCuadrupleLiga<T>)nodoActual.LigaSiguiente, datoReferencia, nuevoNodo);
            if (mensajeDerecha == "Encontrado")
                return mensajeDerecha;

            return "No encontrado";
        }

        // Metodo para insertar un nodo antes de un dato en especifico

        // Método para insertar un nodo antes de un nodo específico (versión recursiva)
        public string InsertarAntes(T datoReferencia, T datoNuevo)
        {
            NodoCuadrupleLiga<T> nuevoNodo = new(datoNuevo);

            string mensaje = InsertarAntesRecursivo(PrimerNodo, datoReferencia, nuevoNodo);

            if (mensaje == "Encontrado")
            {
                return "Nodo insertado antes";
            }
            else
            {
                return "El nodo de referencia no se encuentra en la lista";
            }
        }

        // Método auxiliar recursivo para buscar el nodo de referencia e insertar el nuevo nodo
        private string InsertarAntesRecursivo(NodoCuadrupleLiga<T>? nodoActual, T datoReferencia, NodoCuadrupleLiga<T> nuevoNodo)
        {
            if (nodoActual == null)
                return "No encontrado";

            if (nodoActual.Dato.Equals(datoReferencia))
            {
                nuevoNodo.LigaSiguiente = nodoActual;
                nuevoNodo.LigaAnterior = nodoActual.LigaAnterior;

                if (nodoActual.LigaAnterior != null)
                {
                    nodoActual.LigaAnterior.LigaSiguiente = nuevoNodo;
                }

                nodoActual.LigaAnterior = nuevoNodo;

                return "Encontrado";
            }

            // Buscar hacia arriba
            string mensajeArriba = InsertarAntesRecursivo((NodoCuadrupleLiga<T>)nodoActual.LigaSuperior, datoReferencia, nuevoNodo);
            if (mensajeArriba == "Encontrado")
                return mensajeArriba;

            // Buscar hacia abajo
            string mensajeAbajo = InsertarAntesRecursivo((NodoCuadrupleLiga<T>)nodoActual.LigaInferior, datoReferencia, nuevoNodo);
            if (mensajeAbajo == "Encontrado")
                return mensajeAbajo;

            // Buscar hacia la izquierda
            string mensajeIzquierda = InsertarAntesRecursivo((NodoCuadrupleLiga<T>)nodoActual.LigaAnterior, datoReferencia, nuevoNodo);
            if (mensajeIzquierda == "Encontrado")
                return mensajeIzquierda;

            // Buscar hacia la derecha
            string mensajeDerecha = InsertarAntesRecursivo((NodoCuadrupleLiga<T>)nodoActual.LigaSiguiente, datoReferencia, nuevoNodo);
            if (mensajeDerecha == "Encontrado")
                return mensajeDerecha;

            return "No encontrado";
        }

        // Metodo para insertar un nodo despues de un dato en especifico

        // Método para insertar un nodo después de un nodo específico (versión recursiva)
        //public string InsertarDespues(T datoReferencia, T datoNuevo)
        //{
        //    NodoCuadrupleLiga<T> nuevoNodo = new(datoNuevo);

        //    string mensaje = InsertarDespuesRecursivo(PrimerNodo, datoReferencia, nuevoNodo);

        //    if (mensaje == "Encontrado")
        //    {
        //        return "Nodo insertado después";
        //    }
        //    else
        //    {
        //        return "El nodo de referencia no se encuentra en la lista";
        //    }
        //}

        //// Método auxiliar recursivo para buscar el nodo de referencia e insertar el nuevo nodo
        //private string InsertarDespuesRecursivo(NodoCuadrupleLiga<T>? nodoActual, T datoReferencia, NodoCuadrupleLiga<T> nuevoNodo)
        //{
        //    if (nodoActual == null)
        //        return "No encontrado";

        //    if (nodoActual.Dato.Equals(datoReferencia))
        //    {
        //        nuevoNodo.LigaAnterior = nodoActual;
        //        nuevoNodo.LigaSiguiente = nodoActual.LigaSiguiente;

        //        if (nodoActual.LigaSiguiente != null)
        //        {
        //            (NodoCuadrupleLiga<T>)nodoActual.LigaSiguiente.LigaAnterior = nuevoNodo;
        //        }

        //        nodoActual.LigaSiguiente = nuevoNodo;

        //        return "Encontrado";
        //    }

        //    // Buscar hacia arriba
        //    string mensajeArriba = InsertarDespuesRecursivo((NodoCuadrupleLiga<T>)nodoActual.LigaSuperior, datoReferencia, nuevoNodo);
        //    if (mensajeArriba == "Encontrado")
        //        return mensajeArriba;

        //    // Buscar hacia abajo
        //    string mensajeAbajo = InsertarDespuesRecursivo((NodoCuadrupleLiga<T>)nodoActual.LigaInferior, datoReferencia, nuevoNodo);
        //    if (mensajeAbajo == "Encontrado")
        //        return mensajeAbajo;

        //    // Buscar hacia la izquierda
        //    string mensajeIzquierda = InsertarDespuesRecursivo((NodoCuadrupleLiga<T>)nodoActual.LigaAnterior, datoReferencia, nuevoNodo);
        //    if (mensajeIzquierda == "Encontrado")
        //        return mensajeIzquierda;

        //    // Buscar hacia la derecha
        //    string mensajeDerecha = InsertarDespuesRecursivo((NodoCuadrupleLiga<T>)nodoActual.LigaSiguiente, datoReferencia, nuevoNodo);
        //    if (mensajeDerecha == "Encontrado")
        //        return mensajeDerecha;

        //    return "No encontrado";
        //}



        // Metodo para recorrer la lista con IEnumerable

        public IEnumerator<T> GetEnumerator()
        {
            NodoCuadrupleLiga<T>? nodoActual = PrimerNodo;
            while (nodoActual != null)
            {
                yield return nodoActual.Dato;
                nodoActual = (NodoCuadrupleLiga<T>)nodoActual.LigaSiguiente;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
