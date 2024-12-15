using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Module.User.Core.Abstraction;
using Module.User.Core.Entities;
using Shared.Models.Contracts;
using System.Data;

namespace Module.User.Core.Consumer;

public class GetUserConsumer : IConsumer<FetchRequestUser>
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IUserDbContext dbContext;

    public GetUserConsumer(UserManager<ApplicationUser> _userManager, IUserDbContext _dbContext)
    {
        userManager = _userManager;
        dbContext = _dbContext;
    }

    public async Task Consume(ConsumeContext<FetchRequestUser> context)
    {
        try
        {
            string email = context.Message.email;
            string connectionId = context.Message.connectionId;
            long userId = context.Message.userId;

            ApplicationUser isUserExist = null;

            if (!string.IsNullOrEmpty(email))
            {
                isUserExist = await userManager.FindByEmailAsync(email);
            }
            else if (!string.IsNullOrEmpty(connectionId))
            {
                var userConnection = await dbContext.ApplicationUserConnections.Where(x => x.ConnectionId == connectionId).FirstOrDefaultAsync();
                isUserExist = await userManager.FindByIdAsync(userConnection.UserId.ToString());
            }
            else if (userId != -1)
            {
                isUserExist = await userManager.FindByIdAsync(userId.ToString());
            }

            string roleName = "";
            var UserRole = await userManager.GetRolesAsync(isUserExist);
            if (UserRole == null) {
                roleName = "Customer";
            }
            else {
                roleName = UserRole.Count > 0 ? UserRole.FirstOrDefault() : "Customer";
            }
            if (isUserExist != null)
            {
                var company = await dbContext.ApplicationCompanies.Where(c => c.Id == isUserExist.iCompanyId).FirstOrDefaultAsync();
                var userConnection = await dbContext.ApplicationUserConnections.Where(x => x.UserId == isUserExist.Id).FirstOrDefaultAsync();

                await context.RespondAsync<FetchResponseUser>(new
                {
                    userId = isUserExist.Id,
                    firstName = isUserExist.FirstName,
                    lastName = isUserExist.LastName,
                    email = isUserExist.Email,
                    connectionId = userConnection.ConnectionId,
                    phoneNumber = isUserExist.PhoneNumber,
                    profileImage = isUserExist.ProfileImage,
                    companyId = isUserExist.iCompanyId,
                    companyName = company == null ? "" : company.CompanyName,
                    role = roleName,
                });
            }
            else
            {
                await context.RespondAsync<FetchResponseUser>(new { });
            }
        }
        catch (Exception)
        {
            await context.RespondAsync<FetchResponseUser>(new { });
        }
    }
}
