using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanApplication.Application.Common.Models
{
    public class EmailApp
    {
        public string To { get; set; }
        public string From { get; set; }
        public string Password { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsBodyHtml { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }
}
