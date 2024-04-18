namespace InterRedBE.DAL.DTO
{
    public class DepartamentoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string? Imagen { get; set; }
        public int Poblacion { get; set; }
        public int? IdCabecera { get; set; }
    }
}
