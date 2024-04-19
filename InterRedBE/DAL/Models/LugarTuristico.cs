using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Text.Json.Serialization;

namespace InterRedBE.DAL.Models
{
    public class LugarTuristico
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string? Imagen { get; set; }
        public int? IdMunicipio { get; set; }
        public int? IdDepartamento { get; set; }
        public virtual Municipio Municipio { get; set; }
        public virtual Departamento Departamento { get; set; }
        [JsonIgnore]
        public virtual ICollection<Visita> Visitas { get; set; }
        public virtual ICollection<Calificacion> Calificaciones { get; set; }



    }
}
