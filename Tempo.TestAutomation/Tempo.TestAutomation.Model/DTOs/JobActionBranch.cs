namespace Tempo.TestAutomation.Model.DTOs
{
    public class JobActionBranch
    {
        public string? Customer { get; set; }
        public Jobdetails? JobDetails { get; set; }
        public string? Route { get; set; }
        public string? Service { get; set; }
        public string? SuspendAutoUpdates { get; set; }

        public class Jobdetails
        {
            public string? ActionBranch { get; set; }
            public string? JobType { get; set; }
            public string? ScheduledDateFormat { get; set; }
        }
    }
}