using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Csp.UnitTests
{
    [TestFixture]
    public class TestVariable
    {
        [Test]
        public void Test_Constructor()
        {
            var v = new Variable<int, int>(1, new List<int> {2, 3});

            Assert.AreEqual(1, v.UserObject);
            Assert.AreEqual(2, v.Domain.ToArray()[0]);
            Assert.AreEqual(3, v.Domain.ToArray()[1]);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_Constructor_Null_Domain()
        {
            new Variable<int, int>(1, null);
        }

        [Test]
        public void Test_Constructor_Null_UserObject()
        {
            Assert.Throws<ArgumentNullException>(() => new Variable<string, int>(null, new List<int>()));
        }

        [Test]
        public void Test_Domain_Always_Returns_Same_Result()
        {
            var v = new Variable<string, int>("hi", new List<int> { 2, 22 });

            var d1 = v.Domain;
            var d2 = v.Domain;

            Assert.AreSame(d1, d2);
        }
    }
}
