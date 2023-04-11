using FluentAssertions;

namespace BNZ.TestAutomation.Sample.Tests
{
    [TestClass]
    public class NavigationTests : BaseTest
    {
        [TestMethod]
        public void NavigateToPayeesTest()
        {
            Logger!.LogInformation(Test!, "Navigate to Payees page");
            HomePage homePage = ComponentFactory!.GetComponent<HomePage>().Load();
            homePage.NavigateTo("Payees");

            PayeesPage payeesPage = ComponentFactory!.GetComponent<PayeesPage>();

            payeesPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Payees page is loaded");

            payeesPage.GetHeader().Should().Be("Payees");
            Logger!.LogPass(Test!, "Page header is 'Payees'");
        }
    }
}
