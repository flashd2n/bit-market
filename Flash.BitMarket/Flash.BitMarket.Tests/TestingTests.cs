using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flash.BitMarket.Tests
{
    [TestFixture]
    public class TestingTests
    {
        [Test]
        public void TestingTestFunctionality()
        {
            var a = 42;
            var b = 72;

            Assert.AreNotEqual(a, b);
        }
    }
}
