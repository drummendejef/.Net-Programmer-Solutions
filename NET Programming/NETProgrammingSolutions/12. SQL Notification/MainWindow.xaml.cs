using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Permissions;
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

namespace _12.SQL_Notification
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int _changeCount;
        private const string TABLENAME = "People";
        private const string STATUSMESSAGE = "{0} changes have occurred.";

        private DataSet _dataToWatch;
        private SqlConnection _connection;
        private SqlCommand _command;


        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _changeCount = 0;
            lblChangeCount.Content = String.Format(STATUSMESSAGE, _changeCount);

            // Remove any existing dependency connection, then create a new one.
            SqlDependency.Stop(GetConnectionString());
            SqlDependency.Start(GetConnectionString());

            if (_connection == null)
                _connection = new SqlConnection(GetConnectionString());

            if (_command == null)
            {
                // GetSQL is a local procedure that returns
                // a paramaterized SQL string. You might want
                // to use a stored procedure in your application.
                _command = new SqlCommand(GetSql(), _connection);
            }
            if (_dataToWatch == null)
            {
                _dataToWatch = new DataSet();
            }

            GetData();
        }

        private string GetConnectionString()
        {
            return @"Data Source=.\SQLEXPRESS;Initial Catalog=SQLNET;Integrated Security=True";
        }

        private string GetSql()
        {
            return "select id, name, address from dbo.people";
        }

        private void Dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            // This event will occur on a thread pool thread.
            // Updating the UI from a worker thread is not permitted.
            // The following code checks to see if it is safe to
            // update the UI.


            //CheckAccess is verborgen in intellisense, maar werkt wel.
            //Dit checkt of de huidige thread toegang heeft tot de UI.
            if (!Dispatcher.CheckAccess())
            {
                // Create a delegate to perform the thread switch.
                OnChangeEventHandler tempDelegate = new OnChangeEventHandler(Dependency_OnChange);

                object[] args = { sender, e };

                // Marshal the data from the worker thread
                // to the UI thread.
                Dispatcher.BeginInvoke(tempDelegate, args);
                return;
            }

            // Remove the handler, since it is only good
            // for a single notification.
            SqlDependency dependency =
                (SqlDependency)sender;

            dependency.OnChange -= Dependency_OnChange;

            // At this point, the code is executing on the
            // UI thread, so it is safe to update the UI.
            ++_changeCount;
            lblChangeCount.Content = String.Format(STATUSMESSAGE, _changeCount);

            // Reload the dataset that is bound to the grid.
            GetData();
        }

        private void GetData()
        {
            // Empty the dataset so that there is only
            // one batch of data displayed.
            _dataToWatch.Clear();

            // Make sure the command object does not already have
            // a notification object associated with it.
            _command.Notification = null;

            // Create and bind the SqlDependency object
            // to the command object.
            SqlDependency dependency = new SqlDependency(_command);
            dependency.OnChange += new
                OnChangeEventHandler(Dependency_OnChange);

            using (SqlDataAdapter adapter =
                new SqlDataAdapter(_command))
            {
                adapter.Fill(_dataToWatch, TABLENAME);
                dataGrid.ItemsSource = _dataToWatch.Tables["People"].DefaultView;
            }
        }


        // Code requires directives to
        // System.Security.Permissions and
        // System.Data.SqlClient
        private bool CanRequestNotifications()
        {
            SqlClientPermission permission =
                new SqlClientPermission(
                PermissionState.Unrestricted);
            try
            {
                permission.Demand();
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btnAccessDb.IsEnabled = CanRequestNotifications();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            SqlDependency.Stop(GetConnectionString());
            if (_connection != null)
            {
                _connection.Close();
            }
        }
    }
}
