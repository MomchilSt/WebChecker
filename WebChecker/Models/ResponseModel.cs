using System;

namespace WebChecker.Models
{
    public class ResponseModel
    {
        public string Url { get; set; }

        public bool IsAlive { get; set; }

        public string Content { get; set; }

        public int CookiesCount { get; set; }

        public long ContentLength { get; set; }

        public DateTime TimeOfCrash { get; set; }

        public string ErrorMsg { get; set; }
    }
}
