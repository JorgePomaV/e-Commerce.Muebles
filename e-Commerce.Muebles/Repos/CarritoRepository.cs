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
    public interface ICarritoRepository
    {
        bool AgregarProducto(int id_producto, int id_cliente, int cantidad);
        bool EliminarProducto(int id_producto, int id_cliente);
        IEnumerable<Carrito> GetCarrito(int id_cliente);
        bool EditarCantidadProducto(int id_cliente, int id_procuto, int cantidad);
    }
    public class CarritoRepository : ICarritoRepository
    {
        private string _connectionString;
        public CarritoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool AgregarProducto(int producto_id, int cliente_id, int cantidad)
        {
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO CARRITO (producto_id, cliente_id, cantidad) VALUES (@id_producto, @id_cliente, @cant)";
                var resultado = conn.Execute(query, new { id_producto = producto_id, id_cliente = cliente_id, cant = cantidad });
                return resultado == 1;
            }
        }

        public bool EditarCantidadProducto(int id_cliente, int id_procuto, int cantidad)
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                if (cantidad > 0)
                {
                    string query = "UPDATE CARRITO SET cantidad = @Cantidad WHERE producto_id = @Id_producto AND cliente_id = @Id_cliente";
                    var resultado = connection.Execute(query, new { Cantidad = cantidad, Id_producto = id_procuto, Id_cliente = id_cliente });
                    return resultado == 1;
                }
                else
                {
                    this.EliminarProducto(id_procuto, id_cliente);
                }
                return false;
            }
        }

        public bool EliminarProducto(int id_producto, int id_cliente)
        {
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM CARRITO WHERE producto_id = @Id_producto AND cliente_id = @Id_cliente";
                var resultado = conn.Execute(query, new { Id_producto = id_producto, Id_cliente = id_cliente });
                return resultado == 1;
            }
        }

        public IEnumerable<Carrito> GetCarrito(int id_cliente)
        {
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {
                IEnumerable<Carrito> carritos = conn.Query<Carrito>("SELECT * FROM CARRITO WHERE cliente_id = @Id_cliente", new { Id_cliente = id_cliente }).ToList();
                return carritos;
            }
        }
    }
}
