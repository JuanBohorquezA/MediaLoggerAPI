using MediaLogger.Domain.Enumerables;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLogger.Domain.DTOs.Business
{
    public class SaveVideosDto
    {
        public ETypeLogReq Logtype { get; set; }
    }
}
