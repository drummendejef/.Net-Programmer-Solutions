using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Universal
{
    class Data : ListBox
    {
        public List<string> rows;
        public int aantal = 0;
        private DataTable schemaTable = null;
        Random random = new Random();

        public void getData()
        {
            if (rows != null)
            {
                this.Items.Clear();
                addToList(getHeader());
                for (int i = 0; i < rows.Count(); i++)
                {
                    addToList(rows[i]);
                }
            }
        }

        private void addToList(string value)
        {
            Item item = new Item(value);
            this.Items.Add(item);
        }

        private string getHeader()
        {
            string header = "";
            foreach (DataRow colRow in schemaTable.Rows)
            {
                string naam = colRow.Field<String>("ColumnName");
                header += naam + " ";
            }
            return header;
        }

        public void getData(string name, SqlConnection sqlConnection, string items)
        {
            sqlConnection.Open();
            int offset = GetOffset(items, sqlConnection);
            SqlCommand command = new SqlCommand("SELECT * FROM " + name + " ORDER BY Id " + " OFFSET " + offset + " ROWS FETCH NEXT " + aantal + " ROWS ONLY", sqlConnection);
            SqlDataReader reader = command.ExecuteReader();
            schemaTable = reader.GetSchemaTable();
            if (reader.HasRows)
            {
                rows = new List<string>();
                while (reader.Read())
                {
                    string row = "";
                    foreach (DataRow colRow in schemaTable.Rows)
                    {
                        string naam = colRow.Field<String>("ColumnName");
                        row += reader[naam] + " ";
                    }
                    rows.Add(row);
                }
            }
            getData();
            reader.Close();
            sqlConnection.Close();
        }

        private int GetOffset(string items, SqlConnection sqlConnection)
        {
            if (items == "") return 0;
            string[] values = rows[0].Split(' ');
            int id = Int32.Parse(values[0]);
            int rowId = GetRow(sqlConnection, id);
            if (items == "next")
            {
                return rowId + aantal-1;
            }
            if (items == "previous")
            {
                int offset = rowId - aantal-1;
                if (offset >= 0) return offset;
                else return 0;
            }
            return -1;
        }

        private int GetRow(SqlConnection sqlConnection, int id)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY Id) rown, * FROM Persoon) tmp WHERE tmp.id = " + id, sqlConnection);
            SqlDataReader reader = command.ExecuteReader();
            DataTable schemaTable = reader.GetSchemaTable();
            reader.Read();
            string naam = schemaTable.Rows[0].Field<String>("ColumnName");
            int idRow = Int32.Parse(reader[naam].ToString());
            reader.Close();
            return idRow;
        }


        private void Dummy()
        {
            //de gegevens zijn gesorteerd op FNaam, VNaam, GebDatum
            //veronderstel vFnaam, vVnaam en vGebDatum bevatten de waardes van de laatste rij die nu getoond wordt
            SqlCommand command = new SqlCommand("SELECT * FROM Persoon where " +
                "Fnaam > @Fnaam or (FNaam = @Fnaam and VNaam > @Vnaam) or (Fnaam = @Fnaam and Vnaam = @Vnaam and GebDatum > @gebdatum) oreder by Fnaam, Vnaam, gebdatum"
                
                
                
                ) tmp WHERE tmp.id = " + id, sqlConnection);
        }






        public void add(string name, SqlConnection sqlConnection)
        {
            sqlConnection.Open();
            String[] list = { "jan", "piet", "joris", "ann", "klaas" };
            for (int i = 0; i < 100; i++)
            {
                SqlCommand command = sqlConnection.CreateCommand();
                command.CommandText = "INSERT INTO " + name + "(Voornaam,Naam,Leeftijd) VALUES(@Voornaam,@Naam,@Leeftijd)";

                command.Parameters.AddWithValue("@Voornaam", list[random.Next(list.Count())]);
                command.Parameters.AddWithValue("@Naam", list[random.Next(list.Count())]);
                command.Parameters.AddWithValue("@Leeftijd", i);

                command.ExecuteNonQuery();
            }
        }
    }
}
