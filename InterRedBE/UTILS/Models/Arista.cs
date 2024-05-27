namespace InterRedBE.UTILS.Models
{
    public class Arista<T>
    {
        public NodoGrafo<T> Nodo { get; set; }
        public double Distancia { get; set; }

        public Arista(NodoGrafo<T> nodo, double distancia)
        {
            Nodo = nodo;
            Distancia = distancia;
        }
    }
}
