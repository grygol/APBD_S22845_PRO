using System;
using APBD_PRO.Server.Data;
using APBD_PRO.Server.Models;
using APBD_PRO.Shared;
using Microsoft.EntityFrameworkCore;

namespace APBD_PRO.Server.Services
{
	public class ChartDataService : IChartDataService
	{
		private readonly ApplicationDbContext _context;

		public ChartDataService(ApplicationDbContext context)
		{
			_context = context;
		}

        public async Task CreateChartData(IEnumerable<ChartData> chartData, string ticker)
        {
            foreach (ChartData c in chartData) await CreateChartData(c, ticker);
        }

        public async Task CreateChartData(ChartData chartData, string ticker)
        {
            if (await _context.ChartDatas.Where(e => e.ticker == ticker && e.date == chartData.date).CountAsync() > 0) return;

            var newChartData = new ChartDataDb()
            {
                ticker = ticker,
                date = chartData.date ?? new DateTime(),
                open = chartData.open,
                low = chartData.low,
                close = chartData.close,
                high = chartData.high,
                volume = chartData.volume
            };

            await _context.AddAsync(newChartData);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ChartData>> GetChartData(string ticker)
        {
           return await _context.ChartDatas.Where(e => e.ticker == ticker).Select(e => new ChartData
            {
                date = e.date,
                open = e.open,
                low = e.low,
                close = e.close,
                high = e.high,
                volume = e.volume
            }).ToListAsync();
        }
    }
}

