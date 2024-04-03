﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLogger.Domain.DTOs.Business
{
    public class LogDto
    {
        public long IdLog { get; set; }
        public int IdPaypad { get; set; }
        public string? LogType { get; set; }
        public string? Paypad { get; set; }
        public string? Log { get; set; }
        public DateTime DateCreated { get; set; }

    }
}
