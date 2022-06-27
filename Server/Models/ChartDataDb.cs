using System;
using System.ComponentModel.DataAnnotations;

namespace APBD_PRO.Server.Models
{
    public class ChartDataDb
    {
        [Key]
        public int chart_id { get; set; }
        public string ticker { get; set; }
        public DateTime date { get; set; }
        public Double? open { get; set; }
        public Double? low { get; set; }
        public Double? close { get; set; }
        public Double? high { get; set; }
        public Double? volume { get; set; }

        public virtual FullTickerDb fullTicker { get; set; }

    }
}

