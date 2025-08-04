using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechDataInput.Data.DataClasses
{
    public class ClientLogEntry
    {
        public ClientLogEntry() { }
        public ClientLogEntry(string level, string message)
        {
            Timestamp = DateTime.Now;
            Level = level;
            Message = message;
        }
        public ClientLogEntry(string message, Exception? exception = null)
        {
            Timestamp = DateTime.Now;
            Level = "Error";
            Message = $"{message} {exception?.Message ?? ""}";
            StackTrace = exception?.StackTrace ?? "";
            Source = exception?.Source ?? "";
        }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string Level { get; set; } = "Error"; // можно "Info", "Warning", "Error"
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string Source { get; set; }
    }
}
