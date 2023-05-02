using System.ComponentModel.DataAnnotations;
namespace CandidateSoW.Models
{
    public class RegionModel 
    { 
        public int RegionId { get; set; }
        public string Region { get; set; } = "";
        public string Type { get; set; } = "";
    }
}
