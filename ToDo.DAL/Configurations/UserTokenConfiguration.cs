using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDo.Domain.Entity;

namespace ToDo.DAL.Configurations;

public class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
{
    public void Configure(EntityTypeBuilder<UserToken> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.RefreshToken).IsRequired();
        builder.Property(x => x.RefreshTokenExpiryTime).IsRequired();

        builder.HasData(new List<UserToken>()
        {
            new UserToken()
            {
                Id = 1,
                RefreshToken = "sdf897g9sf87gsf(*$&@#Nsgdf",
                RefreshTokenExpiryTime = new DateTime(2025, 01, 01, 0, 0, 0, DateTimeKind.Utc),
                UserId = 1
            }
        });
    }
}