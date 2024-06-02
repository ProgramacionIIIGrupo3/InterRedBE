namespace InterRedBE.UTILS.Models
{
public class Arista<T>
{
    public NodoGrafo<T> Nodo { get; set; }
    public double Distancia { get; set; }
    public double Penalizacion { get; set; }

    public Arista(NodoGrafo<T> nodo, double distancia)
    {
        Nodo = nodo;
        Distancia = distancia;
        Penalizacion = 0; // Inicialmente sin penalización
    }

    public double DistanciaConPenalizacion => Distancia + Penalizacion;
}

}
