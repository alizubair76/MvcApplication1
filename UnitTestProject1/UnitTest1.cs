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
            string abc = "" + DataAccess.DataAccess.ExecuteScalar("Select Top 1 EmployeeID From Employee");

            var xyz = ActMgr.GetEmployee();


            Assert.That(xyz, Is.Not.Null);
            Assert.That(xyz, Is.EqualTo(abc));

        }
    }
}
