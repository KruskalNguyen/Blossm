namespace BlossmAPI.ModelViews
{
    public class AccessRoleView
    {
        public string Id_Role { get; set; }
        public ICollection<int> Id_Access { get; set; }
    }
}
