using LinkMe.Core.Entities;
using LinkMe.Data.Initializer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LinkMe.Data.Context
{
    public class LinkMeContext : IdentityDbContext<User>
    {
        public LinkMeContext(DbContextOptions<LinkMeContext> options)
            : base(options)
        {
        }

        public DbSet<Link> Links { get; set; }

        public DbSet<LinkClick> LinkClicks { get; set; }

        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Seed();

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
