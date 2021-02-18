namespace UserRegistration.Models
{
    public class ConnectionModel
    {
        public bool YouTrack { get; set; } = false;
        public bool Gitlab { get; set; } = false;
        public bool AzureAD { get; set; } = false;
        public bool Teams { get; set; } = false;
    }
}
