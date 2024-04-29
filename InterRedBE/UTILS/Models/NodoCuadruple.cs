using InterRedBE.DAL.Models;

namespace InterRedBE.UTILS.Models
{
    public class NodoCuadruple<T>
    {
        public T Dato { get; set; }
        public NodoCuadruple<T> Norte { get; set; }
        public NodoCuadruple<T> Sur { get; set; }
        public NodoCuadruple<T> Este { get; set; }
        public NodoCuadruple<T> Oeste { get; set; }
        public int Id { get; set; } // Un identificador único para cada nodo.

        public NodoCuadruple(int id, T dato)
        {
            Id = id;
            Dato = dato;
            Norte = null;
            Sur = null;
            Este = null;
            Oeste = null;
        }
    }
}
