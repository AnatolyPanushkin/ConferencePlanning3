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
    
    public DbSet<Section> Sections { get; set; }
    
    public DbSet<Questionnaire> Questionnaires { get; set; }
    public DbSet<UsersConferences> UsersConferences { get; set; }
    
    public DbSet<PotentialParticipants> PotentialParticipants { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("ConferencePlanningContext"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("adminpack")
            .HasAnnotation("Relational:Collation", "Russian_Russia.1251");

        base.OnModelCreating(modelBuilder);

        var conferences = new Conference[]
        {
            new(){Id = Guid.NewGuid(),Name = "Conference1", Type = "offline",ShortTopic = "Conference1", FullTopic = "Conference1",Date = DateOnly.FromDateTime(DateTime.Now),
                StartTime = TimeOnly.FromDateTime(DateTime.Now), EndTime = TimeOnly.FromDateTime(DateTime.Now), Organizer = "Rut miit", 
                Categories = {"Math","Chemistry","IT"}, City = "Moscow", Addres = "Obrazova 9"},
            
            new(){Id = Guid.NewGuid(),Name = "Conference2",Type = "offline",ShortTopic = "Conference2", FullTopic = "Conference2",Date = DateOnly.FromDateTime(DateTime.Now),
                StartTime = TimeOnly.FromDateTime(DateTime.Now), EndTime = TimeOnly.FromDateTime(DateTime.Now), Organizer = "Rut miit", 
                Categories = {"Math","Chemistry","IT"}, City = "Moscow", Addres = "Obrazova 9"},
            
            new(){Id = Guid.NewGuid(),Name = "Conference3",Type = "offline",ShortTopic = "Conference3", FullTopic = "Conference3",Date = DateOnly.FromDateTime(DateTime.Now),
                StartTime = TimeOnly.FromDateTime(DateTime.Now), EndTime = TimeOnly.FromDateTime(DateTime.Now), Organizer = "Rut miit", 
                Categories = {"Math","Chemistry","IT"}, City = "Moscow", Addres = "Obrazova 9"},
            
            new(){Id = Guid.NewGuid(),Name = "Conference4",Type = "offline",ShortTopic = "Conference4", FullTopic = "Conference4",Date = DateOnly.FromDateTime(DateTime.Now),
                StartTime = TimeOnly.FromDateTime(DateTime.Now), EndTime = TimeOnly.FromDateTime(DateTime.Now), Organizer = "Rut miit", 
                Categories = {"Math","Chemistry","IT"}, City = "Moscow", Addres = "Obrazova 9"}
        };
        
        modelBuilder.Entity<Conference>(entity =>
        {
            entity.HasKey(conference => new {conference.Id});

            entity.HasData(conferences);
            
            entity
                .HasMany(conf => conf.Sections)
                .WithOne(section => section.Conference);
        });
        
        modelBuilder.Entity<IdentityUserRole<Guid>>().HasKey(p => new { p.UserId, p.RoleId });
        
        /*modelBuilder.Entity<ApplicationUser>(apUser =>
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
        });*/

        modelBuilder.Entity<UsersConferences>(entity =>
        {
            entity.HasKey(source => new { source.UserId, source.ConferenceId });
        });

        modelBuilder.Entity<ApplicationUser>(entity =>
        {
            entity.HasOne(u => u.Questionnaire)
                .WithOne(q => q.ApplicationUser)
                .HasForeignKey<Questionnaire>(q=>q.UserId);
        });

        modelBuilder.Entity<PotentialParticipants>(entity =>
        {
            entity.HasKey(source => new { source.UserId, source.ConferenceId });
        });
    }
}

