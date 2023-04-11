namespace Tempo.TestAutomation.Model.DTOs.CustomerPortal.AddNewConsignment
{

    public class AddNewConsignmentDetails
    {
        public string? CustomerPortal_TargetPage { get; set; }
        public string? Tempo_TargetPage { get; set; }
        public ServiceDetails? ServiceDetails { get; set; }
        public DeliveryDetails? DeliveryDetails { get; set; }
        public AddItemDetails? AddItemDetails { get; set; }
        public string? FileName { get; set; }
        public string? FileName2 { get; set; }
        public string? FileExtension { get; set; }
        public FileContentDetails? FileContentDetails { get; set; }
        public int? OrderRowIndex { get; set; }
        public string? PrintMenu { get; set; }
    }
}
