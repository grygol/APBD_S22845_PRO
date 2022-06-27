using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APBD_PRO.Server.Services;
using APBD_PRO.Shared;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APBD_PRO.Server.Controllers
{
    [Route("api/[controller]")]
    public class TickersController : ControllerBase
    {
        private readonly ITickerService _service;

        public TickersController(ITickerService service)
        {
            _service = service;
        }

        [HttpGet("[action]/{ticker}")]
        public async Task<IActionResult> GetBasicTicker(string ticker)
        {
            var res =  await _service.GetBasicTicker(ticker);
            return res == null ? NotFound() : Ok(res);
        }

        [HttpGet("[action]/{ticker}")]
        public async Task<IActionResult> GetFullTicker(string ticker)
        {
            var res = await _service.GetFullTicker(ticker);
            return res == null ? NotFound() : Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFullTicker(FullTicker ticker)
        {
            await _service.CreateFullTicker(ticker);
            return Ok();
        }
    }
}

