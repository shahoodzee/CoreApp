using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Module.User.Core.Commands;
using Module.User.Core.Entities;
using Module.User.Core.Helpers;
using Shared.Models.Contracts;
using Shared.Models.Response;
using System.Web;

namespace Module.User.Core.Handlers;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ApiResponse<bool>>
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<ApplicationRole> roleManager;
    private readonly IUserHelper userHelper;
    private readonly IConfiguration configuration;
    private readonly IBus bus;
    public CreateUserCommandHandler (
        UserManager<ApplicationUser> _userManager, 
        RoleManager<ApplicationRole> _roleManager, 
        IUserHelper _userHelper, 
        IConfiguration _configuration,
        IBus _bus
    )
    {
        userManager = _userManager;
        roleManager = _roleManager;
        userHelper = _userHelper;
        configuration = _configuration;
        bus = _bus;
    }

    public async Task<ApiResponse<bool>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var response = new ApiResponse<bool>();
        try
        {
            #region User Management
            var isUserExist = await userManager.FindByEmailAsync(request.email);
            if (isUserExist != null) 
            {
                response.Data = false;
                response.Message = "This email already exists!";
                return response; 
            }

            ApplicationUser objApplicationUser = new();
            objApplicationUser.FirstName = request.firstName;
            objApplicationUser.LastName = request.lastName;
            objApplicationUser.Email = request.email;
            objApplicationUser.EmailConfirmed = true;
            objApplicationUser.ProfileImage = request.profileImage;
            objApplicationUser.UserName = request.email;
            objApplicationUser.PhoneNumber = request.phoneNumber;
            objApplicationUser.EmailConfirmed = false;
            objApplicationUser.iCreatedBy = 1;
            objApplicationUser.CreatedDate = DateTime.UtcNow;

            objApplicationUser.iCompanyId = await userHelper.GetCompanyId(request.email, request.iCompanyId, request.companyName);

            var result = await userManager.CreateAsync(objApplicationUser, request.password);
            if (!result.Succeeded)
            {
                response.Data = false;
                response.Message = "User not created!";
                return response;
            }
            #endregion

            #region Role Management
            var role = await roleManager.FindByIdAsync(request.iUserRole.ToString());
            if (role != null)
            {
                await userManager.AddToRoleAsync(objApplicationUser, role.Name);
            }
            else
            {
                await userManager.AddToRoleAsync(objApplicationUser, "Customer");
            }
            #endregion


            #region Email Confirmation
            string confirmationToken = userManager.GenerateEmailConfirmationTokenAsync(objApplicationUser).Result;
            confirmationToken = HttpUtility.UrlEncode(confirmationToken);
            confirmationToken = Uri.EscapeDataString(confirmationToken);
            string _confirmationLink = request.origin + configuration["ConfirmationURL"] + "?userId=" + objApplicationUser.Id + "&Token=" + confirmationToken;

            // Publish to Email Module for sending Account Verification Email
            AccountVerificationContract contract = new AccountVerificationContract();
            contract.email = objApplicationUser.Email;
            contract.name = $"{objApplicationUser.FirstName} {objApplicationUser.LastName}";
            contract.confirmationLink = _confirmationLink;

            await bus.Publish(contract, cancellationToken);
            #endregion


            response.Data = true;
            response.Message = "User created successfully!";
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
