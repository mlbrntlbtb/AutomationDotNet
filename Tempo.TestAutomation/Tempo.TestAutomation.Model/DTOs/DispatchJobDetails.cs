namespace Tempo.TestAutomation.Model.DTOs
{
    public class DispatchJobDetails
    {
        public Deliverydetails? DeliveryDetails { get; set; }
        public Jobdetails? JobDetails { get; set; }
        public Pickupdetails? PickUpDetails { get; set; }
        public string? ToastMessage { get; set; }
        public string? FileName { get; set; }
        public string? FileExtension { get; set; }

        public class Deliverydetails
        {
            public string? Address { get; set; }
            public string? EmailAddress { get; set; }
        }

        public class Jobdetails
        {
            public string? ActionBranch { get; set; }
            public string? ColumnName { get; set; }
            public string? ItemCount { get; set; }
            public string? JobType { get; set; }
            public string? Reference { get; set; }
            public string? Route { get; set; }
            public int? RowToCheck { get; set; }
            public string? SpecialInstruction { get; set; }
        }

        public class Pickupdetails
        {
            public string? Address { get; set; }
            public string? EmailAddress { get; set; }
            public string Suburb { get; internal set; }
        }
    }
}