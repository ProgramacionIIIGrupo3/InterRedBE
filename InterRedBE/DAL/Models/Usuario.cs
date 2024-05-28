using System.ComponentModel.DataAnnotations;

namespace InterRedBE.DAL.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public string Correo { get; set; }
        public string Contrasena { get; set; }

        [Required]
        [RegularExpression("^(Invitado|Administrador)$", ErrorMessage = "El rol debe ser 'Invitado' o 'Administrador'.")]
        public string Rol { get; set; }

    }
}

