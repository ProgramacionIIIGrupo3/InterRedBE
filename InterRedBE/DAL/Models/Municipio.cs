namespace InterRedBE.DAL.Models
{
    public class Municipio
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Poblacion { get; set; }
        public int? IdDepartamento { get; set; }
        public virtual Departamento Departamento { get; set; }
        public virtual ICollection<LugarTuristico>? LugaresTuristicos { get; set; }
    }
}
