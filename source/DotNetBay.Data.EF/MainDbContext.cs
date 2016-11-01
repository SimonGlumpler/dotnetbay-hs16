using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetBay.Data.Entity;

namespace DotNetBay.Data.EF
{
    class MainDbContext : DbContext
    {
        public MainDbContext() : base("DatabaseConnection")
        {
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Auction> Auctions { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Bid> Bids { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Auction>().HasRequired(a => a.Seller).WithMany(s => s.Auctions);
            modelBuilder.Entity<Auction>().HasMany(a => a.Bids).WithRequired(b => b.Auction);

            modelBuilder.Conventions.Add(new DateTime2Convention());
        }
    }
}
