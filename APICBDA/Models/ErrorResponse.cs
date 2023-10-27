namespace APICBDA.Models
{
    public class ErrorResponse
    {
        public string StatusCode { get; private set; }
        public string Message { get; private set; }

        public ErrorResponse(string status, string mensagem)
        {
            Message = mensagem;
            StatusCode = status;
        }
    }
}
