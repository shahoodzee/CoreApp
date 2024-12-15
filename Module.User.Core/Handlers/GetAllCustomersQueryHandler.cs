using MediatR;
using Microsoft.AspNetCore.Identity;
using Module.User.Core.Abstraction;
using Module.User.Core.Entities;
using Module.User.Core.Queries;
using Module.User.Core.ResponseDto;
using Shared.Models.Response;

namespace Module.User.Core.Handlers;

public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, ApiResponse<List<CustomerDto>>>
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IUserDbContext dbContext;
    private readonly RoleManager<ApplicationRole> roleManager;

    public GetAllCustomersQueryHandler(UserManager<ApplicationUser> _userManager, IUserDbContext _dbContext, RoleManager<ApplicationRole> _roleManager)
    {
        userManager = _userManager;
        dbContext = _dbContext;
        roleManager = _roleManager;
    }

    public async Task<ApiResponse<List<CustomerDto>>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        var response = new ApiResponse<List<CustomerDto>>();
        response.Data = new List<CustomerDto>();
        try
        {
            var role = await roleManager.FindByIdAsync(request.iRoleId.ToString());
            if (role == null)
            {
                response.Message = "Customers not found!";
                return response;
            }
            var customers = await userManager.GetUsersInRoleAsync(role.Name);
            if (customers.Count <= 0)
            {
                response.Message = "Customers not found!";
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
            response.Message = "Customers found!";
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
