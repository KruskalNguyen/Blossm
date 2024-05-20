namespace BlossmMudBlazor.DTO
{
    public class OnlineUser
    {
        public string idUser { get; set; }
        public string email { get; set; }
        public int idBranch { get; set; }
        public bool isBranchManager { get; set; }
        public string idClient { get; set; }
        public string fullName { get; set; }
        public DateTime startTime { get; set; }
    }
}
