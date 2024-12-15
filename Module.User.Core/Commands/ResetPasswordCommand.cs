using MediatR;
using Shared.Models.Response;

namespace Module.User.Core.Commands;

public class ResetPasswordCommand : IRequest<ApiResponse<bool>>
{
    public long userId { get; set; }
    public string token { get; set; }
    public string newpassword { get; set; }
}