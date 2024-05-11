namespace InterRedBE.DAL.Models
{
    public class Ruta
    {
        public int Id { get; set; }
        public int IdDepartamentoInicio { get; set; }
        public int IdDepartamentoFin { get; set; }
        public double Distancia { get; set; }
        public string Direccion { get; set; }
        public virtual Departamento DepartamentoInicio { get; set; }
        public virtual Departamento DepartamentoFin { get; set; }
    }
}