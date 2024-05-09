namespace InterRedBE.DAL.DTO
{
    public class DepartamentoRutaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdDepartamentoInicio { get; set; } 
        public int IdDepartamentoFin { get; set; }
    }
}
