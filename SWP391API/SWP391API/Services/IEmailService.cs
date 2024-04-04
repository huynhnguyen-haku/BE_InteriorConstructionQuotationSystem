namespace SWP391API.Services
{
    public interface IEmailService
    {
        void sendTo(string to, string title, string body);

        void sendToWithPdfAttachment(string to, string title, string body, byte[] pdfContent, string pdfName);
    }
}
