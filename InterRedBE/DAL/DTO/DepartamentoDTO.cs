namespace InterRedBE.DAL.DTO
{
    public class DepartamentoDTO
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public IFormFile ImagenFile { get; set; } // Archivo de imagen
        public string? Imagen { get; set; } // Ruta de la imagen
        public int? IdCabecera { get; set; }
    }
}
