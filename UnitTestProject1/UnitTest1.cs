using System;
using NUnit.Framework;
using Manager;
using DataAccess;
using System.Net.Mail;
using System.Xml.Linq;

namespace UnitTestProject1
{
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

            dfdlstring abc = "" + _GeneralFunctions.ExecuteScalar("Select Top 1 EmployeeID From Employee");

            var xyz = ActMgr.GetEmployee();

            Assert.That(xyz, Is.Null);
            Assert.That(xyz, Is.Not.EqualTo(abc));

        }

        [Test]
        public void TestMethod2()
        {
            Assert.That(1 == 2, Is.False);
        }

        [Test]
        public void GetEmpNameTest()
        {

            string abc = "" + _GeneralFunctions.ExecuteScalar("Select Top 1 EmployeeID From Employee");

            var xyz = ActMgr.GetEmployee();


            Assert.That(xyz, Is.Not.Null);
            Assert.That(xyz, Is.EqualTo(abc));

        }
    }

}
