namespace InterRedBE.UTILS.Models
{
    public class NodoDobleLiga<T>(T dato) : Nodo<T>(dato)
    {
        public NodoDobleLiga<T>? LigaAnterior { get; set; } = null;
    }
}
