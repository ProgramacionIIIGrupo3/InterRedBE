namespace InterRedBE.UTILS.Models
{
    public class NodoCuadrupleLiga <T>(T dato) : NodoDobleLiga<T>(dato)
    {
        public NodoCuadrupleLiga<T>? LigaArriba { get; set; } = null;
        public NodoCuadrupleLiga<T>? LigaAbajo { get; set; } = null;
    }
}
