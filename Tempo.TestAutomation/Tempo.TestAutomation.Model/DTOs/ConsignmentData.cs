namespace Tempo.TestAutomation.Model.DTOs
{
    public class ConsignmentData
    {
        public Consignmentdetails? ConsignmentDetails { get; set; }


        public class Consignmentdetails
        {
            public string? AssignToRoute { get; set; }
            public string? ConsignmentNotes { get; set; }
            public string? CreateJob { get; set; }
            public string? Customer { get; set; }
            public string? DeliveryAddress { get; set; }
            public string? DeliveryAddressLookup { get; set; }
            public string? Description { get; set; }
            public string? EquipmentType { get; set; }
            public string? PaperWorkRequired { get; set; }
            public string? Quantity { get; set; }
            public string? Reference { get; set; }
            public string? ScannedItems { get; set; }
            public string? Service { get; set; }
            public string? Volume { get; set; }
            public string? Weight { get; set; }
        }
    }
}