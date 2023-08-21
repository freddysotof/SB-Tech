namespace SB.WebApi.Configurations
{
    public class AuthSettings
    {
        public string JwtSecret { get; set; }
        public string GoogleClientId { get; set; }
        public string GoogleClientSecret { get; set; }
        public string JwtEmailEncryption { get; set; }
    }
}
