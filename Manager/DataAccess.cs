using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DataAccess
    {
        private static SqlConnection CreateConnection()
        {

            string connectionString =
                "Data Source=(local)\SQL2012SP1;" +
                "Initial Catalog=BQSample2015;" +
                "User ID=sa;" +
                "Password=iaf349";

            return new SqlConnection(connectionString);

        }

        private static SqlCommand prepareCommand()
        {
            SqlCommand command = new SqlCommand();
            command.Connection = CreateConnection();

            try
            {
                command.Connection.Open();
            }
            catch (Exception ex)
            {
            }
            return command;
        }

        public static object ExecuteScalar(string Query)
        {
            object value;

            using (var command = prepareCommand())
            {
                command.CommandText = Query;
                command.CommandType = CommandType.Text;
                value = command.ExecuteScalar();
                command.Connection.Close();
            }
            return value;
        }

        public static T GetObject<T>(string Query)
        {

            T model = default(T);
            var command = prepareCommand();
            command.CommandText = Query;
            command.CommandType = CommandType.Text;

            using (var myReader = command.ExecuteReader(0))
            {
                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        model = default(T);
                        model = (T)Activator.CreateInstance(typeof(T));

                        break;
                    }
                }
                myReader.Close();
            }

            return model;
        }

    }
}
