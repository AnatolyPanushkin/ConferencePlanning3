using ConferencePlanning.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace ConferencePlanning.Data;

public class ConferencePlanningContext:IdentityDbContext<ApplicationUser>
{
    private readonly IConfiguration _configuration;
    public ConferencePlanningContext(DbContextOptions<ConferencePlanningContext> options,IConfiguration configuration):base(options)
    {
        _configuration = configuration;
    }
    
    public DbSet<Conference> Conferences { get; set; }
    
    public DbSet<Photo> Photos { get; set; }

    public DbSet<UsersConferences> UsersConferences { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("ConferencePlanningContext"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        /*modelBuilder.HasPostgresExtension("adminpack")
            .HasAnnotation("Relational:Collation", "Russian_Russia.1251");*/

        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Conference>(entity => { entity.HasKey(conference => new {conference.Id}); });
        modelBuilder.Entity<IdentityUserRole<Guid>>().HasKey(p => new { p.UserId, p.RoleId });
        modelBuilder.Entity<ApplicationUser>(apUser =>
        {
            apUser.HasMany(user => user.Conferences)
                .WithMany(conf => conf.Users)
                .UsingEntity<UsersConferences>(
                    conf => conf
                        .HasOne(uc => uc.Conference)
                        .WithMany(c => c.UsersConferences)
                        .HasForeignKey(uc => uc.ConferenceId),
                    us => us
                        .HasOne(uc => uc.User)
                        .WithMany(u => u.UsersConferences)
                        .HasForeignKey(uc => uc.UserId)
                );
        });
    }
}

