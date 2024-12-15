using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Module.User.Core.Abstraction;
using Module.User.Core.Commands;
using Module.User.Core.Entities;
using Module.User.Core.ResponseDto;
using Shared.Models.Response;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Module.User.Core.Handlers;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, ApiResponse<LoginDto>>
{
    private readonly IUserDbContext dbContext;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly SignInManager<ApplicationUser> signInManager;
    public LoginUserCommandHandler(IUserDbContext _dbContext, UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager)
    {
        dbContext = _dbContext;
        userManager = _userManager;
        signInManager = _signInManager;
    }

    public async Task<ApiResponse<LoginDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var response = new ApiResponse<LoginDto>();
        LoginDto resObj = new LoginDto();
        try
        {
            var user = await userManager.FindByEmailAsync(request.email);
            if (user == null)
            {
                response.Data = resObj;
                response.Message = "Email address not found!";
                return response;
            }
            var LoginUser = await signInManager.PasswordSignInAsync(user, request.password, false, true);
            if (LoginUser.IsNotAllowed)
            {
                response.Data = resObj;
                response.Message = "Please verify your email address!";
                return response;
            }
            else
            {
                resObj.isEmailConfirmed = true;
            }

            if (!LoginUser.Succeeded && !LoginUser.IsLockedOut)
            {
                response.Data = resObj;
                response.Message = "Incorrect password!";
                return response;
            }
            else if (!LoginUser.Succeeded && LoginUser.IsLockedOut)
            {
                response.Data = resObj;
                response.Message = "Account is disabled for 20 mins";
                return response;
            }
            else
            {
                string role = "";
                int usergroupid = -1;
                var UserRole = await userManager.GetRolesAsync(user);
                if (UserRole == null)
                {
                    role = "Customer";
                }
                else
                {
                    role = UserRole.Count > 0 ? UserRole.FirstOrDefault() : "Customer";
                }

                var usergroup = await dbContext.ApplicationUserGroups.Where(g => g.ApplicationUserId == user.Id).FirstOrDefaultAsync();
                if (usergroup != null)
                {
                    usergroupid = usergroup.ApplicationGroupId;
                }
                else
                {
                    usergroupid = -1;
                }

                var authClaims = new List<Claim>
                {
                    new Claim("mxui",user.Id.ToString()),
                    new Claim("mxug",usergroupid.ToString()),
                    new Claim("mxur",role),
                    new Claim("mxum",user.Email),
                    new Claim("mxun",user.FirstName + " " + user.LastName)
                };

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ByMX000OLlMQG6VVVp1OH7Xzyr7gHuw1qvUC5dcGt3SNM"));

                var token = new JwtSecurityToken(
                    expires: DateTime.UtcNow.AddHours(2),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

                resObj.Token = new JwtSecurityTokenHandler().WriteToken(token);

                ApplicationUserLog applicationUserLog = new ApplicationUserLog();
                applicationUserLog.UserId = user.Id;

                await dbContext.ApplicationUserLogs.AddAsync(applicationUserLog);
                await dbContext.SaveChangesAsync(cancellationToken);

                response.Message = "User logged in successfully!";
                response.Data = resObj;
                return response;
            }
        }
        catch (Exception ex)
        {
            response.Data = resObj;
            response.Message = ex.Message;
            return response;
        }
    }
}
