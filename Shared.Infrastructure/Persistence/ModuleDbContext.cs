using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Infrastructure.Persistence;

public abstract class ModuleDbContext : DbContext
{
    protected ModuleDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }

    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        using (var transaction = this.Database.BeginTransaction())
        {
            try
            {
                var result = await base.SaveChangesAsync(true, cancellationToken);
                transaction.Commit();
                return result;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
            }
        
        }
            return (await base.SaveChangesAsync(true, cancellationToken));
    }

    //public override async Task<bool> DeleteChanges(CancellationToken cancellationToken = default)
    //{
    //    return false;
    //}

}
