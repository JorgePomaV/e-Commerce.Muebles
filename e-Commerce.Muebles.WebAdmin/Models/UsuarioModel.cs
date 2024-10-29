using System.ComponentModel.DataAnnotations;

namespace e_Commerce.Muebles.WebAdmin.Models
{
    public class UsuarioModel
    {
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50, ErrorMessage = "El nombre no puede exceder de 50 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio")]
        [StringLength(50, ErrorMessage = "El apellido no puede exceder de 50 caracteres")]
        public string Apellido { get; set; }

        [Phone(ErrorMessage = "El número de teléfono no es válido")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El tipo de usuario es obligatorio")]
        public TipoUsuario Tipo { get; set; }

        public enum TipoUsuario
        {
            Admin = 1,
            Cliente = 2
        }
    }
}
