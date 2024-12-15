using Microsoft.EntityFrameworkCore;
using Module.User.Core.Entities;

namespace Module.User.Core.Abstraction;

public interface IUserDbContext
{
    public DbSet<ApplicationGroup> ApplicationGroups { get; set; }
    public DbSet<ApplicationUserGroup> ApplicationUserGroups { get; set; }
    public DbSet<ApplicationCompany> ApplicationCompanies { get; set; }
    public DbSet<ApplicationUserConnection> ApplicationUserConnections { get; set; }
    public DbSet<ApplicationUserStatus> ApplicationUserStatuses { get; set; }
    public DbSet<ApplicationUserLog> ApplicationUserLogs { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
