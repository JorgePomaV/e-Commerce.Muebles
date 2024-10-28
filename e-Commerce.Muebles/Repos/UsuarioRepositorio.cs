using e_Commerce.Muebles.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace e_Commerce.Muebles.Repos
{
    public interface IUserRepositorio
    {
        IEnumerable<Usuario> GetAllUsuarios();
        Usuario GetUsuarioById(int id);
        void AddUsuario(Usuario usuario);
        void UpdateUsuario(Usuario usuario);
        void DeleteUsuario(int id);
    }

    public class UserRepos : IUserRepositorio
    {
        private readonly SqlConnection _connection;

        public UserRepos(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        public IEnumerable<Usuario> GetAllUsuarios()
        {
            return _connection.Query<Usuario>("SELECT * FROM Usuario");
        }

        public Usuario GetUsuarioById(int id)
        {
            return _connection.QuerySingleOrDefault<Usuario>("SELECT * FROM Usuario WHERE IdUsuario = @Id", new { Id = id });
        }

        public void AddUsuario(Usuario usuario)
        {
            _connection.Execute("INSERT INTO Usuario (Nombre, Apellido, Telefono) VALUES (@Nombre, @Apellido, @Telefono)", usuario);
        }

        public void UpdateUsuario(Usuario usuario)
        {
            _connection.Execute("UPDATE Usuario SET Nombre = @Nombre, Apellido = @Apellido, Telefono = @Telefono WHERE IdUsuario = @Id", usuario);
        }

        public void DeleteUsuario(int id)
        {
            _connection.Execute("DELETE FROM Usuario WHERE IdUsuario = @Id", new { Id = id });
        }
    }
}
