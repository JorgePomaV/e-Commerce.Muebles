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
    public interface IHistorialRepositorio
    {
        IEnumerable<Historial> GetAllHistorial();
        IEnumerable<Historial> GetHistorialByUsuarioId(int usuarioId);
        void AddHistorial(Historial historial);
        void DeleteHistorial(int id);
    }

    public class HistorialRepos : IHistorialRepositorio
    {
        private readonly SqlConnection _connection;

        public HistorialRepos(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        public IEnumerable<Historial> GetAllHistorial()
        {
            return _connection.Query<Historial>("SELECT * FROM Historial");
        }

        public IEnumerable<Historial> GetHistorialByUsuarioId(int usuarioId)
        {
            return _connection.Query<Historial>("SELECT * FROM Historial WHERE UsuarioId = @UsuarioId", new { UsuarioId = usuarioId });
        }

        public void AddHistorial(Historial historial)
        {
            _connection.Execute("INSERT INTO Historial (UsuarioId, VentaId) VALUES (@UsuarioId, @VentaId)", historial);
        }

        public void DeleteHistorial(int id)
        {
            _connection.Execute("DELETE FROM Historial WHERE IdHistorial = @Id", new { Id = id });
        }
    }
}
