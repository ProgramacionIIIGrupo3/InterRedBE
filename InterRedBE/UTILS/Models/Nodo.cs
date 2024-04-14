namespace InterRedBE.UTILS.Models
{

    public class Nodo<T>(T dato)
    {
        public T Dato { get; set; } = dato;
        public Nodo<T>? LigaSiguiente { get; set; } = null;
    }
}
