namespace BlossmMudBlazor.DTO
{
    public class ProductVariantDTO
    {
        public int id { get; set; }
        public int idProduct { get; set; }
        public int idUnit { get; set; }
        public int idColor { get; set; }
        public int idSize { get; set; }
        public int purchasePrice { get; set; }
        public object sellingPrice { get; set; }
        public object creationDate { get; set; }
        public object updateDate { get; set; }
        public object createBy { get; set; }
        public object publish { get; set; }
        public List<object> branchProductVariants { get; set; }
        public object createByNavigation { get; set; }
        public object idColorNavigation { get; set; }
        public object idProductNavigation { get; set; }
        public object idSizeNavigation { get; set; }
        public object idUnitNavigation { get; set; }
        public List<object> images { get; set; }
        public List<object> issueImages { get; set; }
        public List<object> orderItems { get; set; }
        public List<object> purchaseRequestItems { get; set; }
        public List<object> shoppingCarts { get; set; }
        public List<object> idSuppliers { get; set; }
    }
}
