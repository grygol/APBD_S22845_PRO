using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using APBD_PRO.Server.Models;
using APBD_PRO.Server.Services;
using APBD_PRO.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APBD_PRO.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class PolygonController : ControllerBase
    {
        private readonly IPolygonService _polygonService;
        private readonly ITickerService _tickerService;
        private readonly IChartDataService _chartDataService;
        private readonly ITickerNewsService _tickerNewsService;
        private readonly UserManager<ApplicationUser> _userManager;

        public PolygonController(IPolygonService polygonService, ITickerService tickerService, IChartDataService chartDataService, ITickerNewsService tickerNewsService, UserManager<ApplicationUser> userManager)
        {
            _polygonService = polygonService;
            _tickerService = tickerService;
            _chartDataService = chartDataService;
            _tickerNewsService = tickerNewsService;
            _userManager = userManager;
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            //ApplicationUser i = await _userManager.GetUserAsync(HttpContext.User);
            var i = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Ok("UserID: " + i);
        }

        // GET: api/values
        [Route("[action]")]
        [HttpGet]
        public async Task<IEnumerable<BasicTicker>> GetBasicTickers()
        {
            string filterParam = HttpContext.Request.Query["$filter"].ToString();
            string filter = filterParam.Substring(filterParam.IndexOf("'") + 1, filterParam.LastIndexOf("'") - filterParam.IndexOf("'") - 1);

            Console.WriteLine("FILTER: " + filter);

            var res = await _polygonService.GetBasicTickers(filter);
            if (res != null) return res;

            return await _tickerService.GetBasicTicker(filter);
            
        }

        [Route("[action]/{ticker}")]
        [HttpGet]
        public async Task<FullTicker> GetFullTicker(string ticker)
        {
            var res = await _polygonService.GetFullTicker(ticker);
            if (res != null)
            {
                await _tickerService.CreateFullTicker(res);
                return res;
            }
            else return await _tickerService.GetFullTicker(ticker);
        }

        [Route("[action]/{ticker}")]
        [HttpGet]
        public async Task<IQueryable<ChartData>> GetChartData(string ticker)
        {
            string past3M = DateTime.Now.AddMonths(-3).ToString("yyyy-MM-dd");
            string now = DateTime.Now.ToString("yyyy-MM-dd");

            var res = await  _polygonService.GetChartData(ticker, past3M, now);
            if (res != null)
            {
                await _chartDataService.CreateChartData(res, ticker); //Not Await?
                return res.AsQueryable();
            }
            else return (await _chartDataService.GetChartData(ticker)).AsQueryable();
        }

        [Route("[action]/{ticker}")]
        [HttpGet]
        public async Task<IQueryable<TickerNews>> GetNews(string ticker)
        {
            var res = await _polygonService.GetTickerNews(ticker);
            if (res != null)
            {
                await _tickerNewsService.CreateTickerNews(res, ticker);
                return res.AsQueryable();
            }
            else return (await _tickerNewsService.GetTickerNews(ticker)).AsQueryable();
        }
    }
}

