using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Module.User.Core.Abstraction;
using Module.User.Core.Commands;
using Module.User.Core.Entities;
using Module.User.Core.ResponseDto;
using Shared.Models.Response;

namespace Module.User.Core.Handlers;

public class LogoutUserCommandHandler : IRequestHandler<LogoutUserCommand, ApiResponse<bool>>
{
    private readonly IUserDbContext dbContext;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly SignInManager<ApplicationUser> signInManager;
    public LogoutUserCommandHandler(IUserDbContext _dbContext, UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager)
    {
        dbContext = _dbContext;
        userManager = _userManager;
        signInManager = _signInManager;
    }

    public async Task<ApiResponse<bool>> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
    {
        var response = new ApiResponse<bool>();
        try
        {
            var userLog = await dbContext.ApplicationUserLogs.Where(l => l.UserId == request.userId && l.LogoutDateTime == null).FirstOrDefaultAsync();
            if (userLog != null)
            {
                userLog.LogoutDateTime = DateTime.UtcNow;
                await dbContext.SaveChangesAsync(cancellationToken);

                response.Data = true;
                response.Message = "User Logged Out!";
                return response;
            }
            response.Data = false;
            response.Message = "User not logged in!";
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
