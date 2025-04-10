using System;
using System.Windows.Forms;
using Npgsql;

namespace Construccion.Construc
{
    internal class CConexion
    {
        NpgsqlConnection conex = new NpgsqlConnection();

        static String servidor = "localhost";
        static String bd = "hotel_db";
        static String usuario = "postgres";
        static String password = "Jadrian8";
        static String puerto = "5432";

        String cadenaConexion = "server=" + servidor + ";" + "port=" + puerto + ";" + "user id=" + usuario + ";" + "password=" + password + ";" + "database=" + bd + ";";

        public NpgsqlConnection establecerConexion()
        {
            try
            {
                conex.ConnectionString = cadenaConexion;
                conex.Open();
                MessageBox.Show("Se estableció conexión con la base de datos.");
            }
            catch (NpgsqlException e)
            {
                MessageBox.Show("No se pudo conectar a la base de datos en PostgreSQL.");
            }

            return conex;
        }

        internal bool login(string idusuario, string contrasena)
        {
            bool loginExitoso = false;

            try
            {
                string query = "SELECT COUNT(*) FROM hotel.usuario WHERE idusuario = @idusuario AND contra = @contrasena";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conex))
                {
                    cmd.Parameters.AddWithValue("@idusuario", idusuario);
                    cmd.Parameters.AddWithValue("@contrasena", contrasena);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    if (count > 0)
                    {
                        loginExitoso = true;                  }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en el proceso de login: " + ex.Message);
            }

            return loginExitoso;
        }
    }
}
