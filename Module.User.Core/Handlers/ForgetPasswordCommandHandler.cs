using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Module.User.Core.Commands;
using Module.User.Core.Entities;
using Shared.Models.Contracts;
using Shared.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Module.User.Core.Handlers;

public class ForgetPasswordCommandHandler : IRequestHandler<ForgetPasswordCommand, ApiResponse<bool>>
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IBus bus;

    public ForgetPasswordCommandHandler(UserManager<ApplicationUser> _userManager, IBus _bus)
    {
        userManager = _userManager;
        bus = _bus;
    }

    public async Task<ApiResponse<bool>> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
    {
        var response = new ApiResponse<bool>();
        try
        {
            if (string.IsNullOrEmpty(request.email))
            {
                response.Data = false;
                response.Message = "Email address cannot be empty";
                return response;
            }

            var user = await userManager.FindByEmailAsync(request.email);
            if (user == null)
            {
                response.Data = false;
                response.Message = "User not found. Please check your email and try again.";
                return response;
            }

            string Resettoken = await userManager.GeneratePasswordResetTokenAsync(user);
            Resettoken = HttpUtility.UrlEncode(Resettoken);
            Resettoken = Uri.EscapeDataString(Resettoken);
            string _confirmationLink = request.origin + "/resetpassword" + "?userId=" + user.Id + "&Token=" + Resettoken;

            if (string.IsNullOrEmpty(_confirmationLink))
            {
                response.Data = false;
                response.Message = "Unable to reset password. Please try again.";
                return response;
            }

            ResetPasswordContract contract = new ResetPasswordContract();
            contract.email = user.Email;
            contract.name = $"{user.FirstName} {user.LastName}";
            contract.confirmationLink = _confirmationLink;
            await bus.Publish(contract, cancellationToken);

            response.Data = true;
            response.Message = "Please check your email for reset password link.";
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
