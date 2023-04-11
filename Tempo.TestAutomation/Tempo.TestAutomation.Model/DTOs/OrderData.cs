namespace Tempo.TestAutomation.Model.DTOs
{
    public class OrderData
    {
        public string? Customer { get; set; }
        public string? Service { get; set; }
        public string? Reference { get; set; }
        public string? OrderInternalNotes { get; set; }
        public string? OrderNumber { get; set; }
        public string? ColumnName { get; set; }
        public string? JobColumnName { get; set; }
        public Surcharge? surcharge { get; set; }

        public class Surcharge
        {
            public string? SurchargeName { get; set; }
            public string? Description { get; set; }
        }
    }
}