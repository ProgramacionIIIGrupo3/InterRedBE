using InterRedBE.UTILS.Interfaces;

namespace InterRedBE.DAL.Models
{
    public class Municipio : IIdentificable
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Poblacion { get; set; }
        public int? IdDepartamento { get; set; }
        public string? Imagen { get; set; }
        public virtual Departamento Departamento { get; set; }
        public virtual ICollection<LugarTuristico> LugaresTuristicos { get; set; }

        // Nuevas relaciones con Ruta
        public virtual ICollection<Ruta> RutasInicio { get; set; }
        public virtual ICollection<Ruta> RutasFin { get; set; }
    }
}
