using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace APBD_PRO.Server.Models
{
    public class WatchlistDb
    {
        [Key]
        public string user_id { get; set; }
        public string ticker { get; set; }

        public virtual FullTickerDb fullTicker { get; set; }
        public virtual ApplicationUser applicationUser { get; set; }
    }
}

