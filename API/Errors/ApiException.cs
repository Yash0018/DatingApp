namespace API.Errors
{
    // This class will be used in out middleware and its used to determine what are we gonna send to client when catching an exception
    public class ApiException
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
        public ApiException(int statusCode, string message, string details)
        {
            StatusCode = statusCode;
            Message = message;
            Details = details;
        }

    }
}
