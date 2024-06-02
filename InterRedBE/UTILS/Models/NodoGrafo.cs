using InterRedBE.UTILS.Services;

namespace InterRedBE.UTILS.Models
{
    public class NodoGrafo<T>
    {
        public string IdX { get; }
        public T Dato { get; }
        public ListaEnlazadaDoble<Arista<T>> Adyacentes { get; private set; }

        public NodoGrafo(string idX, T dato)
        {
            IdX = idX;
            Dato = dato;
            Adyacentes = new ListaEnlazadaDoble<Arista<T>>();
        }

        public void SetAdyacentes(ListaEnlazadaDoble<Arista<T>> nuevaLista)
        {
            Adyacentes = nuevaLista;
        }
    }



}
