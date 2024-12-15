using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Module.User.Core.Abstraction;
using Module.User.Core.Entities;
using Shared.Models.Contracts;

namespace Module.User.Core.Consumer;

public class OnConnectedConsumer : IConsumer<OnConnectedContract>
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IUserDbContext dbContext;

    public OnConnectedConsumer(UserManager<ApplicationUser> _userManager, IUserDbContext _dbContext)
    {
        userManager = _userManager;
        dbContext = _dbContext;
    }
    public async Task Consume(ConsumeContext<OnConnectedContract> context)
    {
        try
        {
            string connectionId = context.Message.connectionId;
            string email = context.Message.email;

            var isUserExist = await userManager.FindByEmailAsync(email);
            if (isUserExist != null)
            {
                var userConnection = await dbContext.ApplicationUserConnections.Where(x => x.UserId == isUserExist.Id).FirstOrDefaultAsync();
                if (userConnection != null)
                {
                    userConnection.ConnectionId = connectionId;
                    userConnection.iUserStatus = 1; //Online
                }
                else
                {
                    ApplicationUserConnection applicationUserConnection = new ApplicationUserConnection();
                    applicationUserConnection.UserId = isUserExist.Id;
                    applicationUserConnection.ConnectionId = connectionId;
                    applicationUserConnection.iUserStatus = 1; //Online

                    var result = await dbContext.ApplicationUserConnections.AddAsync(applicationUserConnection);
                }
                await dbContext.SaveChangesAsync(CancellationToken.None);
            }
        }
        catch (Exception)
        {
            return;
        }
    }
}
