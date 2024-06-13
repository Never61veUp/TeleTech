using Microsoft.VisualStudio.TestTools.UnitTesting;
using TeleTech.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TeleTech.Model;

namespace TeleTech.Commands.Tests
{
    [TestClass()]
    public class GetAgreementCommandTests
    {

        GetAgreementCommand get = new GetAgreementCommand();
        [TestMethod()]
        public void ExecuteTest()
        {
            var expected = "";
            var actual = get.GetLastName(21);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void ExecuteTest1()
        {
            var expected = "Ильинский";
            var actual = get.GetLastName(121240);
            Assert.AreEqual(expected, actual);
        }
    }
}