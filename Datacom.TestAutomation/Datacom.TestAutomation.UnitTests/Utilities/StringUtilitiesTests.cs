using Datacom.TestAutomation.Common;
using FluentAssertions;

namespace Datacom.TestAutomation.UnitTests.Utilities
{
    [TestClass]
    public class StringUtilitiesTests
    {
        [TestMethod]
        public void TestDecyrpXOR()
        {
            var message = "Mg4fB3VeKhhBQEsZMAw7".DecryptXOR("fkls00Ia302mhCihYZ8jew==");
            message.Should().Be("TestEncyrpytXOR");

        }

        [TestMethod]
        public void TestEncyrptXOR()
        {
            var message = "TestEncyrpytXOR".EncryptXOR("fkls00Ia302mhCihYZ8jew==");
            message.Should().Be("Mg4fB3VeKhhBQEsZMAw7");
        }
    }
}