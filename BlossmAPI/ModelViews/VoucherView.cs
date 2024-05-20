namespace BlossmAPI.ModelViews
{
    public class VoucherView
    {
        public string id { get; set; }
        public string? name { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public bool? isPercentage { get; set; }
        public int? amount { get; set; }
        public bool? active { get; set; }
        public int? limit { get; set; }
        public int? condition { get; set; }
    }
}
