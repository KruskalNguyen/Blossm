namespace BlossmMudBlazor.DTO
{
    public class PurchaseRequest
    {
        public int id { get; set; }
        public DateTime createDate { get; set; }
        public DateTime deadline { get; set; }
        public string idRequester { get; set; }
        public string fullNameRequester { get; set; }
        public int idStatus { get; set; }
        public string status { get; set; }
        public int idPrioviry { get; set; }
        public string prioviry { get; set; }
        public string notes { get; set; }
        public DateTime? approvalDate { get; set; }
        public int totalAmount { get; set; }
        public int totalQuantity { get; set; }
        public string approverEmail { get; set; }
        public string? approverFirstName { get; set; }
        public string? approverLastName { get; set; }
        public string? approverPhoneNumner { get; set; }
        public int idBranch { get; set; }
        public string branchAdress { get; set; }
        public int idSupplier { get; set; }
        public string supplierName { get; set; }
        public List<SupplierContact> supplierContacts { get; set; }
        public List<ProductItem> productItems { get; set; }
    }
}
