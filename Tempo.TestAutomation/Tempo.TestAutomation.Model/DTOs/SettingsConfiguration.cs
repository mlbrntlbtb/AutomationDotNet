namespace Tempo.TestAutomation.Model.DTOs
{
    public class SettingsConfiguration
    {
        public string? ColumnName { get; set; }
        public string? RowText { get; set; }
        public string? CreateConsignmentOrderValue1 { get; set; }
        public string? CreateConsignmentOrderValue2 { get; set; }
        public string? MenuItem { get; set; }
        public string? ToastMessage { get; set; }
        public AuditHistory? auditHistory { get; set; }

        public class AuditHistory
        {
            public string? DateFormat { get; set; }
            public string? Activity { get; set; }
            public string? ConfigurationSetting { get; set; }
        }
    }
}