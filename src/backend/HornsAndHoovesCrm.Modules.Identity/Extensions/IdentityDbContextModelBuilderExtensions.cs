using HornsAndHoovesCrm.Modules.Identity.Domain;
using HornsAndHoovesCrm.Modules.Identity.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace HornsAndHoovesCrm.Modules.Identity.Extensions;

public static class IdentityDbContextModelBuilderExtensions
{
    public static void ConfigureIdentity<TIdentityUser, TIdentityRole>(this ModelBuilder builder, string schema = "identity")
        where TIdentityUser : IdentityUser<TIdentityRole>
        where TIdentityRole : IdentityRole
    {
        builder.HasDefaultSchema(schema);
        
        builder.Entity<TIdentityUser>(b =>
        {

            b.ToTable("Users");

            //b.ConfigureByConvention();
            //b.ConfigureAbpUser();
            b.Property(x => x.UserName).IsRequired().HasMaxLength(IdentityUserConsts.MaxUserNameLength);
            b.Property(u => u.NormalizedUserName).IsRequired()
                .HasMaxLength(IdentityUserConsts.MaxNormalizedUserNameLength);
            b.Property(u => u.NormalizedEmail).IsRequired()
                .HasMaxLength(IdentityUserConsts.MaxNormalizedEmailLength);
            b.Property(u => u.PasswordHash).HasMaxLength(IdentityUserConsts.MaxPasswordHashLength);
            b.Property(u => u.SecurityStamp).IsRequired().HasMaxLength(IdentityUserConsts.MaxSecurityStampLength);
            //b.Property(u => u.TwoFactorEnabled).HasDefaultValue(false)
            //    .HasColumnName(nameof(IdentityUser.TwoFactorEnabled));
            b.Property(u => u.LockoutEnabled).HasDefaultValue(false);

            //b.Property(u => u.IsExternal).IsRequired().HasDefaultValue(false)
            //    .HasColumnName(nameof(IdentityUser.IsExternal));

            b.Property(u => u.AccessFailedCount);
            

            
            var claimsNavigation =
              b.Metadata.FindNavigation("Claims");


            claimsNavigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            var rolesNavigation =
              b.Metadata.FindNavigation("Roles");


            rolesNavigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            b.HasMany(u => u.Claims).WithOne().HasForeignKey(uc => uc.UserId).IsRequired();
            b.HasMany(u => u.Roles).WithOne().HasForeignKey(ur => ur.UserId).IsRequired();

            b.HasIndex(u => u.NormalizedUserName);
            b.HasIndex(u => u.NormalizedEmail);
            b.HasIndex(u => u.UserName);
            b.HasIndex(u => u.Email);

        });

        builder.Entity<IdentityUserClaim>(b =>
        {
            b.ToTable("UserClaims");

            //b.ConfigureByConvention();

            b.Property(x => x.Id).ValueGeneratedNever();

            b.Property(uc => uc.ClaimType).HasMaxLength(IdentityUserClaimConsts.MaxClaimTypeLength).IsRequired();
            b.Property(uc => uc.ClaimValue).HasMaxLength(IdentityUserClaimConsts.MaxClaimValueLength);

            b.HasIndex(uc => uc.UserId);

            //b.ApplyObjectExtensionMappings();
        });

        builder.Entity<IdentityUserRole<TIdentityRole>>(b =>
        {
            b.ToTable("UserRoles");

            //b.ConfigureByConvention();

            b.HasKey(ur => new { ur.UserId, ur.RoleId });

            b.HasOne(x => x.Role).WithMany().HasForeignKey(ur => ur.RoleId).IsRequired();
            b.HasOne<TIdentityUser>().WithMany(u => u.Roles).HasForeignKey(ur => ur.UserId).IsRequired();

            b.HasIndex(ur => new { ur.RoleId, ur.UserId });

            //b.ApplyObjectExtensionMappings();
        });


        builder.Entity<TIdentityRole>(b =>
        {
            b.ToTable("Roles");

            

            b.Property(r => r.Name).IsRequired().HasMaxLength(IdentityRoleConsts.MaxNameLength);
            b.Property(r => r.NormalizedName).IsRequired().HasMaxLength(IdentityRoleConsts.MaxNormalizedNameLength);

            b.HasMany(r => r.Claims).WithOne().HasForeignKey(rc => rc.RoleId).IsRequired();

            b.HasIndex(r => r.NormalizedName);

            var claimsNavigation =
              b.Metadata.FindNavigation("Claims");


            claimsNavigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        });

        builder.Entity<IdentityRoleClaim>(b =>
        {
            b.ToTable("RoleClaims");


            b.Property(x => x.Id).ValueGeneratedNever();

            b.Property(uc => uc.ClaimType).HasMaxLength(IdentityRoleClaimConsts.MaxClaimTypeLength).IsRequired();
            b.Property(uc => uc.ClaimValue).HasMaxLength(IdentityRoleClaimConsts.MaxClaimValueLength);

            b.HasIndex(uc => uc.RoleId);

        });
    }
}