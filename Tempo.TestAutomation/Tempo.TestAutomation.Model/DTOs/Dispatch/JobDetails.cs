namespace Tempo.TestAutomation.Model
{
    public class JobDetails
    {
        public bool? CreateOrder { get; set; }
        public string? JobType { get; set; }
        public string? Route { get; set; }
        public string? Reference { get; set; }
        public int? ItemCount { get; set; }
        public string? ActionBranch { get; set; }
        public string? VehicleCategory { get; set; }
        public string? WeightClass { get; set; }
        public string? ScheduleDate { get; set; }
        public string? PaperWorkRequired { get; set; }
        public string? ScannedItems { get; set; }
        public string? PlanningPriority { get; set; }
        public bool? Urgent { get; set; }
        public string? SpecialInstructions { get; set; }
        public PickupJobTypeDetails? PickUpDetails { get; set; }
        public DeliveryJobTypeDetails? DeliveryDetails { get; set; }
        public object FileName { get; internal set; }
    }
}