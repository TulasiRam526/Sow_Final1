namespace CandidateSoW.Models
{
    public class SoWCandidateModel
    {
        public int SOW_CandidateId { get; set; }
        public int SowId { get; set; }
        public int CandidateId { get; set; }
        public int StatusId { get; set; } = 0;
        public string Type { get; set; } = "";
        public string SoName { get; set; } = "";
        public string CandidateName { get; set; } = "";
        public string StatusName { get; set; } = "";

    }
}
