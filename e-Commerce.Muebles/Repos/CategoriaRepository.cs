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
    public interface ICategoriaRepository
    {
        IEnumerable<Categoria> GetCategorias();
        Categoria GetCategoria(int id);
        bool BorrarCategoria(int categoriaId);
        bool agregarCategoria(Categoria categoria);
        bool editarCategoria(int id, Categoria categoria);

    }
    public class CategoriaRepository : ICategoriaRepository
    {
        private string _ConnectionString;
        public CategoriaRepository(string ConnectionString)
        {
            _ConnectionString = ConnectionString;
        }

        public bool agregarCategoria(Categoria categoria)
        {
            using (IDbConnection conn = new SqlConnection(_ConnectionString))
            {
                string query = "INSERT INTO Categoria (id_categoria, categoria) VALUES (@id_categoria, @categoria)";
                var resultado = conn.Execute(query, categoria);
                return resultado == 1;
            }
        }

        public bool BorrarCategoria(int categoriaId)
        {
            using (IDbConnection conn = new SqlConnection(_ConnectionString))
            {
                string query = "DELETE FROM Categoria WHERE id_categoria = @categoriaId";
                var resultado = conn.Execute(query, new { categoriaId = categoriaId });
                return resultado == 1;
            }
        }

        public bool editarCategoria(int id, Categoria categoria)
        {
            using (IDbConnection con = new SqlConnection(_ConnectionString))
            {
                string query = "UPDATE Categoria SET categoria = @categoria WHERE id_categoria = @Id";
                var resultado = con.Execute(query, new { categoria = categoria.categoria, Id = id });
                return resultado == 1;
            }
        }

        public Categoria GetCategoria(int id)
        {
            using (IDbConnection con = new SqlConnection(_ConnectionString))
            {
                Categoria categoria = con.QuerySingleOrDefault<Categoria>( "SELECT * FROM Categoria WHERE id_categoria = @categoria_id", new { categoria_id = id } );
                return categoria;
            }
        }


        public IEnumerable<Categoria> GetCategorias()
        {
            using (IDbConnection conn = new SqlConnection(_ConnectionString))
            {
                IEnumerable<Categoria> categorias = conn.Query<Categoria>("SELECT * FROM Categoria").ToList();
                return categorias;
            }
        }


    }
}
