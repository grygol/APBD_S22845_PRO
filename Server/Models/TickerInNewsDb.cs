using System;
using System.ComponentModel.DataAnnotations;

namespace APBD_PRO.Server.Models
{
    public class TickerInNewsDb
    {
        [Key]
        public string news_Id { get; set; }
        public string ticker { get; set; }

        public virtual TickerNewsDb tickerNews { get; set; }
        public virtual FullTickerDb fullTicker { get; set; }



    }
}

