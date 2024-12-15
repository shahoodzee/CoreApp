using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Module.User.Core.Commands;
using Module.User.Core.Entities;
using Shared.Models.Response;
using System.Web;

namespace Module.User.Core.Handlers;

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, ApiResponse<bool>>
{
    private readonly UserManager<ApplicationUser> userManager;
    public ResetPasswordCommandHandler(UserManager<ApplicationUser> _userManager)
    {
        userManager = _userManager;
    }
    public async Task<ApiResponse<bool>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var response = new ApiResponse<bool>();
        try
        {
            if (string.IsNullOrEmpty(request.token))
            {
                response.Data = false;
                response.Message = "Token verification failed. Please try again!";
                return response;
            }
            var user = await userManager.FindByIdAsync(request.userId.ToString());
            if (user == null)
            {
                response.Data = false;
                response.Message = "Reset Password verficiation failed. Please try again!";
                return response;
            }
            string token = HttpUtility.UrlDecode(request.token);
            var result = await userManager.ResetPasswordAsync(user, token, request.newpassword);
            if (result.Succeeded)
            {
                response.Data = true;
                response.Message = "Password reset successfully!";
                return response;
            }
            response.Data = false;
            response.Message = "Unable to reset password.";
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
