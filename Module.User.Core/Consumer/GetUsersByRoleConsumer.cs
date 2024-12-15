using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Module.User.Core.Abstraction;
using Module.User.Core.Entities;
using Shared.Models.Contracts;

namespace Module.User.Core.Consumer;

public class GetUsersByRoleConsumer : IConsumer<FetchRequestUsersByRole>
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<ApplicationRole> roleManager;
    private readonly IUserDbContext dbContext;
    public GetUsersByRoleConsumer(UserManager<ApplicationUser> _userManager, RoleManager<ApplicationRole> _roleManager, IUserDbContext _dbcontext)
    {
        userManager = _userManager;
        roleManager = _roleManager;
        userManager = _userManager;
        dbContext = _dbcontext;
    }

    public async Task Consume(ConsumeContext<FetchRequestUsersByRole> context)
    {
        List<string> connectionIds = new List<string>();
        List<string> emails = new List<string>();
        List<long> userIds = new List<long>();
        try
        {
            long roleId = context.Message.roleId;

            var role = await roleManager.FindByIdAsync(roleId.ToString());
            if (role == null)
            {
                await context.RespondAsync<FetchResponseUsersByRole>(new { connectionIds, emails });
            }
            List<long> AllUserIds = new List<long>();
            var users = await userManager.GetUsersInRoleAsync(role.Name);
            AllUserIds = users.Select(u => u.Id).ToList();
            var userConnections = await dbContext.ApplicationUserConnections
                                                 .Where(x => AllUserIds.Contains(x.UserId))
                                                 .ToListAsync();
            foreach (var user in users)
            {
                var userConnection = userConnections.FirstOrDefault(uc => uc.UserId == user.Id);
                if (userConnection != null)
                {
                    connectionIds.Add(userConnection.ConnectionId);
                }
                emails.Add(user.Email);
                userIds.Add(user.Id);
            }
            await context.RespondAsync<FetchResponseUsersByRole>(new { connectionIds, emails, userIds });
        }
        catch (Exception)
        {
            await context.RespondAsync<FetchResponseUsersByRole>(new { connectionIds, emails, userIds });
        }
    }
}
