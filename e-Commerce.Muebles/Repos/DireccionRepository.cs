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
    public interface IDireccionRepository
    {
        bool AgregarDireccion(Direccion direccion);
        bool EditarDireccion(Direccion direccion);
        Direccion GetDireccion(int id_direccion);
        bool BorrarDireccion(int id_cliente);
    }
    public class DireccionRepository : IDireccionRepository
    {
        private string _ConnectionString;
        public DireccionRepository(string connectionString)
        {
            _ConnectionString = connectionString;
        }
        public bool AgregarDireccion(Direccion direccion)
        {
            using (IDbConnection con = new SqlConnection(_ConnectionString))
            {
                string query = "ISERT INTO DIRECCION (id_direccion, calle, altura, departamento, barrio, localidad) VALUES (@id_direccion, @calle, @altura, @departamoento, @barrio, @localidad)";
                var resultado = con.Execute(query, direccion);
                return resultado == 1;
            }
        }

        public bool BorrarDireccion(int id_direccion)
        {
            using (IDbConnection conn = new SqlConnection(_ConnectionString))
            {
                string query = "DELETE FROM DIRECCION WHERE id_direccion = @Id_Direcion";
                var resultado = conn.Execute(query, new { Id_direccin = id_direccion });
                return resultado == 1;
            }



        }

        public bool EditarDireccion(Direccion direccion)
        {
            using (IDbConnection conn = new SqlConnection(_ConnectionString))
            {
                string query = "UPDATE DIRECCION SET calle = @calle, altura = @altura, departamento = @departamento, barrio = @barrio, localidad = @localidad WHERE id_direccion = @id_direccion";
                var resultado = conn.Execute(query, direccion);
                return resultado == 1;
            }
        }

        public Direccion GetDireccion(int id_direccion)
        {
            using (IDbConnection conn = new SqlConnection(_ConnectionString))
            {
                Direccion direccion = conn.QuerySingle<Direccion>("SELECT * FROM DIRECCION WHERE id_direccion = @Id_direccion", new { Id_direccion = id_direccion });
                return direccion;
            }
        }
    }
}
