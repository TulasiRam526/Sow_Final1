namespace CandidateSoW.Models
{
    public class LoginModel
    {
        public string LoginName { get; set; } = "";
        public string EmailId { get; set; } = "";
        public string RoleName { get; set; } = "";
        public string ScreenNames { get; set; } = "";
        public string Status { get; set; } = "";
        public string PermissionName { get; set; } = "";
        public int FailureAttempts { get; set; } = 0;

    }
}
