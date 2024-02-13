using MediaLogger.Domain.Enumerables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLogger.Domain.DTOs.Business
{
    public class ReqLog
    {
        public ETypeLogReq Logtype { get; set; }
        public string Content {  get; set; } = string.Empty;
    }
}
