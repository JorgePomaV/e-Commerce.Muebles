using e_Commerce.Muebles.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace e_Commerce.Muebles.Repos
{
    public interface IVentaRepositori
    {
        bool RegistrarVenta(double monto, string estado, int numero_factura, int usuario_id, int direccion_id);
        IEnumerable<Venta> GetVentas();
        Venta GetVenta(int id_usuario);
    }
    internal class VentaRepository : IVentaRepositori
    {
        private string _ConnectionString;
        public VentaRepository(string connectionString)
        {
            _ConnectionString = connectionString;
        }
        public Venta GetVenta(int id_usuario)
        {
            using (IDbConnection con = new SqlConnection(_ConnectionString))
            {
                Venta venta = con.QuerySingle<Venta>("SELECT * FROM VENTA WHERE usuario_id = @Id_usuario", new { Id_usuario = id_usuario });
                return venta;
            }
        }

        //falta agregar select por estado

        public IEnumerable<Venta> GetVentas()
        {
            using (IDbConnection conn = new SqlConnection(_ConnectionString))
            {
                string query = "SELECT * FROM VENTA";
                IEnumerable<Venta> ventas = conn.Query<Venta>(query).ToList();
                return ventas;
            }
        }

        public bool RegistrarVenta(double monto, string estado, int numero_factura, int usuario_id, int direccion_id)
        {
            throw new NotImplementedException();
        }
    }
}
