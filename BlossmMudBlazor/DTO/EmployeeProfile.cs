using BlossmAPI.Models;

namespace BlossmMudBlazor.DTO
{
    public class EmployeeProfile
    {
        public string idUser { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public int idBranch { get; set; }
        public string address { get; set; }
        public string name { get; set; }
        public string firstNameManager { get; set; }
        public string lastNameManager { get; set; }
        public string emailManager { get; set; }
        public string phoneManager { get; set; }
        public string BranchManager { get; set; }
        
        public List<AspNetRole> Roles { get; set; }
    }
}
