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
        public int AddUsuario(Usuario usuario, Autenticacion autenticacion);
        void UpdateUsuario(Usuario usuario);
        void DeleteUsuario(int id);
        public Usuario? GetUsuarioPorGoogleSubject(string googleSubject);
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

        public int AddUsuario(Usuario usuario, Autenticacion autenticacion)
        {
            //se agregan datos manualmente por que las tabla no aceptan null
            usuario.Telefono = "11111111";
            autenticacion.Clave = "12345";
            int resultado = _connection.QuerySingle<int>("INSERT INTO Usuario (Nombre, Apellido, Telefono, tipo_usuario) VALUES (@Nombre, @Apellido, @Telefono, @tipo_usuario); SELECT SCOPE_IDENTITY();", usuario);
            autenticacion.usuario_id = resultado;
            _connection.Execute("INSERT INTO Autenticacion (Email, Clave, usuario_id, GoogleIdentificador) VALUES (@Email, @Clave, @usuario_id, @GoogleIdentificador)", autenticacion);
            return resultado;
        }


        public void UpdateUsuario(Usuario usuario)
        {
            _connection.Execute("UPDATE Usuario SET Nombre = @Nombre, Apellido = @Apellido, Telefono = @Telefono WHERE IdUsuario = @Id", usuario);
        }

        public void DeleteUsuario(int id)
        {
            _connection.Execute("DELETE FROM Usuario WHERE IdUsuario = @Id", new { Id = id });
        }
        public Usuario? GetUsuarioPorGoogleSubject(string googleSubject)
        {
            Usuario usuarios = _connection.Query<Usuario>("SELECT u.* FROM Usuario u INNER JOIN Autenticacion a ON u.id_usuario = a.usuario_id WHERE a.GoogleIdentificador = '" + googleSubject.ToString() + "'").FirstOrDefault();

            return usuarios;

        }
    }
}
