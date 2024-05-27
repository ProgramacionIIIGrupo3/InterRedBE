using InterRedBE.UTILS.Services;

namespace InterRedBE.UTILS.Models
{
    public class NodoGrafo<T>
    {
        public int Id { get; }
        public T Dato { get; }
        public ListaEnlazadaDoble<Arista<T>> Adyacentes { get; }

        public NodoGrafo(int id, T dato)
        {
            Id = id;
            Dato = dato;
            Adyacentes = new ListaEnlazadaDoble<Arista<T>>();
        }
    }
}
