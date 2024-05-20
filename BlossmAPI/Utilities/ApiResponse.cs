namespace BlossmAPI.Utilities
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public string ErrorMessage { get; set; }

        public ApiResponse<T> FalseResult(string error)
        {
            Success = false;
            ErrorMessage = error;

            return this;
        }

        public ApiResponse<T> SuccessResult()
        {
            Success = true;
            return this;
        }
        public ApiResponse<T> SuccessResult(T data)
        {
            Data = data;
            Success = true;
            return this;
        }
    }
}
