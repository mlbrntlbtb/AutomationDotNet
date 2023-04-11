using Datacom.TestAutomation.Common;
using Microsoft.Extensions.Logging;
using Tempo.TestAutomation.Model.DTOs;
using Tempo.TestAutomation.Model.Web.Components.Modals;
using Tempo.TestAutomation.Model.Web.Components.Pages;

namespace Tempo.TestAutomation.Tests.Web
{
    [TestFixture]
    public class TFS_Test_Case_1821 : BaseTest
    {
        /***************************************************************************************************
        * *************************  TFS Ticket number :  1821   *******************************************
        * Given: I'm on the Orders page
        * When: I enter order number in the search box
        * Then: the order list will be filtered
        * And: the searched order is highlighted
        ***************************************************************************************************/

        [Test]
        public void TC_1821_SearchOrder()
        {
            //Initialization of Test Data
            //========================================================================
            OrderData OrderData = TestDataService.Instance.LoadFile<OrderData>("TC_1821_SearchOrder");
            //ConsignmentData? ConsignmentData = TestDataService.Instance.LoadFile<ConsignmentData>("TC_1795_AddConsignment");

            //Page Elements Initialization
            //========================================================================
            LoginPage loginPage = PageFactory!.GetComponent<LoginPage>().Load();
            HomePage homePage = PageFactory!.GetComponent<HomePage>();
            OrdersPage ordersPage = PageFactory!.GetComponent<OrdersPage>();
            EditOrdersPage editOrdersPage = PageFactory!.GetComponent<EditOrdersPage>();
            EditJobModal editJobModal = PageFactory!.GetComponent<EditJobModal>();
            ViewMapModal viewMapModal = PageFactory!.GetComponent<ViewMapModal>();

            //1. Load Tempo Login Page
            //Expected Result: Tempo Login page is loaded
            //========================================================================
            Logger!.LogInformation(Test!, "Load Automation Dev Login Page");
            loginPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Login page is loaded");

            //2. Log in using Automation Generic Credentials
            //Expected Result: Username is displayed on the top right of the nav bar
            //========================================================================
            Logger!.LogInformation(Test!, "Logging In authorized user");
            loginPage.LoginUser(AdminUser!);
            homePage.IsLoaded.Should().BeTrue();
            string authorisedUser = homePage.GetAuthorisedUser();
            authorisedUser.Should().Be(AdminUser!.Username);
            Logger!.LogPass(Test!, $"Username '{authorisedUser}' should be '{AdminUser.Username}'");

            //3. Proceed to the Order Page
            //Expected Result: Order page is loaded : Order table list is displayed
            //========================================================================
            Logger!.LogInformation(Test!, "Navigation to Orders page");
            homePage.ClickTempoLinkByText("orders");
            ordersPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Orders Page is Loaded");

            //4. Search for Order using Order Number
            //Expected Result: Order list will be filtered based on the given Order Number
            //========================================================================
            ordersPage.FillOrdersSearchTextBox(OrderData!);
            ordersPage.ClickSearchButton();
            Logger!.LogPass(Test!, "Order list is filtered based on order number search parameter", ScreenCaptureService!.CaptureScreenImage());

            //5. Navigate to the Edit Order Page
            //Expected Result: User will be redirected to Edit Order Page
            //========================================================================
            ordersPage.ClickOrderRow(OrderData!);
            Logger!.LogPass(Test!, "Searched order is highligted in the list", ScreenCaptureService!.CaptureScreenImage());
            ordersPage.ClickEditOrderButton();
            editOrdersPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Edit Order Page is loaded", ScreenCaptureService!.CaptureScreenImage());

            //6. Navigate to the Edit Job Modal
            //Expected Result: User will be redirected to Edit Job Modal Page
            //========================================================================
            editOrdersPage.ClickJobRow(OrderData!);
            editOrdersPage.ClickEditJobButton();
            editJobModal.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Edit Job Modal is loaded", ScreenCaptureService!.CaptureScreenImage());
            editJobModal.ClickCloseButton();
            Logger!.LogPass(Test!, "Edit Job Modal is closed");

            //7. Navigate to the Map Modal
            //Expected Result: User will be redirected to Edit Job Modal Page
            //========================================================================
            editOrdersPage.ClickMapButton();
            viewMapModal.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Map Modal is loaded", ScreenCaptureService!.CaptureScreenImage());
            viewMapModal.ClickCloseButton();
            Logger!.LogPass(Test!, "Map Modal is closed");

            //8. Logout user from tempo App
            ///Expected Result: Tempo Login page is loaded
            //========================================================================
            homePage.LogoutUser();
            loginPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Successfully logout authorized user : " + authorisedUser);
        }
    }
}