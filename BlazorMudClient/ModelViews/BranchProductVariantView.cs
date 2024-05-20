namespace BlossmAPI.ModelViews
{
    public class BranchProductVariantView
    {
        public int IdBranch { get; set; }

        public int IdProductVariant { get; set; }

        public List<int> IdProductVariants { get; set; } = new List<int>();

        public int? InventoryQuantity { get; set; }
    }
}
