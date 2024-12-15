using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Module.User.Core.Abstraction;
using Module.User.Core.Entities;
using Module.User.Core.Queries;
using Module.User.Core.ResponseDto;
using Shared.Models.Response;

namespace Module.User.Core.Handlers;

public class GetAllOnlineExpertSupport : IRequestHandler<GetAllOnlineExpertSupportQuery, ApiResponse<List<CustomerDto>>>
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IUserDbContext dbContext;
    private readonly RoleManager<ApplicationRole> roleManager;

    public GetAllOnlineExpertSupport(UserManager<ApplicationUser> _userManager, IUserDbContext _dbContext, RoleManager<ApplicationRole> _roleManager)
    {
        userManager = _userManager;
        dbContext = _dbContext;
        roleManager = _roleManager;
    }

    public async Task<ApiResponse<List<CustomerDto>>> Handle(GetAllOnlineExpertSupportQuery request, CancellationToken cancellationToken)
    {
        var response = new ApiResponse<List<CustomerDto>>();
        List<CustomerDto> customerDtos = new List<CustomerDto>();

        try
        {
            var role = await roleManager.FindByIdAsync(request.iRoleId.ToString());
            if (role == null)
            {
                response.Data = customerDtos;
                response.Message = "Expert Support not found!";
                return response;
            }
            var expertSupport = await userManager.GetUsersInRoleAsync(role.Name);
            var userIds = expertSupport.Select(u => u.Id).ToList();
            var userConnections = await dbContext.ApplicationUserConnections
                                                 .Where(x => userIds.Contains(x.UserId))
                                                 .ToListAsync();
            if (expertSupport.Count <= 0)
            {
                response.Data = customerDtos;
                response.Message = "Expert Support not found!";
                return response;
            }
            foreach (var expert in expertSupport)
            {
                var userConnection = userConnections.FirstOrDefault(uc => uc.UserId == expert.Id);
                if (userConnection != null && userConnection.ConnectionId != null && userConnection.iUserStatus != 3)
                {
                    var customerObj = new CustomerDto();
                    customerObj.userId = expert.Id;
                    customerObj.name = $"{expert.FirstName} {expert.LastName}";
                    customerObj.email = expert.Email;
                    customerDtos.Add(customerObj);
                }
            }
            response.Data = customerDtos;
            response.Message = "Expert Support found!";
            return response;
        }
        catch (Exception ex)
        {
            response.Data = null;
            response.Message = ex.Message;
            return response;
        }
    }
}
