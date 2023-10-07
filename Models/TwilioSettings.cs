using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TDM.Models
{
    public class TwilioSettings
    {
            public string? AccountSid { get; set; }
        public string? AuthToken { get; set; }
        public string? WhatsAppFromNumber { get; set; }
    }
}