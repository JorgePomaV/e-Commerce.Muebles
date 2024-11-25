using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using e_Commerce.Muebles.Entidades;

namespace e_Commerce.Muebles.Repos
{
    public interface IProductoRepositorio
      {
         IEnumerable<Producto> GetAllProductos();
         Producto GetProductoById(int id);
         void AddProducto(Producto producto);
         void UpdateProducto(Producto producto);
         void DeleteProducto(int id);
    }

    public class ProductoRepos : IProductoRepositorio
    {
        private readonly SqlConnection _connection;

        public ProductoRepos(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        public IEnumerable<Producto> GetAllProductos()
        {
            return _connection.Query<Producto>("SELECT * FROM Producto");
        }

        public Producto GetProductoById(int id)
        {
            return _connection.QuerySingleOrDefault<Producto>("SELECT * FROM Producto WHERE id_producto = @Id", new { Id = id });
        }

        public void AddProducto(Producto producto)
        {
            _connection.Execute("INSERT INTO Producto (nombre, descripcion, precio, stock, categoria_id) VALUES (@nombre, @descripcion, @precio, @stock, @categoria_id)", producto);
        }

        public void UpdateProducto(Producto producto)
        {
            _connection.Execute("UPDATE Producto SET nombre = @nombre, descripcion = @descripcion, precio = @precio, stock = @stock, categoria_id = @categoria_id WHERE id_producto = @id_producto", producto);
        }

        public void DeleteProducto(int id)
        {
          
            _connection.Execute("DELETE FROM Carrito WHERE producto_id = @Id", new { Id = id });

            _connection.Execute("DELETE FROM Producto WHERE id_producto = @Id", new { Id = id });
        }

    }

}
