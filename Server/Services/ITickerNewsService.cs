using System;
using APBD_PRO.Shared;

namespace APBD_PRO.Server.Services
{
	public interface ITickerNewsService
	{
		public Task<IEnumerable<TickerNews>> GetTickerNews(string ticker);
		public Task CreateTickerNews(IEnumerable<TickerNews> tickerNews, string ticker);
	}
}

