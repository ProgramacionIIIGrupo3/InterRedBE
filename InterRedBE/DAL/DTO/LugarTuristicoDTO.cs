namespace InterRedBE.DAL.DTO
{
    public class LugarTuristicoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string? Imagen { get; set; }
        public int IdMunicipio { get; set; }
        public int IdDepartamento { get; set; }
    }
}
