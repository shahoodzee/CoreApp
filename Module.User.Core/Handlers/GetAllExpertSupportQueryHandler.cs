using MediatR;
using Microsoft.AspNetCore.Identity;
using Module.User.Core.Abstraction;
using Module.User.Core.Entities;
using Module.User.Core.Queries;
using Module.User.Core.ResponseDto;
using Shared.Models.Response;

namespace Module.User.Core.Handlers;

public class GetAllExpertSupportQueryHandler : IRequestHandler<GetAllExpertSupportQuery, ApiResponse<List<CustomerDto>>>
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<ApplicationRole> roleManager;

    public GetAllExpertSupportQueryHandler(UserManager<ApplicationUser> _userManager, RoleManager<ApplicationRole> _roleManager)
    {
        userManager = _userManager;
        roleManager = _roleManager;
    }

    public async Task<ApiResponse<List<CustomerDto>>> Handle(GetAllExpertSupportQuery request, CancellationToken cancellationToken)
    {
        var response = new ApiResponse<List<CustomerDto>>();
        response.Data = new List<CustomerDto>();
        try
        {
            var role = await roleManager.FindByIdAsync(request.iRoleId.ToString());
            if (role == null)
            {
                response.Message = "Expert support persons not found!";
                return response;
            }
            var customers = await userManager.GetUsersInRoleAsync(role.Name);
            if (customers.Count <= 0)
            {
                response.Message = "Expert support persons not found!";
                return response;
            }
            foreach (var customer in customers)
            {
                var customerObj = new CustomerDto();
                customerObj.userId = customer.Id;
                customerObj.name = $"{customer.FirstName} {customer.LastName}";
                customerObj.email = customer.Email;
                response.Data.Add(customerObj);
            }
            response.Message = "Expert support persons found!";
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
