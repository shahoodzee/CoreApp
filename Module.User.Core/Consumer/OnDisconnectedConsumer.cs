using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Module.User.Core.Abstraction;
using Module.User.Core.Entities;
using Shared.Models.Contracts;

namespace Module.User.Core.Consumer;

public class OnDisconnectedConsumer : IConsumer<OnDisconnectedContract>
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IUserDbContext dbContext;
    public OnDisconnectedConsumer(UserManager<ApplicationUser> _userManager, IUserDbContext _dbContext)
    {
        userManager = _userManager;
        dbContext = _dbContext;
    }

    public async Task Consume(ConsumeContext<OnDisconnectedContract> context)
    {
        try
        {
            string email = context.Message.email;

            var isUserExist = await userManager.FindByEmailAsync(email);
            if (isUserExist != null)
            {
                var userConnection = await dbContext.ApplicationUserConnections.Where(x => x.UserId == isUserExist.Id).FirstOrDefaultAsync();
                if (userConnection != null)
                {
                    userConnection.ConnectionId = null;
                    userConnection.iUserStatus = 2; //Offline
                }
                else
                {
                    ApplicationUserConnection applicationUserConnection = new ApplicationUserConnection();
                    applicationUserConnection.UserId = isUserExist.Id;
                    applicationUserConnection.ConnectionId = null;
                    applicationUserConnection.iUserStatus = 2; //Offline

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
