namespace BlossmAPI.ModelViews
{
    public class RoleView
    {
        public string Id { get; set; } = null!;

        public string? Name { get; set; }

        public string? NormalizedName { get; set; }

        public string? ConcurrencyStamp { get; set; }
    }
}
