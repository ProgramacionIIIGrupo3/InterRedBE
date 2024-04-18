namespace InterRedBE.DAL.DTO
{
    public class MunicipioDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Poblacion { get; set; }
        public int? IdDepartamento { get; set; }
    }
}
