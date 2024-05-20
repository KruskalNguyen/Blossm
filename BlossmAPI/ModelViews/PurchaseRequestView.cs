namespace BlossmAPI.ModelViews
{
    public class PurchaseRequestView
    {
        public DateTime? Deadline { get; set; }

        public int? Priority { get; set; }

        public string? Notes { get; set; }

        public int? IdBranch { get; set; }

        public int? IdSupplier { get; set; }

        public List<PurchaseRequestItemView> Items { get; set; }
    }
}
