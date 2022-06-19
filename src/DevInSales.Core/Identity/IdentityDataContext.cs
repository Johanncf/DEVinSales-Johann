using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DevInSales.Core.Data.Context
{
    public class IdentityDataContext : IdentityDbContext
    {
        public IdentityDataContext(DbContextOptions<IdentityDataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);  
            builder.Entity<IdentityRole>(entity =>
            {
                entity.HasData(new List<IdentityRole>()
                {
                    new IdentityRole("Administrador"),
                    new IdentityRole("Gerente"),
                    new IdentityRole("Usuario")
                });
            });
        }
    }
}