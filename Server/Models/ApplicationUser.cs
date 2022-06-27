using Microsoft.AspNetCore.Identity;

namespace APBD_PRO.Server.Models;

public class ApplicationUser : IdentityUser
{
    public virtual IEnumerable<WatchlistDb> watchlists { get; set; }
}

