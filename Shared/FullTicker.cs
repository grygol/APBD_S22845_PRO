using System;
namespace APBD_PRO.Shared
{
	public class FullTicker
	{
        public string ticker { get; set; }
        public string name { get; set; }
        public string locale { get; set; }
        public string primary_exchange { get; set; }
        public string description { get; set; }
        public string homepage_url { get; set; }
        public Dictionary<string, string> branding { get; set; }
        public Dictionary<string, string> address { get; set; }

    }
}

