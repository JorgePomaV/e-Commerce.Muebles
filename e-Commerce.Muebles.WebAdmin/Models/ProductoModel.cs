using e_Commerce.Muebles.Entidades;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace e_Commerce.Muebles.WebAdmin.Models
{
    public class ProductoModel
    {
        public int id_producto { get; set; }

        [Required(ErrorMessage = "El nombre del producto es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder de 100 caracteres")]
        public string nombre { get; set; }

        [StringLength(250, ErrorMessage = "La descripción no puede exceder de 250 caracteres")]
        public string descripcion { get; set; }

        [Range(0.01, 1000000.00, ErrorMessage = "El precio debe estar entre 0.01 y 1000000.00")]
        public double precio { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo")]
        public int stock { get; set; }

        [Required(ErrorMessage = "La categoría es obligatoria")]
        public int categoria_id { get; set; }

        public List<SelectListItem>? ListaCategoriasItem { get; set; }
    }
}
