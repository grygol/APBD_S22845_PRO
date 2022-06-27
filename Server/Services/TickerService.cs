using System;
using APBD_PRO.Server.Data;
using APBD_PRO.Server.Models;
using APBD_PRO.Shared;
using Microsoft.EntityFrameworkCore;

namespace APBD_PRO.Server.Services
{
    public class TickerService : ITickerService
    {
        private readonly ApplicationDbContext _context;

        public TickerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<FullTicker?> GetFullTicker(string ticker)
        {
            return await _context.FullTickers.Where(e => e.ticker == ticker).Select(e => new FullTicker
            {
                ticker = e.ticker,
                name = e.name,
                locale = e.locale,
                primary_exchange = e.primary_exchange,
                description = e.description,
                homepage_url = e.homepage_url,
                total_employees = e.total_employees,
                phone_number = e.phone_number,
                list_date = e.list_date,
                branding = new Dictionary<string, string>()
                {
                    {"icon_url", e.icon_url },
                    {"logo_url", e.logo_url }
                },
                address = new Dictionary<string, string>()
                {
                    {"address1", e.address1 ?? "" },
                    {"city", e.city },
                    {"postal_code", e.postal_code },
                    {"state", e.state }
                }
            }).FirstOrDefaultAsync();
        }

        public async Task CreateFullTicker(FullTicker e)
        {
            if (await _context.FullTickers.Where(t => t.ticker == e.ticker).CountAsync() > 0) return;

            var fullTicker = new FullTickerDb()
            {
                ticker = e.ticker ?? "",
                name = e.name ?? "",
                locale = e.locale ?? "",
                primary_exchange = e.primary_exchange ?? "",
                description = e.description ?? "",
                homepage_url = e.homepage_url ?? "",
                total_employees = e.total_employees ?? 1,
                phone_number = e.phone_number ?? "",
                list_date = e.list_date ?? "",
                icon_url = e.branding is not null ? e.branding.ContainsKey("icon_url") ? e.branding["icon_url"] : "" : "",
                logo_url = e.branding is not null ? e.branding.ContainsKey("logo_url") ? e.branding["logo_url"] : "" : "",
                address1 = e.address is not null ? e.address.ContainsKey("address1") ? e.address["address1"] : "" : "",
                city = e.address is not null ? e.address.ContainsKey("city") ? e.address["city"] : "" : "",
                postal_code = e.address is not null ? e.address.ContainsKey("postal_code") ? e.address["postal_code"] : "" : "",
                state = e.address is not null ? e.address.ContainsKey("state") ? e.address["state"] : "" : ""
            };

            await _context.FullTickers.AddAsync(fullTicker);
            await _context.SaveChangesAsync();
        }

        public async Task<List<BasicTicker>> GetBasicTicker(string ticker)
        {
            return await _context.FullTickers.Where(e => e.ticker.Contains(ticker)).Select(e => new BasicTicker
            {
                ticker = e.ticker,
                name = e.name,
                primary_exchange = e.primary_exchange
            }).ToListAsync();
        }

    }
}

