namespace Rookies_EF.API.Dtos.ResponseDtos
{
    public class ApiReponse
    {
        public object Data { get; set; } = new { };

        public int StatusCode { get; set; } = StatusCodes.Status200OK;

        public string Message { get; set; } = string.Empty;
    }
}
