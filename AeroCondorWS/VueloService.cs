using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Services;

namespace AeroCondorWS
{
    public class VueloService
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["ViajecitosDB"].ConnectionString;

        public List<Vuelo> ObtenerVuelos(string origen, string destino)
        {
            var vuelos = new List<Vuelo>();
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM Vuelos WHERE CIUDAD_ORIGEN = @origen AND CIUDAD_DESTINO = @destino", conn);
                cmd.Parameters.AddWithValue("@origen", origen);
                cmd.Parameters.AddWithValue("@destino", destino);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        vuelos.Add(new Vuelo
                        {
                            Id = (int)reader["Id"],
                            CiudadOrigen = reader["CIUDAD_ORIGEN"].ToString(),
                            CiudadDestino = reader["CIUDAD_DESTINO"].ToString(),
                            Valor = (decimal)reader["VALOR"],
                            HoraSalida = (DateTime)reader["HORA_SALIDA"]
                        });
                    }
                }
            }
            return vuelos;
        }

        public string ComprarVuelo(int vueloId, int usuarioId)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("INSERT INTO Compras (VueloId, id_usuario, PurchaseDate) VALUES (@vueloId, @usuarioId, GETDATE())", conn);
                cmd.Parameters.AddWithValue("@vueloId", vueloId);
                cmd.Parameters.AddWithValue("@usuarioId", usuarioId);
                int rows = cmd.ExecuteNonQuery();
                return rows > 0 ? "Compra realizada con éxito" : "Error al realizar la compra";
            }
        }

        public Vuelo ObtenerVueloPorId(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM Vuelos WHERE Id = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Vuelo
                        {
                            Id = (int)reader["Id"],
                            CiudadOrigen = reader["CIUDAD_ORIGEN"].ToString(),
                            CiudadDestino = reader["CIUDAD_DESTINO"].ToString(),
                            Valor = (decimal)reader["VALOR"],
                            HoraSalida = (DateTime)reader["HORA_SALIDA"]
                        };
                    }
                }
            }
            return null;
        }

        public List<Vuelo> ObtenerVuelosPorUnFiltro(string tipoFiltro, string valor)
        {
            var vuelos = new List<Vuelo>();
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string query = "";
                SqlCommand cmd = new SqlCommand();

                if (tipoFiltro.ToLower() == "origen")
                {
                    query = "SELECT * FROM Vuelos WHERE CIUDAD_ORIGEN = @valor";
                    cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@valor", valor);
                }
                else if (tipoFiltro.ToLower() == "destino")
                {
                    query = "SELECT * FROM Vuelos WHERE CIUDAD_DESTINO = @valor";
                    cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@valor", valor);
                }
                else if (tipoFiltro.ToLower() == "fecha")
                {
                    query = "SELECT * FROM Vuelos WHERE CONVERT(date, HORA_SALIDA) = @valorFecha";
                    cmd = new SqlCommand(query, conn);
                    if (!DateTime.TryParse(valor, out DateTime fecha))
                        return vuelos; // Fecha inválida → lista vacía
                    cmd.Parameters.AddWithValue("@valorFecha", fecha.Date);
                }
                else
                {
                    return vuelos; // Filtro no válido
                }

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        vuelos.Add(new Vuelo
                        {
                            Id = (int)reader["Id"],
                            CiudadOrigen = reader["CIUDAD_ORIGEN"].ToString(),
                            CiudadDestino = reader["CIUDAD_DESTINO"].ToString(),
                            Valor = (decimal)reader["VALOR"],
                            HoraSalida = (DateTime)reader["HORA_SALIDA"]
                        });
                    }
                }
            }

            return vuelos;
        }

        public string Login(string username, string password)
        {
            return (username == "Monster" && password == "Monster9")
                ? "Acceso concedido"
                : "Credenciales incorrectas";
        }

        public string ComprarVuelo(int vueloId, int usuarioId, int cantidad)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // 1. Insertar en Compras
                    var insertCompra = new SqlCommand(
                        "INSERT INTO Compras (VueloId, id_usuario, PurchaseDate, Cantidad) " +
                        "OUTPUT INSERTED.Id VALUES (@vueloId, @usuarioId, GETDATE(), @cantidad)", conn, transaction);
                    insertCompra.Parameters.AddWithValue("@vueloId", vueloId);
                    insertCompra.Parameters.AddWithValue("@usuarioId", usuarioId);
                    insertCompra.Parameters.AddWithValue("@cantidad", cantidad);

                    int compraId = (int)insertCompra.ExecuteScalar();

                    // 2. Obtener datos del vuelo
                    var getVuelo = new SqlCommand("SELECT VALOR FROM Vuelos WHERE Id = @vueloId", conn, transaction);
                    getVuelo.Parameters.AddWithValue("@vueloId", vueloId);

                    decimal precioUnitario = (decimal)getVuelo.ExecuteScalar();
                    decimal precioTotal = precioUnitario * cantidad;

                    // 3. Insertar en Facturas
                    var insertFactura = new SqlCommand(
                        "INSERT INTO Facturas (CompraId, UsuarioId, VueloId, Cantidad, PrecioUnitario, PrecioTotal, FechaEmision) " +
                        "VALUES (@compraId, @usuarioId, @vueloId, @cantidad, @precioUnitario, @precioTotal, GETDATE())", conn, transaction);

                    insertFactura.Parameters.AddWithValue("@compraId", compraId);
                    insertFactura.Parameters.AddWithValue("@usuarioId", usuarioId);
                    insertFactura.Parameters.AddWithValue("@vueloId", vueloId);
                    insertFactura.Parameters.AddWithValue("@cantidad", cantidad);
                    insertFactura.Parameters.AddWithValue("@precioUnitario", precioUnitario);
                    insertFactura.Parameters.AddWithValue("@precioTotal", precioTotal);

                    insertFactura.ExecuteNonQuery();

                    transaction.Commit();

                    return $"Compra realizada con éxito. Factura generada con total de ${precioTotal:F2}";
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return $"Error al realizar la compra: {ex.Message}";
                }
            }
        }

        public Factura ObtenerFacturaPorId(int facturaId)
        {
            Factura factura = null;
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = @"
            SELECT 
                f.Id AS IdFactura,
                u.id_usuario,
                u.nombre,
                u.edad,
                u.nacionalidad,
                v.CIUDAD_ORIGEN,
                v.CIUDAD_DESTINO,
                v.HORA_SALIDA,
                f.Cantidad,
                f.PrecioUnitario,
                f.PrecioTotal,
                f.FechaEmision
            FROM Facturas f
            INNER JOIN Usuarios u ON f.UsuarioId = u.id_usuario
            INNER JOIN Vuelos v ON f.VueloId = v.Id
            WHERE f.Id = @facturaId";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@facturaId", facturaId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            factura = new Factura
                            {
                                Id = (int)reader["IdFactura"],
                                UsuarioId = (int)reader["id_usuario"],
                                NombreUsuario = reader["nombre"].ToString(),
                                EdadUsuario = (int)reader["edad"],
                                NacionalidadUsuario = reader["nacionalidad"].ToString(),
                                CiudadOrigen = reader["CIUDAD_ORIGEN"].ToString(),
                                CiudadDestino = reader["CIUDAD_DESTINO"].ToString(),
                                HoraSalida = (DateTime)reader["HORA_SALIDA"],
                                Cantidad = (int)reader["Cantidad"],
                                PrecioUnitario = (decimal)reader["PrecioUnitario"],
                                PrecioTotal = (decimal)reader["PrecioTotal"],
                                FechaEmision = (DateTime)reader["FechaEmision"]
                            };
                        }
                    }
                }
            }
            return factura;
        }
    }
}
