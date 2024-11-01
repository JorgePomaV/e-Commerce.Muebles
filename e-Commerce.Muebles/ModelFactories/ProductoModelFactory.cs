using e_Commerce.Muebles.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Commerce.Muebles.ModelFactories
{
    public class ProductoCompleto
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public double Precio { get; set; }
        public int Stock { get; set; }

        // Relación con categoría
        public Categoria Categoria { get; set; }

    }
}
