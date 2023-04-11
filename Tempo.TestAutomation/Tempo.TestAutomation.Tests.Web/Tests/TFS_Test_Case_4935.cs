using Microsoft.Extensions.Logging;
using Tempo.TestAutomation.Model.Web.Components.Pages;

namespace Tempo.TestAutomation.Tests.Web
{
    [TestFixture]
    public class TFS_Test_Case_4935 : BaseTest
    {
        /***************************************************************************************************
         * *************************TFS Ticket number :  4935    *******************************************
         * Test Case 4935: User can select page size for order screen
         *
         * Given: User login Ops Console and select Order ib Dashboard
         * When:  click on "items per page" to show available number of items (25) per page options and click
         *        a different number(100).
         * Then:  The orders page reload with 100 numbers of records.
         *
         *************************************************************************************************/

        [Test]
        public void TC_4935_ValidateOrdersPageItemsDropdown()
        {
            //Initialization of Pages
            //========================================================================
            LoginPage loginPage = PageFactory!.GetComponent<LoginPage>();
            HomePage homePage = PageFactory!.GetComponent<HomePage>();
            OrdersPage ordersPage = PageFactory!.GetComponent<OrdersPage>();

            //1. Load Tempo Login Page
            //Expected Result: Tempo Login page is loaded
            //========================================================================
            Logger!.LogInformation(Test!, "Load Automation Dev Login Page");
            loginPage.Load();
            loginPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Login page is loaded");

            //2. Log in using Automation Generic Credentials
            //Expected Result: Username is displayed on the top right of the nav bar
            //========================================================================
            loginPage.LoginUser(AdminUser!);
            homePage.IsLoaded.Should().BeTrue();
            string authorisedUser = homePage.GetAuthorisedUser();
            authorisedUser.Should().Be(AdminUser!.Username);
            Logger!.LogPass(Test!, $"Username '{authorisedUser}' should be '{AdminUser.Username}'");

            //3. Proceed to the orders Page
            //Expected Result: Orders page is loaded : Orders table is displayed
            //========================================================================
            homePage.ClickTempoLinkByText("orders");
            ordersPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Orders Page is Loaded");

            //4. Click Items Per Page dropdown button
            //Expected Result: Items Per Page menu should be displayed
            //========================================================================
            ordersPage.ClickItemsPerPage();
            ordersPage.IsDropDownDisplayed().Should().BeTrue();
            Logger!.LogPass(Test!, "Items Per Page button is clicked and menu is displayed", ScreenCaptureService!.CaptureScreenImage());

            //5. Select item '100' in Items Per Page dropdown
            //Expected Result: Orders table row count is changed to 100
            //========================================================================
            ordersPage.SelectItemsPerPageDropdownItem("100");
            ordersPage.IsOrdersRowCountConsistent(100);
            Logger!.LogPass(Test!, "Orders table row count is changed to 100", ScreenCaptureService!.CaptureScreenImage());

            //6. Refresh page to reset Orders table row count
            //Expected Result: Orders table row count is reset to 25
            //========================================================================
            homePage.RefreshPage();
            ordersPage.IsLoaded.Should().BeTrue();
            ordersPage.IsOrdersRowCountConsistent(25);
            Logger!.LogPass(Test!, "Orders table row count is reset to 25", ScreenCaptureService!.CaptureScreenImage());

            //7. Logout user from tempo App
            ///Expected Result: Tempo Login page is loaded
            //========================================================================
            homePage.LogoutUser();
            loginPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Successfully logout authorized user : " + authorisedUser);
        }
    }
}