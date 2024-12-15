using MediatR;
using Microsoft.AspNetCore.Identity;
using Module.User.Core.Commands;
using Module.User.Core.Entities;
using Module.User.Core.Helpers;
using Shared.Models.Response;

namespace Module.User.Core.Handlers;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ApiResponse<bool>>
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<ApplicationRole> roleManager;
    private readonly IUserHelper userHelper;

    public UpdateUserCommandHandler(UserManager<ApplicationUser> _userManager, RoleManager<ApplicationRole> _roleManager, IUserHelper _userHelper)
    {
        userManager = _userManager;
        roleManager = _roleManager;
        userHelper = _userHelper;

    }
    public async Task<ApiResponse<bool>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var response = new ApiResponse<bool>();
        try
        {
            var userInfo = await userManager.FindByIdAsync(request.userId.ToString());
            if (userInfo != null)
            {
                userInfo.FirstName = request.firstName;
                userInfo.LastName = request.lastName;
                userInfo.PhoneNumber = request.phoneNumber;
                userInfo.ProfileImage = request.profileImage;
                userInfo.iModifiedBy = request.iModifiedBy;
                userInfo.ModifyDate = DateTime.UtcNow;

                userInfo.iCompanyId = await userHelper.GetCompanyId(userInfo.Email.ToString(), request.iCompanyId.Value, request.companyName);

                var result = await userManager.UpdateAsync(userInfo);
                if (!result.Succeeded)
                {
                    response.Data = false;
                    response.Message = "User not updated!";
                    return response;
                }

                #region Update Role
                var roles = await userManager.GetRolesAsync(userInfo);
                if (!roles.Contains(request.iUserRole.ToString()))
                {
                    foreach (var role in roles)
                    {
                        await userManager.RemoveFromRoleAsync(userInfo, role);
                    }
                    var roleDb = await roleManager.FindByIdAsync(request.iUserRole.ToString());
                    if (roleDb != null)
                    {
                        await userManager.AddToRoleAsync(userInfo, roleDb.Name);
                    }
                }
                #endregion

                response.Data = true;
                response.Message = "User updated successfully!";
                return response;
            }
            response.Data = false;
            response.Message = "User not updated!";
            return response;
        }
        catch (Exception ex)
        {
            response.Data = false;
            response.Message = ex.Message;
            return response;
        }
    }
}