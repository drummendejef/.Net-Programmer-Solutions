using System;
using System.Data;
using System.Data.SqlClient;

namespace KwadraatDemo
{
    class RunScript
    {
        public RunScript() {
            using (SqlConnection con = new SqlConnection("context connection=true"))
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
        }
    }
}