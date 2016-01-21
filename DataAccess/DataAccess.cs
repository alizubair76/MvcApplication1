using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataAccess
{
    public class _GeneralFunctions
    {
        private static SqlConnection CreateConnection()
        {

            string settingPath = System.IO.Directory.GetCurrentDirectory() + @"\Setting\Setting.xml";
            XDocument settingXML = XDocument.Load(settingPath);

            XElement settings = settingXML.Element("Settings");

            XElement sqlSettings = settings.Element("SQLSettings");

            string connectionString =
                "Data Source=" + sqlSettings.Element("ServerName").Value + ";" +
                "Initial Catalog=" + sqlSettings.Element("Database").Value + ";" +
                "User ID=" + sqlSettings.Element("UserID").Value + ";" +
                "Password=" + sqlSettings.Element("Password").Value;

            return new SqlConnection(connectionString);

        }

        private static SqlCommand prepareCommand()
        {
            SqlCommand command = new SqlCommand();
            command.Connection = CreateConnection();

            //try
            //{
            command.Connection.Open();
            //}
            //catch (Exception ex)
            //{
            //}
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
