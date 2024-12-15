using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Module.User.Core.Abstraction;
using Module.User.Core.Entities;
using Shared.Models.Contracts;

namespace Module.User.Core.Consumer;

public class UpdateUserStatusConsumer : IConsumer<UpdateUserStatusContract>
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IUserDbContext dbContext;
    private readonly IBus bus;

    public UpdateUserStatusConsumer(IBus _bus, UserManager<ApplicationUser> _userManager, IUserDbContext _dbContext)
    {
        userManager = _userManager;
        dbContext = _dbContext;
        bus = _bus;
    }

    public async Task Consume(ConsumeContext<UpdateUserStatusContract> context)
    {
        try
        {
            UpdateUserStatusContract data = context.Message;

            foreach (var user in data.userIds)
            {
                var userConnection = await dbContext.ApplicationUserConnections.Where(x => x.UserId == user).FirstOrDefaultAsync();
                if (userConnection != null)
                {
                    userConnection.iUserStatus = data.statusId;
                    await dbContext.SaveChangesAsync(CancellationToken.None);
                }
            }

            #region Send user status update Signal
            UpdateLiveUserStatusContract updateLiveUserStatusContract = new UpdateLiveUserStatusContract();
            updateLiveUserStatusContract.userIds = data.userIds;
            await bus.Publish(updateLiveUserStatusContract);
            #endregion
        }
        catch (Exception)
        {
            return;
        }
    }
}
