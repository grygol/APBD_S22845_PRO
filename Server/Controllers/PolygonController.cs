using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APBD_PRO.Server.Services;
using APBD_PRO.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APBD_PRO.Server.Controllers
{
    [Route("api/[controller]")]
    public class PolygonController : ControllerBase
    {
        private readonly IPolygonService _service;

        public PolygonController(IPolygonService service)
        {
            _service = service;
        }


        // GET: api/values
        [Route("[action]")]
        [HttpGet]
        public async Task<IEnumerable<BasicTicker>> Get()
        {
            return await _service.GetBasicTickers("AAPL");
        }

        // GET: api/values
        [Route("[action]")]
        [HttpGet]
        public async Task<IEnumerable<BasicTicker>> GetBasicTickers()
        {
            string filterParam = HttpContext.Request.Query["$filter"].ToString();
            string filter = filterParam.Substring(filterParam.IndexOf("'") + 1, filterParam.LastIndexOf("'") - filterParam.IndexOf("'") - 1);

            Console.WriteLine("FILTER: " + filter);

            return await _service.GetBasicTickers(filter);
        }

        [Route("[action]/{ticker}")]
        [HttpGet]
        public async Task<FullTicker> GetFullTicker(string ticker)
        {
            return await _service.GetFullTicker(ticker);
        }

        [Route("[action]/{ticker}")]
        [HttpGet]
        public async Task<IQueryable<ChartData>> GetChartData(string ticker)
        {
            string past3M = DateTime.Now.AddMonths(-3).ToString("yyyy-MM-dd");
            string now = DateTime.Now.ToString("yyyy-MM-dd");

            var res = await  _service.GetChartData(ticker, past3M, now);
            return res.AsQueryable();
        }


        //// GET api/values/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}

