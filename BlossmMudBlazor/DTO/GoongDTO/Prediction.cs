namespace BlazorMudClient.DTO.GoongDTO
{
    public class Prediction
    {
        public string description { get; set; }
        public List<object> matched_substrings { get; set; }
        public string place_id { get; set; }
        public string reference { get; set; }
        public StructuredFormatting structured_formatting { get; set; }
        public bool has_children { get; set; }
        public PlusCode plus_code { get; set; }
        public Compound compound { get; set; }
        public List<Term> terms { get; set; }
        public List<string> types { get; set; }
        public object distance_meters { get; set; }
    }
}
