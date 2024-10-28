using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Commerce.Muebles.Entidades
{
    public class Autenticacion
    {
        public int IdAtenticacion { get; set; }
        public string Email { get; set; }
        public String Clave { get; set; }
        public int UsuarioId { get; set; }

    }
}
