using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Module.User.Core.Abstraction;
using Module.User.Core.Entities;
using Shared.Models.Contracts;
using System.Data;

namespace Module.User.Core.Consumer;

public class GetCCAndESUsersConsumer : IConsumer<FetchRequestCCAndES>
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IUserDbContext dbContext;
    public GetCCAndESUsersConsumer(UserManager<ApplicationUser> _userManager, IUserDbContext _dbContext)
    {
        userManager = _userManager;
        dbContext = _dbContext;
    }

    public async Task Consume(ConsumeContext<FetchRequestCCAndES> context)
    {
        FetchResponseCCAndES response = new FetchResponseCCAndES();
        List<FetchResponseUser> users = new List<FetchResponseUser>();
        try
        {
            List<ApplicationUser> usersList = new List<ApplicationUser>();
            var customerCareUsers = await userManager.GetUsersInRoleAsync("CustomerCare");
            var expertSupportUsers = await userManager.GetUsersInRoleAsync("ExpertSupport");

            usersList.AddRange(customerCareUsers);
            usersList.AddRange(expertSupportUsers);

            foreach (var userObj in usersList)
            {
                var company = await dbContext.ApplicationCompanies.Where(c => c.Id == userObj.iCompanyId).FirstOrDefaultAsync();
                var userConnection = await dbContext.ApplicationUserConnections.Where(x => x.UserId == userObj.Id).FirstOrDefaultAsync();
                FetchResponseUser userRes = new FetchResponseUser();
                userRes.userId = userObj.Id;
                userRes.firstName = userObj.FirstName;
                userRes.lastName = userObj.LastName;
                userRes.email = userObj.Email;
                userRes.connectionId = userConnection.ConnectionId;
                userRes.phoneNumber = userObj.PhoneNumber;
                userRes.companyId = userObj.iCompanyId;
                userRes.companyName = company == null ? "" : company.CompanyName;

                users.Add(userRes);
            }
            response.users = users;
            await context.RespondAsync<FetchResponseCCAndES>(response);
        }
        catch (Exception)
        {
            await context.RespondAsync<FetchResponseCCAndES>(response);
        }
    }
}
