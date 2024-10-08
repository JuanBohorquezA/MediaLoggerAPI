﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLogger.Domain
{
    public static class ResponseMessage
    {
        public static string EMPTYFIELDS { get { return "Complete all the fields"; } }

        public static string UNAUTHORIZED = "UNAUTHORIZED";
        public static string BADREQUEST = "BAD REQUEST";
        public static string INTERNALSERVERERROR= "INTERNAL SERVER ERROR";
        public static string OK(string? Message)
        {
            return $"{Message} has been successfully";
        }
        public static string Error(string? Message)
        {
            return $"Error, {Message}. Check it and validate again.";
        }
        public static string UNAUTHORIZED_MESSAGE(string? Message) 
        {
            return $"You are not authorized, {Message}"; 
        }
        public static string INTERNALSERVERERROR_MESSAGE(string? Message)
        {
            return $"Error, {Message}";
        }

    }
}
