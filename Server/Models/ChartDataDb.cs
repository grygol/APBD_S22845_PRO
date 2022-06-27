using System;
namespace APBD_PRO.Shared
{
    public class ChartData
    {
        public string ticker { get; set; }
        public DateTime date { get; set; }
        public Double open { get; set; }
        public Double low { get; set; }
        public Double close { get; set; }
        public Double high { get; set; }
        public Double volume { get; set; }

        public virtual FullTicker fullTicker { get; set; }

    }
}

