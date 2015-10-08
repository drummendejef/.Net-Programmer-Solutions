using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Universal
{
    class Table : ListBox
    {
        SqlConnection sqlConnection = null;
        Data data = null;
        public Table()
        {
            this.SelectionChanged += Table_SelectionChanged;
        }

        void Table_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBoxItem item = (ListBoxItem)this.SelectedItem;
            if (item!=null) data.getData(item.Content.ToString(), sqlConnection, "");
            //data.add(item.Content.ToString(), sqlConnection);
        }

        public void getDataNext()
        {
            ListBoxItem item = (ListBoxItem)this.SelectedItem;
            if (item != null) data.getData(item.Content.ToString(), sqlConnection, "next");
        }
        public void getDataPrevious()
        {
            ListBoxItem item = (ListBoxItem)this.SelectedItem;
            if (item != null) data.getData(item.Content.ToString(), sqlConnection, "previous");
        }
        public void getTables(string name, Data data)
        {
            this.data = data;
            string connString = "Data Source=CNU131260S\\DATAMANAGEMENT;Initial Catalog=" + name + ";Integrated Security=True";
            try
            {
                if (name != "")
                {
                    sqlConnection = new SqlConnection(connString);
                    sqlConnection.Open();
                }
            }
            catch (Exception e)
            {
                sqlConnection = null;
                Console.WriteLine("Couldn't make database: " + e);
            }
            if (sqlConnection != null)
            {
                DataTable t = sqlConnection.GetSchema("Tables");                
                this.Items.Clear();
                foreach (DataRow row in t.Rows)
                {
                    Item i = new Item(row[2].ToString());
                    this.Items.Add(i);
                }
                sqlConnection.Close();
            }
        }
    }
}
