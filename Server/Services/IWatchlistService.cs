using System;
using APBD_PRO.Shared;

namespace APBD_PRO.Server.Services
{
	public interface IWatchlistService
	{
		public Task<IEnumerable<BasicTicker>> GetWatchlistedTickers(string id);
		public Task AddToWatchlist(string id, string ticker);
		public Task RemoveFromWatchlist(string id, string ticker);
	}
}

