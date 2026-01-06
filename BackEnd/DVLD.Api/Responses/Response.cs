using System;
using System.Collections.Generic;
using System.Text;

namespace DVLD.Api.Responses
{
    public class Response
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; }
        public object? Data { get; set; } = null;
    }
}
