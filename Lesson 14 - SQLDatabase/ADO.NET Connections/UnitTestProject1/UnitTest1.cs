using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string owner = null;
            List<string> model = new List<string>();
            string connectionString = "Data Source=PANFILENOKA\\SQLEXPRESS;Initial Catalog=Webinar2017;User ID=default;Password=default";

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand command = new SqlCommand("select * from Cat where owner = @owner", connection);
            command.Parameters.AddWithValue("@owner", owner);
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                model.Add((string)dataReader["Name"]);
            }

            dataReader.Close();
            connection.Close();
        }
    }
}
