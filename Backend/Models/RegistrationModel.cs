namespace CandidateSoW.Models
{
    public class RegistrationModel
    {
        public string LoginName { get; set; } = "";
        public string LoginPassword { get; set; } = "";
        public int LoginId { get; set; } = 0;
        public string EmailId { get; set; } = "";
        public int RoleId { get; set; } = 0;        
        public string RoleName { get; set; } = "";
        public string Type { get; set; } = "";
        public int FailureAttempts { get; set; } = 0;

    }
}
