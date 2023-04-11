using Datacom.TestAutomation.Common;
using Tempo.TestAutomation.Model.DTOs.Dispatch;
using Tempo.TestAutomation.Model.Web.Components.Pages;

namespace Tempo.TestAutomation.Tests.Web.Tests
{
    public class TFS_Test_Case_1817 : BaseTest
    {
        /***************************************************************************************************
        * *************************  TFS Ticket number :  1817   ******************************************
        * Given: I'm on the Show/View Map page.
        * When: I change the @MapFilter
        * Then:  I can see the status of the drivers based on filter and event types (@expectedResults)
        *
        * Expected Results
        * Logged on - Show only logged on users based on their event type last actioned
        * Routes with vehicles selected - Shows users that have completed a Vechile selected
        * Route Display - Layout of the Route (Header, Sub-header, Route icon style)
        *************************************************************************************************/

        [Test]
        public void TFS_1817_ShowMap()
        {
            //Loading and Initialization of Data
            RouteMapData? RoutemapDetails = TestDataService.Instance.LoadFile<RouteMapData>("TC_1817_ShowMap");

            //Page Elements Initialization
            LoginPage loginPage = PageFactory!.GetComponent<LoginPage>().Load();
            HomePage homePage = PageFactory!.GetComponent<HomePage>();
            DispatchPage dispatchPage = PageFactory!.GetComponent<DispatchPage>();
            RouteMapPage routeMapPage = PageFactory!.GetComponent<RouteMapPage>();

            //Step 1. Navigation to Tempo Login Page
            //Expected Result: Tempo Login page is loaded
            //========================================================================
            Logger!.LogInformation(Test!, "Load Automation Dev Login Page");
            loginPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Login page was loaded");

            //Steps 2-3. Logging-in using Automation Generic Credentials
            //Expected Result: Username is displayed on the top right of the nav bar
            //========================================================================
            Logger!.LogInformation(Test!, "View data details: " + RoutemapDetails.ViewArea!);
            Logger!.LogInformation(Test!, "Logging In authorized user");
            loginPage.LoginUser(AdminUser!);
            homePage.IsLoaded.Should().BeTrue();
            string authorisedUser = homePage.GetAuthorisedUser();
            authorisedUser.Should().Be(AdminUser!.Username);
            Logger!.LogPass(Test!, $"Username '{authorisedUser}' should be '{AdminUser.Username}'");

            //Step 4. Navigation to the Dispatch Page
            //Expected Result: Dispatch page is loaded : Dispatch table and route list is displayed
            //========================================================================
            Logger!.LogInformation(Test!, "Navigation to Dispatch page");
            homePage.ClickTempoLinkByText("dispatch");
            dispatchPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Dispatch Page was Loaded");

            //Steps 5-6. Navigation to the Rooutes Map page
            //Expected Result: Routes Map page is loaded on a new window tab.
            //========================================================================
            Logger!.LogInformation(Test!, "Navigation to Routes Map page");
            dispatchPage.ClickRouteOptions();
            dispatchPage.ClickViewRouteMap();
            Logger!.LogPass(Test!, "Navigated to View Route Map page and opened a new window");

            //Step 7. Switch to Route Map window tab.
            //Expected Result: Routes Map page is now the active window tab.
            //========================================================================
            Logger!.LogInformation(Test!, "Switch active window to Routes Map page");
            routeMapPage.SwitchToRouteMapWindow();
            routeMapPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Successfully switched to new Route Map window tab", ScreenCaptureService!.CaptureScreenImage());

            //Steps 8-10. Setting of the LoggedOn Map Filter.
            //Expected Result: LoggedOn Map Filter was selected.
            //========================================================================
            Logger!.LogInformation(Test!, "Setting the LoggedOn map filter");
            routeMapPage.ClickShowRoutesCheckBox();
            routeMapPage.ExpandRouteFilterAccordion();
            routeMapPage.ClickLoggedOnCheckBox();
            Logger!.LogPass(Test!, "Verified that LoggedOn map filter was selected", ScreenCaptureService!.CaptureScreenImage());

            //Steps 11-14. Closing of Routes Tab and navigation back to Tempo Homepage
            //Expected Result: Routes Tab was closed and Tempo Homepage is now displayed
            //========================================================================
            Logger!.LogInformation(Test!, "Closing Routes Tab and navigating to Tempo Home page");
            routeMapPage.UntickShowRoutesCheckBox();
            routeMapPage.CloseRoutesTab();
            Logger!.LogPass(Test!, "Successfully closed Routes window and navigated back to previous window", ScreenCaptureService!.CaptureScreenImage());
            homePage.NavigateSidebarByText("Home");
            homePage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Tempo Homepage was displayed");

            //Step 15. Navigation to the Dispatch Page
            //Expected Result: Dispatch page is loaded : Dispatch table and route list is displayed
            //========================================================================
            Logger!.LogInformation(Test!, "Navigation to Dispatch page");
            homePage.ClickTempoLinkByText("dispatch");
            dispatchPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Dispatch Page was Loaded");

            //Steps 16-17. Navigation to the Rooutes Map page
            //Expected Result: Routes Map page is loaded on a new window tab.
            //========================================================================
            Logger!.LogInformation(Test!, "Navigation to Routes Map page");
            dispatchPage.ClickRouteOptions();
            dispatchPage.ClickViewRouteMap();
            Logger!.LogPass(Test!, "Navigated to View Route Map page and opened a new window");

            //Step 18. Switch to Route Map window tab.
            //Expected Result: Routes Map page is now the active window tab.
            //========================================================================
            Logger!.LogInformation(Test!, "Switch active window to Routes Map page");
            routeMapPage.SwitchToRouteMapWindow();
            routeMapPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Switched to new Route Map window tab", ScreenCaptureService!.CaptureScreenImage());

            //Steps 19. Setting of the Vehicle with Routes Map Filter.
            //Expected Result: Vehicle with Routes Map Filter was selected.
            //========================================================================
            Logger!.LogInformation(Test!, "Setting the Vehicle with Routes map filter");
            routeMapPage.ClickShowRoutesCheckBox();
            routeMapPage.ExpandRouteFilterAccordion();
            routeMapPage.ClickVehiclesWithRoutesCheckBox();
            Logger!.LogPass(Test!, "Verified that Vehicle with Routes map filter was selected", ScreenCaptureService!.CaptureScreenImage());

            //Steps 20-23. Closing of Routes Tab and navigation back to Tempo Homepage
            //Expected Result: Routes Tab was closed and Tempo Homepage is now displayed
            //========================================================================
            Logger!.LogInformation(Test!, "Closing Routes Tab and navigating to Tempo Home page");
            routeMapPage.UntickShowRoutesCheckBox();
            routeMapPage.CloseRoutesTab();
            Logger!.LogPass(Test!, "Closed Routes window and navigated back to previous window", ScreenCaptureService!.CaptureScreenImage());
            homePage.NavigateSidebarByText("Home");
            homePage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Tempo Homepage was displayed");

            //Step 24. Navigation to the Dispatch Page
            //Expected Result: Dispatch page is loaded : Dispatch table and route list is displayed
            //========================================================================
            Logger!.LogInformation(Test!, "Navigation to Dispatch page");
            homePage.ClickTempoLinkByText("dispatch");
            dispatchPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Dispatch Page was Loaded");

            //Steps 25-26. Navigation to the Rooutes Map page
            //Expected Result: Routes Map page is loaded on a new window tab.
            //========================================================================
            Logger!.LogInformation(Test!, "Navigation to Routes Map page");
            dispatchPage.ClickRouteOptions();
            dispatchPage.ClickViewRouteMap();
            Logger!.LogPass(Test!, "Navigated to View Route Map page and opened a new window");

            //Step 27. Switch to Route Map window tab.
            //Expected Result: Routes Map page is now the active window tab.
            //========================================================================
            Logger!.LogInformation(Test!, "Switch active window to Routes Map page");
            routeMapPage.SwitchToRouteMapWindow();
            routeMapPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Switched to new Route Map window tab", ScreenCaptureService!.CaptureScreenImage());

            //Steps 28-30. Setting of View Area Map Filter.
            //Expected Result: Location on the View Map Area Filter was selected.
            //========================================================================
            Logger!.LogInformation(Test!, "Setting the View Map filter");
            routeMapPage.ClickShowRoutesCheckBox();
            routeMapPage.ExpandViewAreaAccordion();
            routeMapPage.SelectViewArea(RoutemapDetails!);
            Logger!.LogPass(Test!, "Verified that View map filter was selected", ScreenCaptureService!.CaptureScreenImage());

            //Steps 31-34. Closing of Routes Tab and navigation back to Tempo Homepage
            //Expected Result: Routes Tab was closed and Tempo Homepage is now displayed
            //========================================================================
            Logger!.LogInformation(Test!, "Closing Routes Tab and navigating to Tempo Home page");
            routeMapPage.UntickShowRoutesCheckBox();
            routeMapPage.CloseRoutesTab();
            Logger!.LogPass(Test!, "Closed Routes window and navigated back to previous window", ScreenCaptureService!.CaptureScreenImage());
            homePage.NavigateSidebarByText("Home");
            homePage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Tempo Homepage was displayed");

            //Step 35. Navigation to the Dispatch Page
            //Expected Result: Dispatch page is loaded : Dispatch table and route list is displayed
            //========================================================================
            Logger!.LogInformation(Test!, "Navigation to Dispatch page");
            homePage.ClickTempoLinkByText("dispatch");
            dispatchPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Dispatch Page was Loaded");

            //Steps 36-37. Navigation to the Rooutes Map page
            //Expected Result: Routes Map page is loaded on a new window tab.
            //========================================================================
            Logger!.LogInformation(Test!, "Navigation to Routes Map page");
            dispatchPage.ClickRouteOptions();
            dispatchPage.ClickViewRouteMap();
            Logger!.LogPass(Test!, "Navigated to View Route Map page and opened a new window");

            //Step 38. Switch to Route Map window tab.
            //Expected Result: Routes Map page is now the active window tab.
            //========================================================================
            Logger!.LogInformation(Test!, "Switch active window to Routes Map page");
            routeMapPage.SwitchToRouteMapWindow();
            routeMapPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Switched to new Route Map window tab", ScreenCaptureService!.CaptureScreenImage());

            //Step 39-42. Verification of Route Display Menu and components.
            //Expected Result: Route Display Menu and components were displayed.
            //========================================================================
            Logger!.LogInformation(Test!, "Verifying Route Display menu and sub menu items.");
            routeMapPage.ClickShowRoutesCheckBox();
            routeMapPage.ExpandRouteDisplayAccordion();
            routeMapPage.VerifyRouteDisplaySideMenu();
            Logger!.LogPass(Test!, "Verified that Route Display Menu and Sub Menu items were displayed", ScreenCaptureService!.CaptureScreenImage());

            //Steps 43-44. Closing of Routes Tab and logout of user from tempo App
            ///Expected Result: Tempo Login page is loaded
            //========================================================================
            Logger!.LogInformation(Test!, "Logging out the current user");
            routeMapPage.UntickShowRoutesCheckBox();
            routeMapPage.CloseRoutesTab();
            Logger!.LogPass(Test!, "Closed Routes window and navigated back to previous window", ScreenCaptureService!.CaptureScreenImage());
            homePage.LogoutUser();
            loginPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Successfully logout authorized user : " + authorisedUser);
        }
    }
}