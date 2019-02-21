using Microsoft.EntityFrameworkCore;
using web.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace web.Context
{
    public class RSSContext : DbContext
    {
        public RSSContext(DbContextOptions<RSSContext> opt) : base(opt)
        {
        }

        public DbSet<RSS> RSSes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
