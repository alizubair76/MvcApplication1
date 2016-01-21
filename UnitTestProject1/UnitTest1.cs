using System;
using NUnit.Framework;
using Manager;
using DataAccess;

namespace UnitTestProject1
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void TestMethod1()
        {
            zdsidsvsdovisstring abc = "" + DataAccess.DataAccess.ExecuteScalar("Select Top 1 EmployeeID From Employee");

            var xyz = ActMgr.GetEmployee();


            Assert.That(xyz, Is.Not.Null);
            Assert.That(xyz, Is.EqualTo(abc));

        }

        [Test]
        public void TestMethod2()
        {
            Assert.That(1 == 2, Is.False);
        }

        [Test]
        public void GetEmpNameTest()
        {

            string abc = "" + DataAccess.DataAccess.ExecuteScalar("Select Top 1 EmployeeID From Employee");

            var xyz = ActMgr.GetEmployee();


            Assert.That(xyz, Is.Not.Null);
            Assert.That(xyz, Is.EqualTo(abc));

        }
    }

}
