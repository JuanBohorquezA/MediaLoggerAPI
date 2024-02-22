using MediaLogger.Domain.Enumerables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLogger.Domain.Entities.Business
{
    public class LogsMongo
    {
        public string? IP { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public ETypeLogApp typeLog { get; set; }

    }
}
