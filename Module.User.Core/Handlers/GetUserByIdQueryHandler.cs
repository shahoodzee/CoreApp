using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Module.User.Core.Abstraction;
using Module.User.Core.Entities;
using Module.User.Core.Queries;
using Module.User.Core.ResponseDto;
using Shared.Models.Response;
using System.ComponentModel.Design;

namespace Module.User.Core.Handlers;

internal class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, ApiResponse<UserDto>>
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IUserDbContext dbContext;

    public GetUserByIdQueryHandler(UserManager<ApplicationUser> _userManager, IUserDbContext _dbContext)
    {
        userManager = _userManager;
        dbContext = _dbContext;
    }

    public async Task<ApiResponse<UserDto>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        var response = new ApiResponse<UserDto>();
        var userObj = new UserDto();
        try
        {
            var userInfo = await userManager.FindByIdAsync(query.userId.ToString());
            if (userInfo != null)
            {
                var company = await dbContext.ApplicationCompanies.Where(c => c.Id == userInfo.iCompanyId).FirstOrDefaultAsync();

                userObj.userId = userInfo.Id;
                userObj.firstName = userInfo.FirstName;
                userObj.lastName = userInfo.LastName;
                userObj.email = userInfo.Email;
                userObj.phoneNumber = userInfo.PhoneNumber;
                userObj.profileImage = userInfo.ProfileImage;
                userObj.companyId = userInfo.iCompanyId;
                userObj.companyName = company == null ? "" : company.CompanyName;

                response.Data = userObj;
                response.Message = "User found successfully!";
                return response;
            }
            response.Data = userObj;
            response.Message = "User not found!";
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
