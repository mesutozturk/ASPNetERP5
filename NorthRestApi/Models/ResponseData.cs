using System;

namespace NorthRestApi.Models
{
    public class ResponseData
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public object Data { get; set; }
        public DateTime ResponseTime { get; set; } = DateTime.Now;
    }
}