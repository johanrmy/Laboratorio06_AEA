using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Laboratorio06
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string connectionString = "Data Source=LAB1504-19\\SQLEXPRESS;Initial Catalog=Neptuno;User Id=johanramor;Password=123456";

        public MainWindow()
        {
            InitializeComponent();
            ListarEmpleados();
        }

        private void ListarEmpleados()
        {
            List<EmpleadoListado> empleados = new List<EmpleadoListado>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SP_listar_empleados", connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        int idEmpleado = Convert.ToInt32(reader["IdEmpleado"]);
                        string apellidos = reader.IsDBNull(reader.GetOrdinal("Apellidos")) ? string.Empty : reader.GetString(reader.GetOrdinal("Apellidos"));
                        string nombre = reader.IsDBNull(reader.GetOrdinal("Nombre")) ? string.Empty : reader.GetString(reader.GetOrdinal("Nombre"));
                        string cargo = reader.IsDBNull(reader.GetOrdinal("Cargo")) ? string.Empty : reader.GetString(reader.GetOrdinal("Cargo"));
                        string direccion = reader.IsDBNull(reader.GetOrdinal("direccion")) ? string.Empty : reader.GetString(reader.GetOrdinal("direccion"));
                        DateTime fechaContratacion = reader.IsDBNull(reader.GetOrdinal("FechaContratacion")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("FechaContratacion"));
                        string ciudad = reader.IsDBNull(reader.GetOrdinal("Ciudad")) ? string.Empty : reader.GetString(reader.GetOrdinal("Ciudad"));
                        string pais = reader.IsDBNull(reader.GetOrdinal("pais")) ? string.Empty : reader.GetString(reader.GetOrdinal("pais"));
                        string codPostal = reader.IsDBNull(reader.GetOrdinal("codPostal")) ? string.Empty : reader.GetString(reader.GetOrdinal("codPostal"));
                        decimal sueldoBasico = reader.IsDBNull(reader.GetOrdinal("SueldoBasico")) ? 0 : reader.GetDecimal(reader.GetOrdinal("SueldoBasico"));

                        empleados.Add(new EmpleadoListado
                        {
                            IdEmpleado = idEmpleado,
                            Apellidos = apellidos,
                            Nombre = nombre,
                            Cargo = cargo,
                            FechaContratacion = fechaContratacion.Date,
                            Direccion = direccion,
                            Ciudad = ciudad,
                            Pais = pais,
                            CodPostal = codPostal,
                            SueldoBasico = sueldoBasico
                        });
                    }
                }

                dgGrid.ItemsSource = empleados;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al listar empleados: " + ex.Message);
            }
        }

        private void dgGrid_selected(object sender, SelectionChangedEventArgs e)
        {

            EmpleadoListado empleadoSelected = (EmpleadoListado)dgGrid.SelectedItem;

            if (empleadoSelected != null)
            {
                txtBoxApellidos.Text = empleadoSelected.Apellidos;
                txtBoxNombre.Text = empleadoSelected.Nombre;
                txtBoxDireccion.Text = empleadoSelected.Direccion;
                txtBoxCargo.Text = empleadoSelected.Cargo;
                txtBoxContratacion.Text = empleadoSelected.FechaContratacion.ToString();
                txtBoxCodPostal.Text = empleadoSelected.CodPostal;
                txtBoxCiudad.Text = empleadoSelected.Ciudad;
                txtBoxPais.Text = empleadoSelected.Pais;
                txtBoxSueldo.Text = empleadoSelected.SueldoBasico.ToString();

            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            EmpleadoListado empleadoSelected = (EmpleadoListado)dgGrid.SelectedItem;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SP_eliminar_empleado", connection);

                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IdEmpleado", empleadoSelected.IdEmpleado);

                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Registro Eliminado");
                    ListarEmpleados();
                    
                }
                catch (Exception ex) {
                    MessageBox.Show("Error al eliminar empleado: " + ex.Message);
                }
            }
        }

        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            EmpleadoListado empleadoSelected = (EmpleadoListado)dgGrid.SelectedItem;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SP_update_empleado", connection);

                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IdEmpleado", empleadoSelected.IdEmpleado);
                command.Parameters.AddWithValue("@Apellidos", txtBoxApellidos.Text);
                command.Parameters.AddWithValue("@Nombre", txtBoxNombre.Text);
                command.Parameters.AddWithValue("@direccion", txtBoxDireccion.Text);
                command.Parameters.AddWithValue("@cargo", txtBoxCargo.Text);
                command.Parameters.AddWithValue("@FechaContratacion", txtBoxContratacion.Text);
                command.Parameters.AddWithValue("@codPostal", txtBoxCodPostal.Text);
                command.Parameters.AddWithValue("@ciudad", txtBoxCiudad.Text);
                command.Parameters.AddWithValue("@pais", txtBoxPais.Text);
                command.Parameters.AddWithValue("@sueldoBasico", txtBoxSueldo.Text);

                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Registro Actualizado");
                    ListarEmpleados();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar empleado: " + ex.Message);
                }
            }
        }
    }
}
