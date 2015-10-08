//------------------------------------------------------------------------------
// <copyright file="CSSqlClassFile.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Text;
using Microsoft.SqlServer.Server;

namespace SquaredDemo
{
    public class SelectPerson
    {
        private int _id;
        private string _name;
        public SelectPerson(int id, string name)
        {
            _id = id;
            _name = name;
        }

        [SqlFunction(FillRowMethodName="FillRows", TableDefinition="id int, name nvarchar(50)", DataAccess=DataAccessKind.Read)]
        public static IEnumerable SelectPeople(string name)
        {
            ArrayList people = new ArrayList();

            using (SqlConnection connection = new SqlConnection("context connection=true"))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("select * from dbo.People where name like @name", connection);
                command.Parameters.Add(new SqlParameter("@name", "%" + name + "%"));
                var reader = command.ExecuteReader();

                if(!reader.HasRows)
                {
                    reader.Close();
                    return people;
                }
                   

                while(reader.Read())
                {
                    people.Add(new SelectPerson((int)reader["id"], reader["name"].ToString()));
                }

                reader.Close();
                return people;
            }
        }

        public static void FillRows(object person, out SqlInt32 id, out SqlString name)
        {
            id = ((SelectPerson)person)._id;
            name = ((SelectPerson)person)._name;
        }
    }
}
