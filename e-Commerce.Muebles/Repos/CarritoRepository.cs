using e_Commerce.Muebles.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using e_Commerce.Muebles.ModelFactories;

namespace e_Commerce.Muebles.Repos
{
    public interface ICarritoRepository
    {
        bool AgregarProducto(int id_producto, int id_cliente, int cantidad);
        bool EliminarProducto(int id_producto, int id_cliente);
        IEnumerable<Carrito> GetCarritos(int id_cliente);
        bool EditarCantidadProducto(int id_cliente, int id_procuto, int cantidad);
        public IEnumerable<CarritoCompleto> GetCarritosCompleto(int id_cliente);
        public bool AgregarProductoAlCarrito(int clienteId, int productoId, int cantidad);
        public bool RestarCantidadProducto(int productoId, int cantidadArestar);
        public int RegistrarVenta(int usuarioId, decimal monto, string estado);
        public bool RegistrarHistorialVenta(int ventaId, int productoId, int cantidad);
        public bool VaciarCarrito(int id_cliente);
        public bool FinalizarCompra (int id_cliente);
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

        public IEnumerable<Carrito> GetCarritos(int id_cliente)
        {
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {
                IEnumerable<Carrito> carritos = conn.Query<Carrito>("SELECT * FROM CARRITO WHERE cliente_id = @Id_cliente", new { Id_cliente = id_cliente }).ToList();
                return carritos;
            }
        }

        public IEnumerable<CarritoCompleto> GetCarritosCompleto(int id_cliente)
        {
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {
                string sql = @"
                                SELECT 
                                    c.id_carrito, c.cantidad, c.producto_id, c.cliente_id,
                                    p.id_producto, p.nombre, p.precio
                                FROM Carrito c
                                INNER JOIN Producto p ON c.producto_id = p.id_producto
                                WHERE c.cliente_id = @ClienteId";

                IEnumerable<CarritoCompleto> carritosConProducto = conn.Query<CarritoCompleto, Producto, CarritoCompleto>(
                    sql,
                    (carrito, producto) =>
                    {
                        carrito.producto = producto; // Asignamos el producto al carrito
                        return carrito;
                    },
                    new { ClienteId = id_cliente }, // Parámetro para la consulta
                    splitOn: "id_producto" // Indica que los datos del producto comienzan a partir de "id_producto"
                ).ToList();
                return carritosConProducto;

            }
        }

        public bool AgregarProductoAlCarrito(int clienteId, int productoId, int cantidad)
        {
            // Verificamos si el producto ya existe en el carrito
            var carritos = this.GetCarritos(clienteId);
            var carritoItem = carritos?.FirstOrDefault(c => c.producto_id == productoId);

            if (carritoItem != null)
            {
                // Si ya existe, aumentamos la cantidad
                int nuevaCantidad = carritoItem.cantidad + cantidad;
                return this.EditarCantidadProducto(clienteId, productoId, nuevaCantidad);
            }
            else
            {
                // Si no existe, lo agregamos
                return this.AgregarProducto(productoId, clienteId, cantidad);
            }
        }

        public bool RestarCantidadProducto(int productoId, int cantidadArestar)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {

                // Obtenemos la cantidad actual del producto
                var producto = dbConnection.QueryFirstOrDefault<Producto>("SELECT * FROM Producto WHERE id_producto = @Id", new { Id = productoId });

                if (producto == null)
                {
                    return false; // Producto no encontrado
                }

                if (producto.stock < cantidadArestar)
                {
                    return false; // No hay suficiente inventario
                }

                // Restamos la cantidad del producto en inventario
                string query = "UPDATE Producto SET stock = stock - @Cantidad WHERE id_producto = @Id";
                var resultado = dbConnection.Execute(query, new { Cantidad = cantidadArestar, Id = productoId });

                return resultado > 0; // Si se actualizó el producto correctamente
            }
        }
        public int RegistrarVenta(int usuarioId, decimal monto, string estado)
        {
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Venta (usuario_id, fecha, monto, estado) OUTPUT INSERTED.id_venta VALUES (@UsuarioId, GETDATE(), @Monto, @Estado)";
                var idVenta = conn.Query<int>(query, new { UsuarioId = usuarioId, Monto = monto, Estado = estado }).Single();
                return idVenta;
            }
        }
        public bool RegistrarHistorialVenta(int ventaId, int productoId, int cantidad)
        {
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Historial (venta_id, producto_id, cantidad) VALUES (@VentaId, @ProductoId, @Cantidad)";
                var resultado = conn.Execute(query, new { VentaId = ventaId, ProductoId = productoId, Cantidad = cantidad});
                return resultado > 0;
            }
        }
        public bool VaciarCarrito(int clienteId)
        {
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM CARRITO WHERE cliente_id = @ClienteId";
                var resultado = conn.Execute(query, new { ClienteId = clienteId });
                return resultado > 0; // Retorna true si se eliminaron los elementos del carrito
            }
        }



        public bool FinalizarCompra(int clienteId)
        {
            try
            {
                using (IDbConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (var transaction = conn.BeginTransaction())
                    {
                        // Mover los productos del carrito al historial de compras
                        string queryHistorial = @"
                    INSERT INTO Historial (cliente_id, producto_id, cantidad, fecha)
                    SELECT cliente_id, producto_id, cantidad, GETDATE()
                    FROM Carrito
                    WHERE cliente_id = @ClienteId";
                        conn.Execute(queryHistorial, new { ClienteId = clienteId }, transaction);

                        // Vaciar el carrito
                        string queryVaciarCarrito = "DELETE FROM Carrito WHERE cliente_id = @ClienteId";
                        conn.Execute(queryVaciarCarrito, new { ClienteId = clienteId }, transaction);

                        // Confirmar la transacción
                        transaction.Commit();
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
