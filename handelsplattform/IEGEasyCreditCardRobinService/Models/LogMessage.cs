namespace LoggingService.Models
{
    public class LogMessage
    {
        public string Message { get; set; }

        public LogMessage(string message)
        {
            this.Message = message;
        }
    }
}
