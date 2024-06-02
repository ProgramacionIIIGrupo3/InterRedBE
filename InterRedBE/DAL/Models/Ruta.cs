namespace InterRedBE.DAL.Models
{
    public class Ruta
    {
        public int Id { get; set; }
        public int IdEntidadInicio { get; set; }
        public TipoEntidad TipoInicio { get; set; }
        public int IdEntidadFin { get; set; }
        public TipoEntidad TipoFin { get; set; }
        public double Distancia { get; set; }
        public string Direccion { get; set; }

        public virtual Departamento DepartamentoInicio { get; set; }
        public virtual Departamento DepartamentoFin { get; set; }
        public virtual Municipio MunicipioInicio { get; set; }
        public virtual Municipio MunicipioFin { get; set; }
    }

    public enum TipoEntidad
    {
        Departamento,
        Municipio
    }
}
