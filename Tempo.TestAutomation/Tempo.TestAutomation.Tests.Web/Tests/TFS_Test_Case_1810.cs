using Datacom.TestAutomation.Common;
using Tempo.TestAutomation.Model.DTOs;
using Tempo.TestAutomation.Model.Web.Components.Pages;

namespace Tempo.TestAutomation.Tests.Web.Tests
{
    [TestFixture]
    public class TFS_Test_Case_1810 : BaseTest
    {
        /***************************************************************************************************
        * *************************  TFS Ticket number :  1810   *******************************************
        * Given: I'm on the dispatch page in the Route section
        * When: I choose the Route filter to show @RouteFilter
        * Then: only the jobs with the Route Filter selected are shown
        *
        * Expected Result:
        * Display routes according to the following status filter
        *   - Dispatch
        *   - Accepted
        *   - Completed
        *************************************************************************************************/

        [Test]
        public void TC_1810_RouteFilter()
        {
            //00. Instantiations
            //Expected Result: Initialized All Page/Modal Components
            LoginPage loginPage = PageFactory!.GetComponent<LoginPage>().Load();
            HomePage homePage = PageFactory!.GetComponent<HomePage>().Load();
            DispatchPage dispatchPage = PageFactory!.GetComponent<DispatchPage>();
            RouteDetails searchRoute = TestDataService.Instance.LoadFile<RouteDetails>("TC_1810_TestData");

            //Step 1: Navigation to Tempo Login Page.
            //Expected Result: Tempo Login page is loaded.
            //========================================================================
            Logger!.LogInformation(Test!, "Load Automation Dev Login Page");
            loginPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Login page is loaded");

            //Steps 2-3: Logging-in using Automation Generic Credentials
            //Expected Result:
            //2. Username and Password field should be populated.
            //3. Username is displayed on the top right of the nav bar
            //========================================================================
            Logger!.LogInformation(Test!, "Logging In authorized user");
            loginPage.LoginUser(AdminUser!);
            homePage.IsLoaded.Should().BeTrue();
            string authorisedUser = homePage.GetAuthorisedUser();
            authorisedUser.Should().Be(AdminUser!.Username);
            Logger!.LogPass(Test!, $"Username '{authorisedUser}' should be '{AdminUser.Username}'");

            //Step 4: Navigation to the dispatch Page
            //Expected Result: Dispatch page is loaded.
            //========================================================================
            Logger!.LogInformation(Test!, "Navigation to Dispatch page");
            homePage.ClickTempoLinkByText("dispatch");
            dispatchPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Dispatch Page is Loaded");

            //Step 5-6: Find a Route. Enter <route> in Find a Route field search bar.
            //Expected Result: <Route> should be displayed.
            //========================================================================
            Logger!.LogInformation(Test!, "Entering <route> on Route search bar");
            string route = dispatchPage.GetRoute(searchRoute!);
            dispatchPage.SearchRoute(route);
            dispatchPage.IsRoutePresent(route).Should().BeTrue();
            Logger!.LogPass(Test!, "<Route> is displayed", ScreenCaptureService!.CaptureScreenImage());

            //Step 7: Click displayed item in the Routes List.
            //Expected Result: Searched item should be selected.
            //=========================================================================
            Logger!.LogInformation(Test!, "Navigation to selected Route.");
            dispatchPage.ClickRouteResult(route);
            Logger!.LogPass(Test!, "<Route> job item/s are displayed");

            //Step 8: Clicking Routes Filter Dropdown
            //Expected Result: Route Filter should be displayed.
            //=========================================================================
            Logger!.LogInformation(Test!, "Opening routes filter dropdown.");
            dispatchPage.ClickJobStatusFilterDropDown();
            dispatchPage.IsJobStatusDropDownDisplayed().Should().BeTrue();
            Logger!.LogPass(Test!, "Routes filter is displayed.");

            //Step 9: Deselecting the Job Statuses on the filter.
            //Expected Result: No items should be displayed on the list.
            //=========================================================================
            Logger!.LogInformation(Test!, "Unselecting the Job Statuses on the filter.");
            dispatchPage.UnselectAllJobStatus();
            dispatchPage.ClickJobStatusFilterDropDown();
            Logger!.LogPass(Test!, "All Job Status filter is unselected. No Job is displayed on the list.");

            //Step 10-13: Selecting Job Status on the Filter. Clear after validation.
            //Expected Result: Only <Job Status> Job item should be displayed. List should be cleared afterwards.
            //=========================================================================
            Logger!.LogInformation(Test!, "Clicking of 'Dispatched' filter");
            var jobstatuses = searchRoute.JobStatuses.ToList();
            foreach (var jobstatus in jobstatuses)
            {
                dispatchPage.ClickJobStatus(jobstatus);
                if (dispatchPage.CheckFilteredAvailableItemsByJobStatus(jobstatus))
                {
                    Logger!.LogPass(Test!, $"Filtered available items for status: {jobstatus} displayed.", ScreenCaptureService!.CaptureScreenImage());
                }
                else
                {
                    Logger!.LogPass(Test!, $"No available filtered items for status: {jobstatus} displayed.", ScreenCaptureService!.CaptureScreenImage());
                }
                dispatchPage.ClickJobStatus(jobstatus);
            }

            //Step 14: Logout user from tempo App
            //Expected Result: Tempo Login page is loaded
            //========================================================================
            dispatchPage.ClickJobStatus("Completed");
            Logger!.LogInformation(Test!, "Logging out the current user");
            homePage.LogoutUser();
            loginPage.IsLoaded.Should().BeTrue();
            Logger!.LogPass(Test!, "Successfully logout authorized user : " + authorisedUser);
        }
    }
}