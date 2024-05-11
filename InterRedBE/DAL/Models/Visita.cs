using System.Text.Json.Serialization;

namespace InterRedBE.DAL.Models
{
    public class Visita
    {
        public int Id { get; set; }
        public int? LugarTuristicoId { get; set; }

        public virtual LugarTuristico LugarTuristico { get; set; }

    }
}
