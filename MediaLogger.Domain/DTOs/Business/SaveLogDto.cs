using MediaLogger.Domain.Enumerables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLogger.Domain.DTOs.Business
{
    public class SaveLogDto
    {
        public string idLog {  get; set; } = string.Empty;
        public string Logtype { get; set; } = string.Empty;
        public string Content {  get; set; } = string.Empty;
    }
}
