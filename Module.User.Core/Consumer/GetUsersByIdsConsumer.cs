using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Module.User.Core.Abstraction;
using Module.User.Core.Entities;
using Shared.Models.Contracts;

namespace Module.User.Core.Consumer;

public class GetUsersByIdsConsumer : IConsumer<FetchUserListIds>
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IUserDbContext dbContext;
    public GetUsersByIdsConsumer(UserManager<ApplicationUser> _userManager, IUserDbContext _dbcontext)
    {
        dbContext = _dbcontext;
        userManager = _userManager;
    }
    public async Task Consume(ConsumeContext<FetchUserListIds> context)
    {
        try
		{
            // Will change it later with store procedure call with db context instead of identity methods
            
            string userids = context.Message.userids;
            string[] UserList = userids.Split(',');
            List<FetchResponseUserListIds> lstUsers = new();
            for (int i = 0; i < UserList.Length; i++)
            {
                var user = await userManager.FindByIdAsync(UserList[i]);
                if (user != null)
                {
                    var UserRole = await userManager.GetRolesAsync(user);
                    var userConnection = await dbContext.ApplicationUserConnections.Where(x => x.UserId == long.Parse(UserList[i])).FirstOrDefaultAsync();
                    FetchResponseUserListIds objUser = new();
                    objUser.userid = user.Id;
                    objUser.email = user.Email;
                    string firstName = string.IsNullOrEmpty(user.FirstName) ? "" : user.FirstName;
                    string lastName = string.IsNullOrEmpty(user.LastName) ? "" : user.LastName;
                    objUser.name = firstName + " " + lastName;
                    objUser.connectionId = userConnection != null && !String.IsNullOrEmpty(userConnection.ConnectionId) ? userConnection.ConnectionId : "";
                    objUser.role = UserRole.FirstOrDefault();
                    objUser.userStatus = userConnection != null && userConnection.iUserStatus != -1 ? userConnection.iUserStatus : -1;
                    lstUsers.Add(objUser);
                }
            }
            FetchResponseUserListIdsObject objData = new();
            objData.lst = lstUsers;
            await context.RespondAsync<FetchResponseUserListIdsObject>(objData);
        }
		catch (Exception)
		{
            await context.RespondAsync<FetchResponseUserListIdsObject>(new { });
        }
    }
}
