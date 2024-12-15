using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Module.User.Core.Abstraction;
using Module.User.Core.Entities;
using Shared.Models.Contracts;

namespace Module.User.Core.Consumer;

public class GetUsersByEmailConsumer : IConsumer<FetchUserIdsByEmail>
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IUserDbContext dbContext;
    public GetUsersByEmailConsumer(UserManager<ApplicationUser> _userManager, IUserDbContext _dbcontext)
    {
        dbContext = _dbcontext;
        userManager = _userManager;
    }

    public async Task Consume(ConsumeContext<FetchUserIdsByEmail> context)
    {
        try
        {
            string emails = context.Message.emails;
            string[] EmailsList = emails.Split(',');
            List<FetchResponseUserEmailListIds> lstUsers = new();
            for (int i = 0; i < EmailsList.Length; i++)
            {
                var user = await userManager.FindByEmailAsync(EmailsList[i]);
                var userConnection = await dbContext.ApplicationUserConnections.Where(x => x.UserId == user.Id).FirstOrDefaultAsync();
                if (user != null)
                {
                    FetchResponseUserEmailListIds objUser = new();
                    objUser.userid = user.Id;
                    objUser.email = user.Email;
                    string firstName = string.IsNullOrEmpty(user.FirstName) ? "" : user.FirstName;
                    string lastName = string.IsNullOrEmpty(user.LastName) ? "" : user.LastName;
                    objUser.name = firstName + " " + lastName;
                    objUser.connectionId = userConnection != null && !String.IsNullOrEmpty(userConnection.ConnectionId) ? userConnection.ConnectionId : "";
                    objUser.userStatus = userConnection != null && userConnection.iUserStatus != -1 ? userConnection.iUserStatus : -1;
                    lstUsers.Add(objUser);
                }
            }
            FetchResponseUserEmailListIdsObject objData = new();
            objData.lst = lstUsers;
            await context.RespondAsync<FetchResponseUserEmailListIdsObject>(objData);
        }
        catch (Exception)
        {
            await context.RespondAsync<FetchResponseUserEmailListIdsObject>(new { });
        }
    }
}
