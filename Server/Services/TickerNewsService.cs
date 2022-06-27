using System;
using System.Transactions;
using APBD_PRO.Server.Data;
using APBD_PRO.Server.Models;
using APBD_PRO.Shared;
using Microsoft.EntityFrameworkCore;

namespace APBD_PRO.Server.Services
{
	public class TickerNewsService : ITickerNewsService
	{
        private readonly ApplicationDbContext _context;

		public TickerNewsService(ApplicationDbContext context)
		{
            _context = context;
		}

        public async Task CreateTickerNews(IEnumerable<TickerNews> tickerNews, string ticker)
        {
            foreach (TickerNews t in tickerNews)
            {
                Console.WriteLine("t.news_id: " + t.id);
                int tickerNewsCount = await _context.TickerNews.Where(e => e.news_id == t.id).CountAsync();
                int tickerInNewsCount = await _context.TickerInNews.Where(e => e.news_Id == t.id && e.ticker == ticker).CountAsync();

                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    try
                    {
                        if (tickerNewsCount == 0)
                        {
                            var newTickerNews = new TickerNewsDb()
                            {
                                news_id = t.id,
                                title = t.title,
                                author = t.author,
                                article_url = t.article_url,
                                image_url = t.image_url,
                                description = t.description
                            };

                            await _context.TickerNews.AddAsync(newTickerNews);
                            await _context.SaveChangesAsync();
                        }

                        if (tickerInNewsCount == 0)
                        {
                            var newTickerInNews = new TickerInNewsDb()
                            {
                                news_Id = t.id,
                                ticker = ticker
                            };
                            await _context.TickerInNews.AddAsync(newTickerInNews);
                        }

                        await _context.SaveChangesAsync();

                        scope.Complete();

                    }
                    catch (Exception) { }
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TickerNews>> GetTickerNews(string ticker)
        {
            return await _context.TickerInNews
                .Where(e => e.ticker == ticker)
                .Join(_context.TickerNews, t => t.news_Id, n => n.news_id, (t, n) =>
                 new TickerNews
                 {
                     title = n.title,
                     author = n.author,
                     article_url = n.article_url,
                     image_url = n.image_url,
                     description = n.description
                 }).ToListAsync();
        }
    }
}

