using BookAPI.Models;
using NUnit.Framework;

namespace UnitTesting
{
    public class Tests
    {
        public Login lg;
        [SetUp]
        public void Setup()
        {
            lg = new Login();
        }

        [Test]
        public void TestLogin()
        {
            string actualRes = lg.LoginId;
            string expectRes = "Nithesh";
            string actualRes1 = lg.password;
            string expectRes1 = "Nit";
            Assert.AreEqual(expectRes, actualRes);
            Assert.AreEqual(expectRes1, actualRes1);

        }
    }
}