namespace CandidateSoW.Models
{
    public class LocationModel
    {
        public int LocationId { get; set; }
        public string Location { get; set; } = "";
        public int RegionId { get; set; } = 0;
        public string Type { get; set; } = "";
    }
}
