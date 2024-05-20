namespace BlazorMudClient.DTO.GoongDTO
{
    public class AutoComplete
    {
        public List<Prediction> predictions { get; set; }
        public string execution_time { get; set; }
        public string status { get; set; }
    }
}
