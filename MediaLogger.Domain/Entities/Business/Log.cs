using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLogger.Domain.Entities.Business
{
    public class Log
    {
        public int ID { get; set; }
        public int ID_PAYPAD { get; set; }
        public int LOG_TYPE { get; set; }
        public string? PAYPAD { get; set; }
        public string? LOG { get; set; }
        public DateTime DATE_CREATED { get; set; }
    }
}
