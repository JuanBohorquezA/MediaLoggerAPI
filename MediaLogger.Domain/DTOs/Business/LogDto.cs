using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLogger.Domain.DTOs.Business
{
    public class LogDto
    {
        public int IdPaypad { get; set; }
        public int LogType { get; set; }
        public string? Paypad { get; set; }
        public string? Log { get; set; }
        public DateTime DateCreated { get; set; }

    }
}
