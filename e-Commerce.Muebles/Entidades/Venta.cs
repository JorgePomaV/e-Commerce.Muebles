using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Commerce.Muebles.Entidades
{
    public class Venta
    {
        public int id_venta { get; set; }
        public DateTime fecha { get; set; }
        public double monto { get; set; }
        public string estado { get; set; }
        public string numero_factura { get; set; }
        public int usuario_id { get; set; }
        public int direccion_id { get; set; }
    }
}
