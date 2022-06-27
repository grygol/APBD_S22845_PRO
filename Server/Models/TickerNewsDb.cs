using System;
using System.ComponentModel.DataAnnotations;

namespace APBD_PRO.Server.Models
{
    public class TickerNewsDb
    {
        [Key]
        public string news_id { get; set; }
        public string title { get; set; }
        public string author { get; set; }
        public string article_url { get; set; }
        public string image_url { get; set; }
        public string description { get; set; }

        public virtual IEnumerable<TickerInNewsDb> tickerInNews { get; set; }
    }
}

