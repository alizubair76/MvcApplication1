using System;
using NUnit.Framework;
using Manager;
using DataAccess;
using System.Net.Mail;
using System.Xml.Linq;
using System.Data.Common;
using System.Data.OleDb;
using System.Data;
using System.IO;



namespace UnitTestProject1
{
    public class Readdatafromdb
    {
        public static OleDbConnection CreateConnection()
        {
            string m_ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;"
                                     + "Data Source=" + Directory.GetCurrentDirectory() + @"\Sample_Datafile2015.mdb;"
                                     + "Persist Security Info=True;"
                                     + "Jet OLEDB:Database Password=admin;";

            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = m_ConnectionString;
            return conn;

        }

        public static DataRowCollection ReadData(string qry)
        {
            DataSet ds = new DataSet();


            var conn = CreateConnection();

            using (conn)
            {
                conn.Open();

                OleDbCommand command = new OleDbCommand(qry, conn);

                OleDbDataAdapter reader = new OleDbDataAdapter(command);
                reader.Fill(ds);
            }

            return ds.Tables[0].Rows;
        }

    }


    [SetUpFixture]
    public class Init
    {
        [TearDown]
        public void fireEmial()
        {
            string settingPath = System.IO.Directory.GetCurrentDirectory() + @"\Settings\Settings.xml";
            XDocument settingXML = XDocument.Load(settingPath);

            XElement settings = settingXML.Element("Settings");

            XElement emailSettings = settings.Element("EmailSettings");

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(emailSettings.Element("SmtpServerAddress").Value);
            SmtpServer.Port = Convert.ToInt32(emailSettings.Element("SmptPort").Value);
            SmtpServer.Credentials =
                new System.Net.NetworkCredential(
                emailSettings.Element("From").Value,
                emailSettings.Element("Password").Value);
            SmtpServer.EnableSsl = true;

            mail.From = new MailAddress(emailSettings.Element("From").Value, emailSettings.Element("NameToDisplay").Value);
            mail.To.Add(emailSettings.Element("To").Value);
            mail.Body = "Hello World";
            SmtpServer.Send(mail);
        }

    }

    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void TestMethod1()
        {
            string abc = "" + _GeneralFunctions.ExecuteScalar("Select Top 1 EmployeeID From Employee");

            var xyz = ActMgr.GetEmployee();

            Assert.That(xyz, Is.Not.Null,"returne Null");
            Assert.That(xyz, Is.EqualTo(abc));

        }

        [Test]
        public void TestMethod2()
        {
            Assert.That(1 == 2, Is.True);
        }

        [Test]
        public void GetEmpNameTest()
        {
            string abc = "" + _GeneralFunctions.ExecuteScalar("Select Top 1 EmployeeID From Employee");

            var xyz = ActMgr.GetEmployee();


            Assert.That(xyz, Is.Not.Null);
            Assert.That(xyz, Is.EqualTo(abc));
        }

        [Test]
        public void ReadData()
        {
            DataRowCollection dbResult = Readdatafromdb.ReadData("select * from employee");
            Assert.That(dbResult, Is.Not.Null);
        }
    }


}
