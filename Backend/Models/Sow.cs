using System;

namespace CandidateSoW.Models
{
    public class Sow
    {
        public int SowId { get; set; } = 0;
        public string SoName { get; set; } = "";
        public string JRCode { get; set; } = "";
        public DateTime RequestCreationDate { get; set; } = DateTime.Now;
        public int AccountId { get; set; } = 0;
        public int TechnologyId { get; set; } = 0;
        public string Role { get; set; } = "";

        public int LocationId { get; set; } = 0;
        public int RegionId { get; set; } = 0;
        public int TargetOpenPositions { get; set; } = 0;
        public int PositionsTobeClosed { get; set; } = 0;
        public int USTPOCId { get; set; } = 0;
        public int RecruiterId { get; set; } = 0;
        public int USTTPMId { get; set; } = 0;
        public int DellManagerId { get; set; } = 0;
        public int StatusId { get; set; } = 0;
        public string Band { get; set; } = "";
        public string ProjectId { get; set; } = "";
        public string AccountManager { get; set; } = "";
        public string ExternalResource { get; set; } = "";
        public string InternalResource { get; set; } = "";
        public string Type { get; set; } = "";
        public string TechnologyName { get; set; } = "";
        public string AccountName { get; set; } = "";
        public string Location { get; set; } = "";
        public string Region { get; set; } = "";
        public string USTPOCName { get; set; } = "";
        public string RecruiterName { get; set; } = "";
        public string USTTPMName { get; set; } = "";
        public string DellManagerName { get; set; } = "";
        public string StatusName { get; set; } = "";

    }
    public class Sowdata
    {
        public IList<Sow>? SOW { get; set; }
    }
}
