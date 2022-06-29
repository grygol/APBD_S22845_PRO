using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APBD_PRO.Server.Services;
using APBD_PRO.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APBD_PRO.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class WatchlistsController : Controller
    {

        private readonly IWatchlistService _service;

        public WatchlistsController(IWatchlistService service)
        {
            _service = service;
        }

        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> Get()
        {
            var id = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var res = await _service.GetWatchlistedTickers(id);
            if (res != null) return Ok(res);
            return NotFound();
        }

        [HttpPost("{ticker}")]
        public async Task<IActionResult> AddToWatchlist(string ticker)
        {
            var id = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _service.AddToWatchlist(id, ticker);
            return Ok();
        }

        [HttpDelete("{ticker}")]
        public async Task<IActionResult> RemoveFromWatchlist(string ticker)
        {
            var id = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _service.RemoveFromWatchlist(id, ticker);
            return Ok();
            
        }

    }
}

