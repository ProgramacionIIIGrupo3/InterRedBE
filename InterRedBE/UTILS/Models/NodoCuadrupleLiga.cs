namespace InterRedBE.UTILS.Models
{
    public class NodoCuadrupleLiga <T>(T dato) : NodoDobleLiga<T>(dato)
    {
        public NodoCuadrupleLiga<T>? LigaSuperior { get; set; } = null;
        public NodoCuadrupleLiga<T>? LigaInferior { get; set; } = null;
    }
}
