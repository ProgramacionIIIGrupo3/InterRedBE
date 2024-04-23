using System.Text.Json.Serialization;

namespace InterRedBE.DAL.Models
{
    public class Visita
    {
        public int Id { get; set; }
        public int? IdLugarTuristico { get; set; }

        [JsonIgnore]
        public virtual LugarTuristico LugarTuristico { get; set; }

    }
}
