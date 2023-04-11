namespace Tempo.TestAutomation.Model.DTOs
{
    public class RouteDetails
    {
        public Routedetails? RouteDetail { get; set; }

        public class Routedetails
        {
            public string? RouteName { get; set; }
        }

        public string[] JobStatuses { get; set; }
    }
}