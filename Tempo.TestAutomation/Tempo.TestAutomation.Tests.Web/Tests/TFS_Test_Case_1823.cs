using Datacom.TestAutomation.Common;
using Microsoft.Extensions.Logging;
using Tempo.TestAutomation.Model.DTOs;
using Tempo.TestAutomation.Model.Web.Components.Modals;
using Tempo.TestAutomation.Model.Web.Components.Pages;

namespace Tempo.TestAutomation.Tests.Web
{
    [TestFixture]
    public class TFS_Test_Case_1823 : BaseTest
    {
        /***************************************************************************************************
        * *************************TFS Ticket number :  1823    *******************************************
        *
        * Given: I'm on the Customers page
        * When: I press @Action
        * Then: I can
        *
        * ActionResult
        * - NOTE: There is no column about filter inn the customer page.
        * - Checking SAVE Layout is when you made changes in the ascending/decending of the column.
        * - Clear Layout is when you made changes in the ascending/decending of the column.
        *
        *      //Expected Result//
        *
        * - Save Layout - Any changes in columns can be saved
        * - Clear Layout - Any changes in columns can be cleared
        * - Search for Customer - Returns Customers that has been searched upon
        * - Edit Customer - Opens Edit Customer dialog
        * - Add Customer - Opens Add New Customer dialog
        * - Customer Name link - Opens Edit Customer dialog
        * - Contact Email link - Opens Mail to send that person an email
        * - Address link - Opens Edit Location dialog
        **************************************************************************************************/

        [Test]
        public void TC_1823_ValidateCustomerPageFunctionalities()
        {
            //Initialization of Test Data
            CustomersDetails CustomerData = TestDataService.Instance.Load<CustomersDetails>("TC_1823_CustomerFunctions");

            //1. Load Tempo Login Page
            //Expected Result: Tempo Login page is loaded
            //========================================================================
            Logger!.LogInformation(Test!, "Load Automation Dev Login Page");
            LoginPage loginPage = PageFactory!.GetComponent<LoginPage>().Load();
            loginPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Login page is loaded");

            //2. Log in using Automation Generic Credentials
            //Expected Result: Username is displayed on the top right of the nav bar
            //========================================================================
            loginPage.LoginUser(AdminUser!);
            HomePage homePage = PageFactory!.GetComponent<HomePage>();
            homePage.IsLoaded.Should().BeTrue();
            string authorisedUser = homePage.GetAuthorisedUser();
            authorisedUser.Should().Be(AdminUser!.Username);
            Logger!.LogPass(Test!, $"Username '{authorisedUser}' should be '{AdminUser.Username}'");

            //3. Proceed to the customers Page
            //Expected Result: Customers page is loaded : Customer table is displayed
            //========================================================================
            homePage.ClickTempoLinkByText("customers");
            CustomersPage customersPage = PageFactory!.GetComponent<CustomersPage>();
            customersPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Customers Page is Loaded");

            //4. Click settings icon of Details column in Customer table
            //Expected Result: Settings menu of Details column is displayed
            //========================================================================
            customersPage.ClickDetailsColumnSettingsButton();
            customersPage.IsDropDownDisplayed().Should().BeTrue();
            Logger!.LogPass(Test!, "Settings button of Details column is clicked and menu is displayed");

            //5. Hover over Filter item of Details Settings menu in Customer table
            //Expected Result: Settings submenu is displayed
            //========================================================================
            customersPage.HoverFilterListItem();
            customersPage.IsSettingsDropDownSubmenuDisplayed().Should().BeTrue();
            Logger!.LogPass(Test!, "Settings submenu is displayed");

            //6. Enter customer name into Filter field
            //Expected Result: Filter field has customer name value
            //========================================================================
            string customer = customersPage.GetCustomer(CustomerData!);
            customersPage.SetFilterValue(customer);
            customersPage.IsFilterFieldPopulatedByText(customer).Should().BeTrue();
            Logger!.LogPass(Test!, "Filter field has customer name value");

            //7. Click Filter button
            //Expected Result: Filter has been applied to customer table
            //========================================================================
            customersPage.ClickFilterButton();
            customersPage.IsTableFiltered().Should().BeTrue();
            Logger!.LogPass(Test!, "Filter field has been applied to customer table");

            //8. Verify if customer table rows are populated only with customer name
            //Expected Result: Customer table has been verified to only contain customer name rows
            //========================================================================
            customersPage.IsTableFilteredByName(customer, "Details").Should().BeTrue();
            Logger!.LogPass(Test!, "Customer table has been verified to only show customer name rows");

            //9. Click Save Layout button
            //Expected Result: "Your configuration has been saved" notification is displayed
            //========================================================================
            customersPage.ClickSaveLayoutButton();
            customersPage.IsLayoutSavedMessageDisplayed().Should().BeTrue();
            Logger!.LogPass(Test!, "Layout saved notification is displayed", ScreenCaptureService!.CaptureScreenImage());

            //10. Use Sidebar menu to navigate to Home
            //Expected Result: Home screen is displayed
            //========================================================================
            homePage.NavigateSidebarByText("Home");
            homePage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Home screen is displayed");

            //11. Proceed to the customers Page
            //Expected Result: Customers page is loaded : Customer table is displayed
            //========================================================================
            homePage.ClickTempoLinkByText("customers");
            customersPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Customers Page is Loaded");

            //12. Verify if customer layout has been retained
            //Expected Result: Customer table has been verified to still only contain customer name rows
            //========================================================================
            customersPage.IsTableFilteredByName(customer, "Details").Should().BeTrue();
            Logger!.LogPass(Test!, "Customer table has been verified to only show customer name rows", ScreenCaptureService!.CaptureScreenImage());

            //13. Click Clear Layout button
            //Expected Result: "Your configuration has been cleared" notification is displayed
            //========================================================================
            customersPage.ClickClearLayoutButton();
            customersPage.IsLayoutClearedMessageDisplayed().Should().BeTrue();
            Logger!.LogPass(Test!, "Layout cleared notification is displayed", ScreenCaptureService!.CaptureScreenImage());

            //14. Verify if customer layout has been cleared
            //Expected Result: Customer table has been verified to contain all rows
            //========================================================================
            customersPage.IsTableFilteredByName(customer, "Details").Should().BeFalse();
            Logger!.LogPass(Test!, "Customer table has been verified to show all rows");

            //15. Use Sidebar menu to navigate to Home
            //Expected Result: Home screen is displayed
            //========================================================================
            homePage.NavigateSidebarByText("Home");
            homePage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Home screen is displayed", ScreenCaptureService!.CaptureScreenImage());

            //16. Proceed to the customers Page
            //Expected Result: Customers page is loaded : Customer table is displayed
            //========================================================================
            homePage.ClickTempoLinkByText("customers");
            customersPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Customers Page is Loaded");

            //17. Click Add Customer button
            //Expected Result: Add Customer modal window is displayed
            //========================================================================
            customersPage.ClickAddCustomerButton();
            AddNewCustomerModal addNewCustomerModal = PageFactory!.GetComponent<AddNewCustomerModal>();
            addNewCustomerModal.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Add Customer modal is displayed", ScreenCaptureService!.CaptureScreenImage());

            //18. Close Add Customer modal
            //Expected Result: Add Customer modal window is closed
            //========================================================================
            addNewCustomerModal.ClickCloseButton();
            addNewCustomerModal.IsModalPresent().Should().BeFalse();
            Logger!.LogPass(Test!, "Add Customer modal is closed");

            //19. Use Sidebar menu to navigate to Home
            //Expected Result: Home screen is displayed
            //========================================================================
            homePage.NavigateSidebarByText("Home");
            homePage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Home screen is displayed");

            //20. Proceed to the customers Page
            //Expected Result: Customers page is loaded : Customer table is displayed
            //========================================================================
            homePage.ClickTempoLinkByText("customers");
            customersPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Customers Page is Loaded");

            //21. Click Edit Customer button
            //Expected Result: Edit Customer modal window is displayed
            //========================================================================
            customersPage.ClickCustomerRow(0);
            customersPage.ClickEditCustomerButton();
            EditCustomerModal editCustomerModal = PageFactory!.GetComponent<EditCustomerModal>();
            editCustomerModal.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Edit Customer modal is displayed", ScreenCaptureService!.CaptureScreenImage());

            //22. Close Edit Customer modal
            //Expected Result: Edit Customer modal window is closed
            //========================================================================
            editCustomerModal.ClickCloseButton();
            editCustomerModal.IsModalPresent().Should().BeFalse();
            Logger!.LogPass(Test!, "Edit Customer modal is closed");

            //23. Use Sidebar menu to navigate to Home
            //Expected Result: Home screen is displayed
            //========================================================================
            homePage.NavigateSidebarByText("Home");
            homePage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Home screen is displayed");

            //24. Proceed to the customers Page
            //Expected Result: Customers page is loaded : Customer table is displayed
            //========================================================================
            homePage.ClickTempoLinkByText("customers");
            customersPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Customers Page is Loaded");

            //25. Click Default Location link of first row in customers table
            //Expected Result: Edit Location modal window is displayed
            //========================================================================
            customersPage.ClickCustomerRowLink("Default Location", 0);
            EditLocationModal editLocationModal = PageFactory!.GetComponent<EditLocationModal>();
            editLocationModal.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Edit Location modal is displayed", ScreenCaptureService!.CaptureScreenImage());

            //26. Close Edit Location modal
            //Expected Result: Edit Location modal window is closed
            //========================================================================
            editLocationModal.ClickCloseButton();
            editLocationModal.IsModalPresent().Should().BeFalse();
            Logger!.LogPass(Test!, "Edit Location modal is closed");

            //27. Logout user from tempo App
            ///Expected Result: Tempo Login page is loaded
            //========================================================================
            homePage.LogoutUser();
            loginPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Successfully logout authorized user : " + authorisedUser);
        }
    }
}