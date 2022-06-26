using System;
using APBD_PRO.Shared;

namespace APBD_PRO.Server.Models
{
	public class TickerDetailsResponse
	{
        public FullTicker results { get; set; }
        public string status { get; set; }
    }

}

