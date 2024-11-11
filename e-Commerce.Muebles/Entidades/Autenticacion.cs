using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Commerce.Muebles.Entidades
{
    public class Autenticacion
    {
        public int id_autenticacion { get; set; }
        public string Email { get; set; }
        public string Clave { get; set; }
        public string GoogleIdentificador { get; set; }
        public int usuario_id { get; set; }

    }
}
