namespace BlossmAPI.Models.Geocode
{
    public class Result
    {
        public List<AddressComponent> address_components { get; set; }
        public string formatted_address { get; set; }
        public Geometry geometry { get; set; }
        public string place_id { get; set; }
        public string reference { get; set; }
        public PlusCode plus_code { get; set; }
        public Compound compound { get; set; }
        public List<string> types { get; set; }
        public string name { get; set; }
        public string address { get; set; }
    }
}
