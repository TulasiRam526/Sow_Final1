namespace CandidateSoW.Models
{
    public class ChangePasswordModel
    {
        public string LoginName { get; set; } = string.Empty;     
        public string OldPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}
