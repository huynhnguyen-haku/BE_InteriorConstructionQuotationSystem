namespace SWP391API.DTO
{
    public class ErrorDTO
    {
        public string Message { get; set; }

        public ErrorDTO(string message) {
           Message = message;
        }
    }
}
