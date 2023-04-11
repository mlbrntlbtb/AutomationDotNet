using Datacom.TestAutomation.Common;
using FluentAssertions;

namespace Datacom.TestAutomation.UnitTests.Utilities
{
    [TestClass]
    public class DateUtilitiesTests
    {
        [TestMethod]
        public void TestGeCurrentDate()
        {
            var date = DateUtilities.GetCurrentDateTime("yyyy-MM-dd'T'HH:mm:ss.fff");
            var validDate = DateTime.TryParse(date, out DateTime parsedDate);

            if (validDate)
            {
                date.Should().BeOfType(typeof(string))
                    .And.Be(parsedDate.ToString("yyyy-MM-dd'T'HH:mm:ss.fff"));
            }
            else
            {
                validDate.Should().BeTrue("Date is valid");
            }
        }
    }
}