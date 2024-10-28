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
    public interface IAuthRepositorio
    {
        Autenticacion GetAutenticacionByEmail(string email);
        void AddAutenticacion(Autenticacion autenticacion);
        void UpdateAutenticacion(Autenticacion autenticacion);
        void DeleteAutenticacion(int id);
    }

    public class AuthRepos : IAuthRepositorio
    {
        private readonly SqlConnection _connection;

        public AuthRepos(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        public Autenticacion GetAutenticacionByEmail(string email)
        {
            return _connection.QuerySingleOrDefault<Autenticacion>("SELECT * FROM Autenticacion WHERE Email = @Email", new { Email = email });
        }

        public void AddAutenticacion(Autenticacion autenticacion)
        {
            _connection.Execute("INSERT INTO Autenticacion (Email, Clave, UsuarioId) VALUES (@Email, @Clave, @UsuarioId)", autenticacion);
        }

        public void UpdateAutenticacion(Autenticacion autenticacion)
        {
            _connection.Execute("UPDATE Autenticacion SET Email = @Email, Clave = @Clave WHERE IdAtenticacion = @Id", autenticacion);
        }

        public void DeleteAutenticacion(int id)
        {
            _connection.Execute("DELETE FROM Autenticacion WHERE IdAtenticacion = @Id", new { Id = id });
        }
    }
}
