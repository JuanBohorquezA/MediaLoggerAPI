using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLogger.Domain.DTOs.Business
{
    public class GetLogDto
    {
        public int IdPaypad { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? FinalDate { get; set; }
    }
}
