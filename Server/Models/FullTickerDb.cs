using System;
using System.ComponentModel.DataAnnotations;

namespace APBD_PRO.Server.Models
{
	public class FullTickerDb
	{
        [Key]
        public string ticker { get; set; }
        public string name { get; set; }
        public string locale { get; set; }
        public string primary_exchange { get; set; }
        public string description { get; set; }
        public string homepage_url { get; set; }
        public int total_employees { get; set; }
        public string phone_number { get; set; }
        public string list_date { get; set; }

        public string icon_url { get; set; }
        public string logo_url { get; set; }
        public string address1 { get; set; }
        public string city { get; set; }
        public string postal_code { get; set; }
        public string state { get; set; }

        public virtual IEnumerable<ChartDataDb> chartDatas { get; set; }
        public virtual IEnumerable<TickerInNewsDb> tickerInNews { get; set; }
        public virtual IEnumerable<WatchlistDb> Watchlists { get; set; }


    }
}

