namespace InterRedBE.DAL.Models
{
    public class Calificacion
    {
        public int Id { get; set; }
        public int IdLugarTuristico { get; set; }
        public string Puntuacion { get; set; }
        public string Comentario { get; set; }
        public virtual LugarTuristico LugarTuristico { get; set; }
    }
}
