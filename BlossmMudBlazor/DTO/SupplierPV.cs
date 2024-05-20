namespace BlossmMudBlazor.DTO
{
    public class SupplierPV
    {
        public int idSup { get; set; }
        public string nameSup { get; set; }
        public int rating { get; set; }
        public List<ListPV> listPV { get; set; }
    }
}
