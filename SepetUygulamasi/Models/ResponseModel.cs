using System;

namespace SepetUygulamasi.Models
{
    public class ResponseModel
    {
        public string message { get; set; }
        public object data { get; set; }
        public bool success { get; set; }
        public DateTime responseTime { get; set; } = DateTime.Now;
    }
}