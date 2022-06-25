using System;
namespace APBD_PRO.Server.Models
{
	public class Aggregates
	{
        public string ticker { get; set; }
        public Boolean adjusted { get; set; }
        public int queryCount { get; set; }
        public string request_id { get; set; }
        public int resultsCount { get; set; }
        public string status { get; set; }
        public List<Dictionary<String, double>> results;



    }
}

