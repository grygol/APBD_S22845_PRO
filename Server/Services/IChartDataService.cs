using System;
using APBD_PRO.Shared;

namespace APBD_PRO.Server.Services
{
	public interface IChartDataService
	{

		public Task<IEnumerable<ChartData>> GetChartData(string ticker);
		public Task CreateChartData(IEnumerable<ChartData> chartData, string ticker);
		public Task CreateChartData(ChartData chartData, string ticker);

	}
}

