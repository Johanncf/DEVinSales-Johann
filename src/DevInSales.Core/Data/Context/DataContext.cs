using System.Reflection;
using DevInSales.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DevInSales.Core.Data.Context
{
    public class DataContext : IdentityDbContext<
        User, 
        Role, 
        int, 
        IdentityUserClaim<int>,
        UserRole,
        IdentityUserLogin<int>,
        IdentityRoleClaim<int>,
        IdentityUserToken<int>
        >
    {
        public 
            DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            int userId = 1;
            int roleId = 1;
            var hasher = new PasswordHasher<User>();
            var user = new User(userId, "devinhouse@admin.com.br", "DEVinHouse", new DateTime(1980, 01, 01))
            {
                UserName = "devinhouse@admin.com.br",
                NormalizedUserName = "DEVINHOUSE@ADMIN.COM.BR",
                NormalizedEmail = "DEVINHOUSE@ADMIN.COM.BR",
                SecurityStamp = Guid.NewGuid().ToString("D")
            };
            user.PasswordHash = hasher.HashPassword(user, "Admin123*");
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            builder.Entity<Role>(entity =>
            {
                entity.ToTable("Roles");
                entity.HasData(new List<Role>()
                {
                    new Role()
                    {
                        Id = roleId, 
                        Name = Identity.Constants.Roles.Admin,
                        NormalizedName = Identity.Constants.Roles.Admin.ToUpper()
                    },
                    new Role()
                    {
                        Id = 2,
                        Name = Identity.Constants.Roles.Gerente,
                        NormalizedName = Identity.Constants.Roles.Gerente.ToUpper()
                    },
                    new Role()
                    {
                        Id = 3,
                        Name = Identity.Constants.Roles.Usuario,
                        NormalizedName = Identity.Constants.Roles.Usuario.ToUpper()
                    }
                });
            });

            builder.Entity<User>(entity =>
            {
                entity.HasData(new List<User>()
                {
                    user
                });
            });

            

            builder.Entity<UserRole>(entity =>
            {
                entity.ToTable("UserRoles");

                entity.HasKey(k => new { k.UserId, k.RoleId });

                entity.HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

                entity.HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

                entity.HasData(new List<UserRole>()
                {
                    new UserRole()
                    {
                        UserId = userId,
                        RoleId = roleId
                    }
                });
            });
            

            builder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins");
            builder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserToken<int>>().ToTable("UserToken");
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Address> Addresses{ get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<SaleProduct> SaleProducts { get; set; }
    }
}
        
  
