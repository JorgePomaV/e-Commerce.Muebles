using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Commerce.Muebles.Entidades
{
    public class Direccion
    {
        public int id_direccion { get; set; }
        public string calle { get; set; }
        public int altura { get; set; }
        public string? departamento { get; set; }
        public string barrio { get; set; }
        public string localidad { get; set; }
    }
}
