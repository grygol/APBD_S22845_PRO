using System;
using APBD_PRO.Shared;

namespace APBD_PRO.Server.Services
{
	public interface ITickerService
	{
		public Task<FullTicker?> GetFullTicker(string ticker);
		public Task<List<BasicTicker>> GetBasicTicker(string ticker);
		public Task CreateFullTicker(FullTicker e);
	}
}

