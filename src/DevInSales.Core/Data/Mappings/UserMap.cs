using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DevInSales.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace DevInSales.Core.Data.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.Property(u => u.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(u => u.BirthDate)
                .IsRequired();
        }
    }

}
