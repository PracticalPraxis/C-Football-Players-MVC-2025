using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ArsenalPlayers.Models;

namespace ArsenalPlayers.Data
{
    public class ArsenalPlayersContext : DbContext
    {
        public ArsenalPlayersContext (DbContextOptions<ArsenalPlayersContext> options)
            : base(options)
        {
        }

        public DbSet<ArsenalPlayers.Models.Player> Player { get; set; } = default!;
    }
}
