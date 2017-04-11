using System.Collections.Generic;

namespace LoggingService.Models
{
    public class LogResponse
    {
        public bool Success { get; set; }
        public IEnumerable<dynamic> Errors { get; set; }
    }
}