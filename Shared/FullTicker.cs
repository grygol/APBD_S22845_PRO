using System;

namespace APBD_PRO.Shared
{
	public class FullTicker
	{
        public string ticker { get; set; }
        public string name { get; set; }
        public string locale { get; set; }
        public string? primary_exchange { get; set; } = "(no data)";
        public string? description { get; set; } = "/Description is not avalible for this company/";
        public string? homepage_url { get; set; } = "(no data)";
        public int? total_employees { get; set; } = 0;
        public string? phone_number { get; set; } = "(no data)";
        public string? list_date { get; set; } = "(no data)";

        public Dictionary<string, string>? branding { get; set; } = new Dictionary<string, string>
        {
            {"logo_url", "" },
            {"icon_url", "" },
        };
        public Dictionary<string, string>? address { get; set; } = new Dictionary<string, string>
        {
            {"address1", "(no data)" },
            {"city", "(no data)" },
            {"postal_code", "(no data)" },
            {"state", "(no data)" }
        };

    }
}

