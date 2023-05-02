using System.Text;

namespace CandidateSoW.Common
{
    public class EncryptData
    {
        public static string Encrypt(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException("Pwd is empty");
            var encrypted = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(encrypted);
        }
        
    }
}
