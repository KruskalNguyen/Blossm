using System.Xml.Linq;

namespace BlossmMudBlazor.DTO
{
    public class PaymentMethod
    {
        public int id { get; set; }
        public string name { get; set; }

        public override string ToString() => name;
    }
}
