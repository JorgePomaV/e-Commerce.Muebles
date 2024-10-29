using System.ComponentModel.DataAnnotations;

namespace e_Commerce.Muebles.WebAdmin.Models
{
    public class ProductoModel
    {
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "El nombre del producto es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder de 100 caracteres")]
        public string Nombre { get; set; }

        [StringLength(250, ErrorMessage = "La descripción no puede exceder de 250 caracteres")]
        public string Descripcion { get; set; }

        [Range(0.01, 10000.00, ErrorMessage = "El precio debe estar entre 0.01 y 10000.00")]
        public double Precio { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "El proveedor es obligatorio")]
        public int ProveedorId { get; set; }

        [Required(ErrorMessage = "La categoría es obligatoria")]
        public int CategoriaId { get; set; }
    }
}
