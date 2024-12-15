using MediatR;
using Microsoft.AspNetCore.Identity;
using Module.User.Core.Commands;
using Module.User.Core.Entities;
using Shared.Models.Response;
using System.Web;

namespace Module.User.Core.Handlers;

public class ActivateUserCommandHandler : IRequestHandler<ActivateUserCommand, ApiResponse<bool>>
{
    private readonly UserManager<ApplicationUser> userManager;

    public ActivateUserCommandHandler(UserManager<ApplicationUser> _userManager)
    {
        userManager = _userManager;
    }

    public async Task<ApiResponse<bool>> Handle(ActivateUserCommand request, CancellationToken cancellationToken)
    {
        var response = new ApiResponse<bool>();
        try
        {
            if (!String.IsNullOrEmpty(request.userId) && !String.IsNullOrEmpty(request.token))
            {
                var user = await userManager.FindByIdAsync(request.userId);
                string codeHtmlVersion = HttpUtility.UrlDecode(request.token);

                IdentityResult result = await userManager.ConfirmEmailAsync(user, codeHtmlVersion);
                if (result.Succeeded)
                {
                    user.EmailConfirmed = true;
                    await userManager.UpdateAsync(user);

                    response.Data = true;
                    response.Message = "User activated successfully!";
                    return response;
                }
            }
            response.Data = false;
            response.Message = "User not activated!";
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
