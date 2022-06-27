using System;
using APBD_PRO.Server.Data;
using APBD_PRO.Server.Models;
using APBD_PRO.Shared;
using Microsoft.EntityFrameworkCore;

namespace APBD_PRO.Server.Services
{
	public class WatchlistService : IWatchlistService
	{
		private readonly ApplicationDbContext _context;

		public WatchlistService(ApplicationDbContext context)
        {
			_context = context;
        }

        public async Task AddToWatchlist(string id, string ticker)
        {
            if (await _context.Watchlists.Where(e => e.user_id == id && e.ticker == ticker).CountAsync() > 0) return;

            await _context.Watchlists.AddAsync(new Models.WatchlistDb
            {
                user_id = id,
                ticker = ticker
            });

            await _context.SaveChangesAsync();

        }

        public async Task<IEnumerable<BasicTicker>> GetWatchlistedTickers(string id)
        {
            return await _context.Watchlists.Where(e => e.user_id == id)
                .Join(_context.FullTickers, w => w.ticker, e => e.ticker, (w, e) => new BasicTicker
                {
                    name = e.name,
                    ticker = e.ticker,
                    primary_exchange = e.primary_exchange
                }).ToListAsync();
        }

        public async Task RemoveFromWatchlist(string id, string ticker)
        {
            //var e = await _context.Watchlists.Where(e => e.user_id == id && ticker == e.ticker).FirstOrDefaultAsync();
            //if (e is null) return;

            WatchlistDb e = new WatchlistDb
            {
                user_id = id,
                ticker = ticker
            };

            _context.Watchlists.Attach(e);
            _context.Watchlists.Remove(e);
            await _context.SaveChangesAsync();
        }
    }
}

