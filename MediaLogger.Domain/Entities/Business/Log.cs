using MediaLogger.Domain.Enumerables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLogger.Domain.Entities.Business
{
    public class Log
    {
        public long ID { get; set; }
        public int ID_PAYPAD { get; set; }
        public string? LOG_TYPE { get; set; }
        public string? PAYPAD { get; set; }
        public string? LOG { get; set; }
        public DateTime DATE_CREATED { get; set; }
    }
}
