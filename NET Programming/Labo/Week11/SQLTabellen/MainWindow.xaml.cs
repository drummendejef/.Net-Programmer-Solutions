using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SQLTabellen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            sql.ItemsSource = getDataFromDatabase();
        }

        private List<Position> getDataFromDatabase()
        {
            List<Position> pos = new List<Position>();

            using (SqlConnection con = new SqlConnection("context connection=true"))
            {
                con.Open();

                con.Close();
            }
            /*
            string connString = "Data Source=POEKIE-PC\\SQLEXPRESS;Initial Catalog=SQLNET;Integrated Security=False;User ID=sa;Password=;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";

            using (SqlConnection con = new SqlConnection(connString))//"context connection=true"))
            {
                con.Open();

                using (SqlCommand command = new SqlCommand("SELECT id, xpos, ypos FROM dbo.positions WHERE xpos>@xpos AND ypos>@ypos ORDER BY xpos, ypos, id", con))
                {
                    command.Parameters.Add("@xpos", SqlDbType.NVarChar);
                    command.Parameters["@xpos"].Value = 150;
                    command.Parameters.Add("@ypos", SqlDbType.NVarChar);
                    command.Parameters["@ypos"].Value = 150;

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        // Loop through all the columns. 
                        object value = null;
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            if (reader.IsDBNull(i))
                            {
                                value = "<NULL>";
                            }
                            else
                            {
                                value = reader.GetValue(i);
                            }
                            Console.WriteLine("{0}: {1} ({2})", reader.GetName(i),
                                value, reader.GetFieldType(i).Name);
                        }
                    }
                }
            }
            */
            return pos;
        }
    }
}
