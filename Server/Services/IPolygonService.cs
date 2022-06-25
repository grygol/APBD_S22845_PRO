using System;
using APBD_PRO.Server.Models;
using APBD_PRO.Shared;

namespace APBD_PRO.Server.Services
{
	public interface IPolygonService
	{
		public Task<Aggregates> GetAggregates(string stocksTicker);
		//public Task<Tickers> GetBasicTickers(string ticker);
		public Task<IEnumerable<BasicTicker>> GetBasicTickers(string ticker);
	}
}

