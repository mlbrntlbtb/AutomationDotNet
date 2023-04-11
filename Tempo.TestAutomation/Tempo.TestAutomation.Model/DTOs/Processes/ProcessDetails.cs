namespace Tempo.TestAutomation.Model.DTOs.Processes
{
    public class ProcessDetails
    {
        public string? ProcessFilterTableBy { get; set; }
        public string? TableColumnHeadingSettings { get; set; }   
        public ConfigurationTabDetails? ConfigurationTabdetails { get; set; }

        public class ConfigurationTabDetails
        {
            public string? ProcessTitleHelpText { get; set; }

        }
    }
}