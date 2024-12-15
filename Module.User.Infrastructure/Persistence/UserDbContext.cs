using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Module.User.Core.Abstraction;
using Module.User.Core.Entities;
using Shared.Infrastructure.Persistence;

namespace Module.User.Infrastructure.Persistence;

public class UserDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, long>, IUserDbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
        ChangeTracker.LazyLoadingEnabled = false;
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return (await base.SaveChangesAsync(true, cancellationToken));
    }

    public DbSet<ApplicationGroup> ApplicationGroups { get; set; }
    public DbSet<ApplicationUserGroup> ApplicationUserGroups { get; set; }
    public DbSet<ApplicationCompany> ApplicationCompanies { get; set; }
    public DbSet<ApplicationUserConnection> ApplicationUserConnections { get; set; }
    public DbSet<ApplicationUserStatus> ApplicationUserStatuses { get; set; }
    public DbSet<ApplicationUserLog> ApplicationUserLogs { get; set; }

}
