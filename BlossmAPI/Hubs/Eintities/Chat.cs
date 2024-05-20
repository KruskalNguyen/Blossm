namespace BlossmAPI.Hubs.Eintities
{
    public class Chat
    {
        public string id_client { get; set; }
        public string message { get; set; }
        public string send_to { get; set; }
        public int? count { get; set; }
    }
}