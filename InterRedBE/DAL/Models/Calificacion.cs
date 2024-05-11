using System.Text.Json.Serialization;

namespace InterRedBE.DAL.Models
{
    public class Calificacion
    {
        public int Id { get; set; }
        public int? LugarTuristicoId { get; set; } // Cambiado de IdLugarTuristico
        public string Puntuacion { get; set; }
        public string Comentario { get; set; }

        public virtual LugarTuristico LugarTuristico { get; set; }
    }

}
