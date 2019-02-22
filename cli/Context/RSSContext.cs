using Microsoft.EntityFrameworkCore;
using RSSRepository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RSSRepository.Context
{
    public class RSSContext : DbContext
    {
        public DbSet<RSS> RSSes { get; set; }
        public DbSet<RSSChanel> Chanels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //config storage
            optionsBuilder.UseSqlServer(@"Server=(LocalDB)\MSSQLLocalDB;Initial Catalog=rss_db;Integrated Security=False;");

            base.OnConfiguring(optionsBuilder);
        }
    }
}
