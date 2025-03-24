using IntermediateLab_Backend.Application.Utils;
using IntermediateLab_Backend.Domain.Entities;
using IntermediateLab_Backend.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IntermediateLab_Backend.Infrastructure.Configs;

public class MemberConfig: IEntityTypeConfiguration<Member>
{
	public void Configure(EntityTypeBuilder<Member> builder)
	{
		builder.ToTable(table => table.HasCheckConstraint("CK_MEMBER_ELO", "Elo BETWEEN 0 AND 3000"));
		builder.Property(member => member.Username)
			   .IsRequired()
			   .HasMaxLength(100);
		builder.HasIndex(member => member.Username).IsUnique();
		builder.HasIndex(member => member.Email).IsUnique();
		builder.HasIndex(member => member.Salt).IsUnique();
		builder.HasData(new Member
						{
							Id = 1,
							Username = "Admin",
							Elo = 1500,
							Email = "test@gmail.com",
							Gender = GenderEnum.Male,
							Role = RoleEnum.Admin,
							Salt = new Guid(),
							BirthDate = new DateTime(1999, 7, 1),
							Password = PasswordUtils.HashPassword("1234", new Guid())
						});
	}
}