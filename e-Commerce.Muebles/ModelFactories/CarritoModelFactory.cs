using e_Commerce.Muebles.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Commerce.Muebles.ModelFactories
{
    public class CarritoCompleto
    {
        public int id_carrito { get; set; }
        public int cantidad { get; set; }
        public int producto_id { get; set; }
        public int cliente_id { get; set; }
        public  Producto? producto {  get; set; }
    }
}
